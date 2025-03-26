using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes_FauzanWahyuM.Modules
{
    public static class StatistikKaryawan
    {
        public static void TampilkanLaporan(Dictionary<string, List<string>> data)
        {
            Console.WriteLine("\nLaporan Kinerja Karyawan:");
            foreach (var karyawan in data.Keys)
            {
                Console.WriteLine($"{karyawan} - Tugas selesai: {data[karyawan].Count}");
            }
        }
    }
}
