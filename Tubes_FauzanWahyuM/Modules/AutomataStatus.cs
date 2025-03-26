using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_FauzanWahyuM.Models;

namespace Tubes_FauzanWahyuM.Modules
{
    public class AutomataStatus
    {
        public static List<string> GetTugas(StatusKaryawan status)
        {
            return status == StatusKaryawan.Junior ? new List<string> { "Entry Task 1", "Entry Task 2" } :
                   status == StatusKaryawan.Middle ? new List<string> { "Intermediate Task 1", "Intermediate Task 2" } :
                   new List<string> { "Advanced Task 1", "Advanced Task 2" };
        }
    }
}
