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

        public static bool TambahTugas(string tugas, Dictionary<string, List<string>> dataKaryawan)
        {
            if (string.IsNullOrWhiteSpace(tugas))
                return false;

            foreach (var tugasList in dataKaryawan.Values)
            {
                if (tugasList.Any(t => t.Contains(tugas) && t.Contains("[SELESAI]")))
                {
                    Console.WriteLine("Tugas ini sudah pernah diselesaikan oleh karyawan. Tidak bisa ditambahkan kembali.");
                    return false;
                }
            }

            if (!tugasTersedia.ContainsKey(tugas))
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
