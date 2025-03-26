using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes_FauzanWahyuM.Modules
{
    public class TableDrivenJadwal
    {
        private static List<string> jadwalTugas = new List<string>();

        public static void SimpanJadwal(string karyawan, string tugas, int durasi, Dictionary<string, List<string>> dataKaryawan)
        {
            if (!dataKaryawan.ContainsKey(karyawan))
            {
                dataKaryawan[karyawan] = new List<string>();
            }

            dataKaryawan[karyawan].Add($"{tugas} - Durasi: {durasi} jam");
        }

        public static void TampilkanJadwal(string karyawan, Dictionary<string, List<string>> dataKaryawan)
        {
            if (!dataKaryawan.ContainsKey(karyawan) || dataKaryawan[karyawan].Count == 0)
            {
                Console.WriteLine("Belum ada tugas yang disimpan untuk karyawan ini.");
            }
            else
            {
                Console.WriteLine($"Jadwal tugas untuk {karyawan}:");
                foreach (var tugas in dataKaryawan[karyawan])
                {
                    Console.WriteLine($"{tugas}");
                }
            }
        }

        public static bool CekTugasTersimpan(string karyawan, string tugas, Dictionary<string, List<string>> dataKaryawan)
        {
            return dataKaryawan.ContainsKey(karyawan) && dataKaryawan[karyawan].Contains(tugas);
        }

        public static void SelesaikanTugas(string karyawan, string tugas, Dictionary<string, List<string>> dataKaryawan)
        {
            if (dataKaryawan.ContainsKey(karyawan) && dataKaryawan[karyawan].Contains(tugas))
            {
                dataKaryawan[karyawan].Remove(tugas);
                Console.WriteLine($"Tugas \"{tugas}\" telah diselesaikan dan dihapus dari jadwal.");
            }
            else
            {
                Console.WriteLine("Tugas tidak ditemukan dalam daftar tugas Anda.");
            }
        }
        public static List<string> GetTugasKaryawan(string karyawan, Dictionary<string, List<string>> dataKaryawan)
        {
            return dataKaryawan.ContainsKey(karyawan) ? new List<string>(dataKaryawan[karyawan]) : new List<string>();
        }
    }
}
