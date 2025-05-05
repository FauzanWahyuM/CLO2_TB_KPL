using System;
using System.Collections.Generic;
using Tubes_Kelompok_BisaYukk.Modules;
using Xunit;

namespace Tubes_Kelompok_BisaYukk.Testing
{
    public class AutomataPemesananTests
    {
        [Fact]
        public void TambahTugas_ShouldAddNewTugas()
        {
            var dataKaryawan = new Dictionary<string, List<string>>();
            var result = AutomataPemesanan.TambahTugas("Laporan", dataKaryawan);
            Assert.True(result);
            Assert.True(AutomataPemesanan.CekTugasTersedia("Laporan"));
        }

        [Fact]
        public void AmbilTugas_ShouldReturnTrueAndSetTugasToFalse()
        {
            var dataKaryawan = new Dictionary<string, List<string>>();
            AutomataPemesanan.TambahTugas("Coding", dataKaryawan);
            var result = AutomataPemesanan.AmbilTugas("Coding");
            Assert.True(result);
            Assert.False(AutomataPemesanan.CekTugasTersedia("Coding"));
        }

        [Fact]
        public void HapusTugas_ShouldRemoveTugas()
        {
            var dataKaryawan = new Dictionary<string, List<string>>();
            AutomataPemesanan.TambahTugas("Testing", dataKaryawan);
            var result = AutomataPemesanan.HapusTugas("Testing");
            Assert.True(result);
        }
    }
}
