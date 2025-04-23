using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes_Kelompok_BisaYukk.Modules
{
    public static class AutomataPemesanan
    {
        private static Dictionary<string, bool> tugasTersedia = new Dictionary<string, bool>();

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

        public static bool TambahTugas(string tugas)
        {
            if (!string.IsNullOrEmpty(tugas) && !tugasTersedia.ContainsKey(tugas))
            {
                tugasTersedia[tugas] = true;
                return true;
            }
            return false;
        }

        public static bool HapusTugas(string tugas)
        {
            return tugasTersedia.Remove(tugas);
        }

        public static Dictionary<string, bool> GetSemuaTugas()
        {
            return new Dictionary<string, bool>(tugasTersedia);
        }
    }
}
