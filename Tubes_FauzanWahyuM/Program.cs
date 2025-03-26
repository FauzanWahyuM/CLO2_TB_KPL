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
                Console.WriteLine("[ERROR] Gagal memuat data karyawan: " + ex.Message);
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
                    continue;
                }

                if (!dataKaryawan.ContainsKey(karyawanAktif))
                {
                    dataKaryawan[karyawanAktif] = new List<string>();
                }

                bool kembaliKeLogin = false;
                while (!kembaliKeLogin)
                {
                    Console.Clear();
                    Console.WriteLine("Karyawan: " + karyawanAktif);
                    Console.WriteLine("1. Mendaftarkan tugas");
                    Console.WriteLine("2. Tugas yang sudah disimpan");
                    Console.WriteLine("3. Selesaikan tugas");
                    Console.WriteLine("4. Statistik & Laporan Karyawan");
                    Console.WriteLine("5. Logout");
                    Console.WriteLine("6. Keluar Program");
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
                            SelesaikanTugas(karyawanAktif);
                            break;
                        case "4":
                            Console.Clear();
                            StatistikKaryawan.TampilkanLaporan(dataKaryawan);
                            Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
                            Console.ReadKey();
                            break;
                        case "5":
                            Console.WriteLine("Logout...");
                            kembaliKeLogin = true;
                            break;
                        case "6":
                            Console.WriteLine("Keluar dari program...");
                            try
                            {
                                FileHandler.SimpanKeFile(dataKaryawan);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("[ERROR] Gagal menyimpan data: " + ex.Message);
                            }
                            exitProgram = true;
                            kembaliKeLogin = true;
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

            Console.WriteLine("Tugas yang tersedia untuk " + status + ":");
            foreach (var t in tugas)
            {
                Console.WriteLine("- " + t + " (Prioritas: " + PrioritasTugas.GetPrioritas(t) + ")");
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
            Console.Write("Masukkan durasi pengerjaan (" + batas.Item1 + "-" + batas.Item2 + " jam): ");

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
                FileHandler.SimpanKeFile(dataKaryawan);
                Console.WriteLine("Jadwal tugas berhasil disimpan!");
            }
            else
            {
                Console.WriteLine("Tugas sudah diambil oleh karyawan lain! Silakan pilih tugas lain.");
            }

            Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
            Console.ReadKey();
        }

        static void SelesaikanTugas(string karyawan)
        {
            while (true) // Loop agar tetap di menu hingga memilih keluar
            {
                Console.Clear();
                Console.WriteLine("Tugas yang sedang dikerjakan:");

                List<string> tugasKaryawan = TableDrivenJadwal.GetTugasKaryawan(karyawan, dataKaryawan);

                if (tugasKaryawan.Count == 0)
                {
                    Console.WriteLine("Tidak ada tugas yang sedang dikerjakan.");
                    Console.WriteLine("\nTekan sembarang tombol untuk kembali ke menu utama...");
                    Console.ReadKey();
                    return;
                }

                // Tampilkan tugas dengan nomor urut
                for (int i = 0; i < tugasKaryawan.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {tugasKaryawan[i]}");
                }
                Console.WriteLine("0. Kembali ke menu utama");

                Console.Write("\nMasukkan nomor tugas yang ingin diselesaikan (atau 0 untuk keluar): ");
                if (int.TryParse(Console.ReadLine()?.Trim(), out int nomorTugas))
                {
                    if (nomorTugas == 0)
                    {
                        return; // Kembali ke menu utama
                    }
                    if (nomorTugas > 0 && nomorTugas <= tugasKaryawan.Count)
                    {
                        string tugasSelesai = tugasKaryawan[nomorTugas - 1];
                        TableDrivenJadwal.SelesaikanTugas(karyawan, tugasSelesai, dataKaryawan);
                        FileHandler.SimpanKeFile(dataKaryawan);
                        Console.WriteLine($"Tugas \"{tugasSelesai}\" telah diselesaikan.");
                    }
                    else
                    {
                        Console.WriteLine("Nomor tugas tidak valid. Coba lagi.");
                    }
                }
                else
                {
                    Console.WriteLine("Input tidak valid. Masukkan angka tugas atau 0 untuk keluar.");
                }

                Console.WriteLine("\nTekan sembarang tombol untuk kembali...");
                Console.ReadKey();
            }
        }
    }
}