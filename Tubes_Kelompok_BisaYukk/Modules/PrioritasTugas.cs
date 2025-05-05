using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes_Kelompok_BisaYukk.Modules
{
    public class PrioritasTugas
    {
        // Tambahkan daftarPrioritas agar tidak error CS0103
        private static Dictionary<string, string> daftarPrioritas = new Dictionary<string, string>
        {
            { "Entry Task 1", "Rendah" },
            { "Entry Task 2", "Sedang" },
            { "Intermediate Task 1", "Sedang" },
            { "Intermediate Task 2", "Tinggi" },
            { "Advanced Task 1", "Tinggi" },
            { "Advanced Task 2", "Tinggi" }
        };

        // Gabungkan metode GetPrioritas
        public static string GetPrioritas(string tugas)
        {
            return daftarPrioritas.ContainsKey(tugas) ? daftarPrioritas[tugas] : "Tidak Diketahui";
        }

        public static void AturPrioritas(string tugas, string prioritas)
        {
            if (!string.IsNullOrEmpty(tugas) && !string.IsNullOrEmpty(prioritas))
            {
                daftarPrioritas[tugas] = prioritas;
            }
        }

        public static Dictionary<string, string> GetSemuaPrioritas()
        {
            return new Dictionary<string, string>(daftarPrioritas);
        }

        public static void HapusPrioritas(string tugas)
        {
            if (daftarPrioritas.ContainsKey(tugas))
            {
                daftarPrioritas.Remove(tugas);
            }
        }
    }
}
