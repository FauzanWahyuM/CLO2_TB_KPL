using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_FauzanWahyuM.Modules;
using Tubes_FauzanWahyuM.Models;

namespace Tubes_FauzanWahyuM
{
    class Program
    {
        static Dictionary<string, List<string>> dataKaryawan;

        static void Main()
        {
            try
            {
                dataKaryawan = FileHandler.MuatDariFile(); // Load data dari file
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Gagal memuat data karyawan: {ex.Message}");
                dataKaryawan = new Dictionary<string, List<string>>(); // Inisialisasi kosong jika gagal
            }

            bool exitProgram = false;

            while (!exitProgram) // Looping login setelah logout
            {
                Console.Clear();
                Console.WriteLine("==== Sistem Penjadwalan Tugas Karyawan ====");

                // Login karyawan (Multi-User)
                UserSession.Login();
                string karyawanAktif = UserSession.GetKaryawanAktif();

                // Defensive Programming: Pastikan login berhasil
                if (string.IsNullOrEmpty(karyawanAktif))
                {
                    Console.WriteLine("[ERROR] Login gagal. Coba lagi.");
                    continue; // Kembali ke login
                }

                if (!dataKaryawan.ContainsKey(karyawanAktif))
                {
                    dataKaryawan[karyawanAktif] = new List<string>();
                }

                bool kembaliKeLogin = false;
                while (!kembaliKeLogin) // Looping menu utama
                {
                    Console.Clear();
                    Console.WriteLine($"Karyawan: {karyawanAktif}");
                    Console.WriteLine("1. Mendaftarkan tugas");
                    Console.WriteLine("2. Tugas yang sudah disimpan");
                    Console.WriteLine("3. Statistik & Laporan Karyawan");
                    Console.WriteLine("4. Logout");
                    Console.WriteLine("5. Keluar Program");
                    Console.Write("Pilih menu: ");

                    string pilihan = Console.ReadLine()?.Trim() ?? "";

                    switch (pilihan)
                    {
                        case "1":
                            DaftarTugas(karyawanAktif);
                            break;
                        case "2":
                            Console.Clear();
                            Console.WriteLine("Tugas yang sudah disimpan:");
                            TableDrivenJadwal.TampilkanJadwal(karyawanAktif, dataKaryawan);
                            Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
                            Console.ReadKey();
                            break;
                        case "3":
                            Console.Clear();
                            StatistikKaryawan.TampilkanLaporan(dataKaryawan);
                            Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
                            Console.ReadKey();
                            break;
                        case "4":
                            Console.WriteLine("Logout...");
                            kembaliKeLogin = true; // Kembali ke login
                            break;
                        case "5":
                            Console.WriteLine("Keluar dari program...");
                            try
                            {
                                FileHandler.SimpanKeFile(dataKaryawan); // Simpan data sebelum keluar
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"[ERROR] Gagal menyimpan data: {ex.Message}");
                            }
                            exitProgram = true;
                            kembaliKeLogin = true; // Menghentikan loop login
                            break;
                        default:
                            Console.WriteLine("Pilihan tidak valid! Tekan sembarang tombol untuk kembali ke menu.");
                            Console.ReadKey();
                            break;
                    }
                }
            }
        }

        static void DaftarTugas(string karyawan)
        {
            Console.Clear();
            Console.WriteLine("Jadwal yang telah disimpan:");
            TableDrivenJadwal.TampilkanJadwal(karyawan, dataKaryawan);

            Console.Write("\nMasukkan status karyawan (Junior, Middle, Senior): ");
            string statusInput = Console.ReadLine()?.Trim() ?? "";

            if (!Enum.TryParse(statusInput, true, out StatusKaryawan status))
            {
                Console.WriteLine("Status tidak valid!");
                Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
                Console.ReadKey();
                return;
            }

            List<string> tugas = AutomataStatus.GetTugas(status);
            tugas.RemoveAll(t => !AutomataPemesanan.CekTugasTersedia(t));

            if (tugas.Count == 0)
            {
                Console.WriteLine("Tidak ada tugas tersedia untuk status ini.");
                Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Tugas yang tersedia untuk {status}:");
            foreach (var t in tugas)
            {
                Console.WriteLine($"- {t} (Prioritas: {PrioritasTugas.GetPrioritas(t)})");
            }

            Console.Write("Pilih tugas: ");
            string tugasDipilih = Console.ReadLine()?.Trim() ?? "";

            string tugasDipilihFormatted = tugas.Find(t => t.Equals(tugasDipilih, StringComparison.OrdinalIgnoreCase));

            if (tugasDipilihFormatted == null)
            {
                Console.WriteLine("Tugas tidak valid atau sudah diambil oleh karyawan lain!");
                Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nJadwal yang telah disimpan:");
            TableDrivenJadwal.TampilkanJadwal(karyawan, dataKaryawan);

            if (TableDrivenJadwal.CekTugasTersimpan(karyawan, tugasDipilihFormatted, dataKaryawan))
            {
                Console.WriteLine("Tugas ini sudah disimpan sebelumnya. Pilih tugas lain.");
                Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
                Console.ReadKey();
                return;
            }

            var batas = TableDrivenBatas.GetBatasWaktu(status);
            Console.Write($"Masukkan durasi pengerjaan ({batas.Item1}-{batas.Item2} jam): ");

            if (!int.TryParse(Console.ReadLine()?.Trim(), out int durasi) || durasi < batas.Item1 || durasi > batas.Item2)
            {
                Console.WriteLine("Durasi tidak valid!");
                Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
                Console.ReadKey();
                return;
            }

            if (AutomataPemesanan.AmbilTugas(tugasDipilihFormatted))
            {
                TableDrivenJadwal.SimpanJadwal(karyawan, tugasDipilihFormatted, durasi, dataKaryawan);

                try
                {
                    FileHandler.SimpanKeFile(dataKaryawan);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Gagal menyimpan data: {ex.Message}");
                }

                Console.WriteLine("Jadwal tugas berhasil disimpan!");
            }
            else
            {
                Console.WriteLine("Tugas sudah diambil oleh karyawan lain! Silakan pilih tugas lain.");
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
            Console.ReadKey();
        }
    }
}
