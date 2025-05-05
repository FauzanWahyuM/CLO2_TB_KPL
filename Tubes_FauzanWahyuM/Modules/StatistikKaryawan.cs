using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes_FauzanWahyuM.Modules
{
    public static class StatistikKaryawan
    {
        public static void TampilkanLaporan(Dictionary<string, List<string>> dataKaryawan)
        {
            Console.WriteLine("=== Statistik dan Laporan Kinerja Karyawan ===\n");

            foreach (var karyawan in dataKaryawan)
            {
                string nama = karyawan.Key;
                List<string> daftarTugas = karyawan.Value;

                int total = daftarTugas.Count;
                int selesai = 0;
                int dalamProses = 0;

                foreach (var tugas in daftarTugas)
                {
                    if (tugas.Contains("(Selesai)"))
                    {
                        selesai++;
                    }
                    else
                    {
                        dalamProses++;
                    }
                }

                Console.WriteLine($"Karyawan: {nama}");
                Console.WriteLine($"- Total Tugas       : {total}");
                Console.WriteLine($"- Tugas Selesai     : {selesai}");
                Console.WriteLine($"- Masih Dalam Proses: {dalamProses}\n");
            }
        }
    }
}
