using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubes_FauzanWahyuM.Modules
{
    public static class PrioritasTugas
    {
        private static Dictionary<string, string> aturanPrioritas = new Dictionary<string, string>
        {
            { "Entry Task 1", "Rendah" },
            { "Entry Task 2", "Sedang" },
            { "Intermediate Task 1", "Sedang" },
            { "Intermediate Task 2", "Tinggi" },
            { "Advanced Task 1", "Tinggi" },
            { "Advanced Task 2", "Tinggi" }
        };

        public static string GetPrioritas(string tugas)
        {
            return aturanPrioritas.ContainsKey(tugas) ? aturanPrioritas[tugas] : "Tidak Diketahui";
        }
    }
}
