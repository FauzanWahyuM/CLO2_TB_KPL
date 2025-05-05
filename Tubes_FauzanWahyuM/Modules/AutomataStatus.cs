using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_FauzanWahyuM.Models;

namespace Tubes_FauzanWahyuM.Modules
{
    public static class AutomataStatus
    {
        private static Dictionary<StatusKaryawan, List<string>> tugasPerStatus = new Dictionary<StatusKaryawan, List<string>>
        {
            { StatusKaryawan.Junior, new List<string>() },
            { StatusKaryawan.Middle, new List<string>() },
            { StatusKaryawan.Senior, new List<string>() }
        };

        public static List<string> GetTugas(StatusKaryawan status)
        {
            return tugasPerStatus.ContainsKey(status) ? new List<string>(tugasPerStatus[status]) : new List<string>();
        }

        public static void TambahTugasUntukStatus(StatusKaryawan status, string tugas)
        {
            if (!tugasPerStatus.ContainsKey(status))
            {
                tugasPerStatus[status] = new List<string>();
            }

            if (!tugasPerStatus[status].Contains(tugas))
            {
                tugasPerStatus[status].Add(tugas);
            }
        }

        public static void HapusTugasDariStatus(StatusKaryawan status, string tugas)
        {
            if (tugasPerStatus.ContainsKey(status))
            {
                tugasPerStatus[status].Remove(tugas);
            }
        }
    }
}
