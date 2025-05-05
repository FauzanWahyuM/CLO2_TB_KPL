using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_Kelompok_BisaYukk.Modules;
using Xunit;

namespace Tubes_Kelompok_BisaYukk.Testing
{
    public class TableDrivenJadwalTests
    {
        [Fact]
        public void SimpanJadwal_ShouldAddFormattedTask()
        {
            var data = new Dictionary<string, List<string>>();
            TableDrivenJadwal.SimpanJadwal("Andi", "Design", 5, data);
            Assert.Single(data["Andi"]);
            Assert.Contains("Tugas: Design, Durasi: 5 jam", data["Andi"][0]);
        }

        [Fact]
        public void CekTugasTersimpan_ShouldReturnTrueIfExists()
        {
            var data = new Dictionary<string, List<string>>();
            TableDrivenJadwal.SimpanJadwal("Budi", "Testing", 3, data);
            Assert.True(TableDrivenJadwal.CekTugasTersimpan("Budi", "Testing", data));
        }

        [Fact]
        public void GetTugasKaryawan_ShouldReturnUnfinishedTasks()
        {
            var data = new Dictionary<string, List<string>>
        {
            { "Citra", new List<string> { "Tugas: Coding, Durasi: 4 jam", "Tugas: Testing, Durasi: 2 jam (Selesai)" } }
        };
            var result = TableDrivenJadwal.GetTugasKaryawan("Citra", data);
            Assert.Single(result);
            Assert.Contains("Coding", result[0]);
        }

        [Fact]
        public void SelesaikanTugas_ShouldMarkTaskAsCompleted()
        {
            var tugas = "Tugas: Coding, Durasi: 4 jam";
            var data = new Dictionary<string, List<string>> { { "Dewi", new List<string> { tugas } } };
            TableDrivenJadwal.SelesaikanTugas("Dewi", tugas, data);
            Assert.Contains("[SELESAI]", data["Dewi"][0]);
        }
    }
}
