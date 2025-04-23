using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_Kelompok_BisaYukk.Models;

namespace Tubes_Kelompok_BisaYukk.Modules
{
    public class TableDrivenBatas
    {
        private static Dictionary<StatusKaryawan, Tuple<int, int>> batasWaktuTugas = new Dictionary<StatusKaryawan, Tuple<int, int>>
        {
            { StatusKaryawan.Intern, Tuple.Create(2, 6) },
            { StatusKaryawan.JuniorStaff, Tuple.Create(4, 8) },
            { StatusKaryawan.SeniorStaff, Tuple.Create(6, 10) }
        };

        public static Tuple<int, int> GetBatasWaktu(StatusKaryawan status)
        {
            return batasWaktuTugas[status];
        }
    }
}
