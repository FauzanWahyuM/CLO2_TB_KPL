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
    public class TableDrivenBatasTests
    {
        [Theory]
        [InlineData(StatusKaryawan.Intern, 2, 6)]
        [InlineData(StatusKaryawan.JuniorStaff, 4, 8)]
        [InlineData(StatusKaryawan.SeniorStaff, 6, 10)]
        public void GetBatasWaktu_ShouldReturnCorrectTuple(StatusKaryawan status, int min, int max)
        {
            var batas = TableDrivenBatas.GetBatasWaktu(status);
            Assert.Equal(min, batas.Item1);
            Assert.Equal(max, batas.Item2);
        }
    }
}
