using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_FauzanWahyuM.Models;

namespace Tubes_FauzanWahyuM.Modules
{
    public class TableDrivenBatas
    {
        private static Dictionary<StatusKaryawan, Tuple<int, int>> batasWaktuTugas = new Dictionary<StatusKaryawan, Tuple<int, int>>
        {
            { StatusKaryawan.Junior, Tuple.Create(2, 6) },
            { StatusKaryawan.Middle, Tuple.Create(4, 8) },
            { StatusKaryawan.Senior, Tuple.Create(6, 10) }
        };

        public static Tuple<int, int> GetBatasWaktu(StatusKaryawan status)
        {
            return batasWaktuTugas[status];
        }
    }
}
