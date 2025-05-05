using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_Kelompok_BisaYukk.Modules;
using Xunit;

namespace Tubes_Kelompok_BisaYukk.Testing
{
    public class PrioritasTugasTests
    {
        [Fact]
        public void GetPrioritas_TugasYangAda_ReturnPrioritas()
        {
            // Arrange
            string tugas = "Entry Task 1";

            // Act
            string result = PrioritasTugas.GetPrioritas(tugas);

            // Assert
            Assert.Equal("Rendah", result);
        }

        [Fact]
        public void AturPrioritas_TugasBaru_PrioritasBerubah()
        {
            // Arrange
            string tugas = "Tugas Baru";
            string prioritas = "Tinggi";

            // Act
            PrioritasTugas.AturPrioritas(tugas, prioritas);
            string result = PrioritasTugas.GetPrioritas(tugas);

            // Assert
            Assert.Equal(prioritas, result);
        }
    }
}
