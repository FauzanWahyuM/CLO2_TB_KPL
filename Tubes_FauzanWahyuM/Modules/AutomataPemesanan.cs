using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes_FauzanWahyuM.Modules
{
    public class AutomataPemesanan
    {
        private static Dictionary<string, bool> tugasTersedia = new Dictionary<string, bool>
        {
            { "Entry Task 1", true },
            { "Entry Task 2", true },
            { "Intermediate Task 1", true },
            { "Intermediate Task 2", true },
            { "Advanced Task 1", true },
            { "Advanced Task 2", true }
        };

        public static bool CekTugasTersedia(string tugas)
        {
            return tugasTersedia.ContainsKey(tugas) && tugasTersedia[tugas];
        }

        public static bool AmbilTugas(string tugas)
        {
            if (CekTugasTersedia(tugas))
            {
                tugasTersedia[tugas] = false;
                return true;
            }
            return false;
        }
    }
}