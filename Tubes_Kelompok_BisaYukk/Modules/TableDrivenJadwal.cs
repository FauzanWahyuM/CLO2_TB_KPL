using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes_Kelompok_BisaYukk.Modules
{
    public static class TableDrivenJadwal
    {
        public static void SimpanJadwal(string karyawan, string tugas, int durasi, Dictionary<string, List<string>> data)
        {
            if (!data.ContainsKey(karyawan))
            {
                data[karyawan] = new List<string>();
            }

            string formatTugas = $"Tugas: {tugas}, Durasi: {durasi} jam";
            data[karyawan].Add(formatTugas);
        }

        public static void TampilkanJadwal(string karyawan, Dictionary<string, List<string>> data)
        {
            if (data.ContainsKey(karyawan) && data[karyawan].Count > 0)
            {
                foreach (var tugas in data[karyawan])
                {
                    Console.WriteLine("- " + tugas);
                }
            }
            else
            {
                Console.WriteLine("Belum ada tugas yang disimpan.");
            }
        }

        public static bool CekTugasTersimpan(string karyawan, string tugas, Dictionary<string, List<string>> data)
        {
            if (data.ContainsKey(karyawan))
            {
                foreach (var item in data[karyawan])
                {
                    if (item.Contains(tugas))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static List<string> GetTugasKaryawan(string karyawan, Dictionary<string, List<string>> data)
        {
            if (data.ContainsKey(karyawan))
            {
                // Hanya ambil tugas yang belum selesai
                List<string> belumSelesai = new List<string>();
                foreach (var tugas in data[karyawan])
                {
                    if (!tugas.Contains("(Selesai)"))
                    {
                        belumSelesai.Add(tugas);
                    }
                }
                return belumSelesai;
            }
            return new List<string>();
        }

        public static void SelesaikanTugas(string karyawan, string tugas, Dictionary<string, List<string>> data)
        {
            if (data.ContainsKey(karyawan))
            {
                for (int i = 0; i < data[karyawan].Count; i++)
                {
                    if (data[karyawan][i].Contains(tugas))
                    {
                        // Hilangkan tanda lama jika ada
                        data[karyawan][i] = data[karyawan][i]
                            .Replace("(Selesai)", "")
                            .Replace("[SELESAI]", "")
                            .Trim();

                        // Tambahkan tanda selesai standar
                        data[karyawan][i] += " [SELESAI]";
                        break;
                    }
                }
            }
        }

        public static Dictionary<string, List<string>> GetTugasSelesai(Dictionary<string, List<string>> data)
        {
            var hasil = new Dictionary<string, List<string>>();

            foreach (var karyawan in data)
            {
                var listSelesai = karyawan.Value
                    .Where(tugas => tugas.Contains("[SELESAI]"))
                    .ToList();

                if (listSelesai.Count > 0)
                {
                    hasil[karyawan.Key] = listSelesai;
                }
            }

            return hasil;
        }

        public static void NormalisasiTugasSelesai(Dictionary<string, List<string>> data)
        {
            foreach (var karyawan in data.Keys.ToList())
            {
                for (int i = 0; i < data[karyawan].Count; i++)
                {
                    string tugas = data[karyawan][i];

                    // Jika mengandung "(Selesai)" tapi belum [SELESAI]
                    if (tugas.Contains("(Selesai)"))
                    {
                        tugas = tugas.Replace("(Selesai)", "").Trim();

                        // Hindari duplikat penanda
                        if (!tugas.EndsWith("[SELESAI]"))
                        {
                            tugas += " [SELESAI]";
                        }

                        data[karyawan][i] = tugas;
                    }
                }
            }
        }
    }
}
