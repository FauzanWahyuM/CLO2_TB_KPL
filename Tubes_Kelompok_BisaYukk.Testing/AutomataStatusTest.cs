using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_Kelompok_BisaYukk.Models;
using Tubes_Kelompok_BisaYukk.Modules;
using Xunit;

namespace Tubes_Kelompok_BisaYukk.Testing
{
    public class AutomataStatusTests
    {
        [Fact]
        public void TambahTugasUntukStatus_ShouldAddTugas()
        {
            AutomataStatus.TambahTugasUntukStatus(StatusKaryawan.Intern, "Research");
            var tugas = AutomataStatus.GetTugas(StatusKaryawan.Intern);
            Assert.Contains("Research", tugas);
        }

        [Fact]
        public void HapusTugasDariStatus_ShouldRemoveTugas()
        {
            AutomataStatus.TambahTugasUntukStatus(StatusKaryawan.JuniorStaff, "Testing");
            AutomataStatus.HapusTugasDariStatus(StatusKaryawan.JuniorStaff, "Testing");
            var tugas = AutomataStatus.GetTugas(StatusKaryawan.JuniorStaff);
            Assert.DoesNotContain("Testing", tugas);
        }

        [Fact]
        public void GetStatusDariTugas_ShouldReturnCorrectStatus()
        {
            AutomataStatus.TambahTugasUntukStatus(StatusKaryawan.SeniorStaff, "Deploy");
            var status = AutomataStatus.GetStatusDariTugas("Deploy");
            Assert.Equal(StatusKaryawan.SeniorStaff, status);
        }
    }
}
