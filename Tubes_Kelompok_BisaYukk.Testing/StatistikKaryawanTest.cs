using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_Kelompok_BisaYukk.Modules;
using Xunit;

namespace Tubes_Kelompok_BisaYukk.Testing
{
    public class StatistikKaryawanTests
    {
        [Fact]
        public void TampilkanLaporan_ShouldPrintCorrectStatistics()
        {
            // Arrange
            var data = new Dictionary<string, List<string>>
        {
            { "Andi", new List<string> { "Tugas A", "Tugas B (Selesai)", "Tugas C (Selesai)" } }
        };

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            StatistikKaryawan.TampilkanLaporan(data);
            var result = output.ToString();

            // Assert
            Assert.Contains("Karyawan: Andi", result);
            Assert.Contains("Total Tugas       : 3", result);
            Assert.Contains("Tugas Selesai     : 2", result);
            Assert.Contains("Masih Dalam Proses: 1", result);
        }
    }
}
