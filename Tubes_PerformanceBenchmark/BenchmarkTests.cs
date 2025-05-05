using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_Kelompok_BisaYukk.Models;
using Tubes_Kelompok_BisaYukk.Modules;

namespace Tubes_PerformanceBenchmark
{
    [MemoryDiagnoser]
    public class BenchmarkTests
    {
        private Dictionary<string, List<string>> dataKaryawan;

        [GlobalSetup]
        public void Setup()
        {
            dataKaryawan = new Dictionary<string, List<string>>();
            AutomataPemesanan.TambahTugas("TugasBenchmark", dataKaryawan);
            AutomataStatus.TambahTugasUntukStatus(StatusKaryawan.JuniorStaff, "TugasBenchmark");
            TableDrivenJadwal.SimpanJadwal("karyawan1", "TugasBenchmark", 5, dataKaryawan);
        }

        [IterationCleanup]
        public void Cleanup()
        {
            // Reset all test data between iterations
            dataKaryawan.Clear();
            AutomataPemesanan.HapusTugas("TugasAmbil");
            AutomataPemesanan.HapusTugas("TugasHapus");
            AutomataStatus.HapusTugasDariStatus(StatusKaryawan.SeniorStaff, "TugasStatusHapus");
        }

        // --- AutomataPemesanan ---

        [Benchmark]
        public void TambahTugas()
        {
            AutomataPemesanan.TambahTugas("TugasBaru" + Guid.NewGuid(), dataKaryawan);
        }

        [Benchmark]
        public void AmbilTugas()
        {
            AutomataPemesanan.TambahTugas("TugasAmbil", dataKaryawan);
            AutomataPemesanan.AmbilTugas("TugasAmbil");
        }

        [Benchmark]
        public void CekTugasTersedia()
        {
            AutomataPemesanan.CekTugasTersedia("TugasBenchmark");
        }

        [Benchmark]
        public void HapusTugas()
        {
            AutomataPemesanan.TambahTugas("TugasHapus", dataKaryawan);
            AutomataPemesanan.HapusTugas("TugasHapus");
        }

        [Benchmark]
        public void GetSemuaTugas()
        {
            var semua = AutomataPemesanan.GetSemuaTugas();
        }

        // --- AutomataStatus ---

        [Benchmark]
        public void TambahTugasUntukStatus()
        {
            AutomataStatus.TambahTugasUntukStatus(StatusKaryawan.Intern, "TugasStatus" + Guid.NewGuid());
        }

        [Benchmark]
        public void HapusTugasDariStatus()
        {
            string tugas = "TugasStatusHapus";
            AutomataStatus.TambahTugasUntukStatus(StatusKaryawan.SeniorStaff, tugas);
            AutomataStatus.HapusTugasDariStatus(StatusKaryawan.SeniorStaff, tugas);
        }

        [Benchmark]
        public void GetTugasDariStatus()
        {
            AutomataStatus.GetTugas(StatusKaryawan.JuniorStaff);
        }

        [Benchmark]
        public void GetStatusDariTugas()
        {
            AutomataStatus.GetStatusDariTugas("TugasBenchmark");
        }

        // --- TableDrivenJadwal ---

        [Benchmark]
        public void SimpanJadwal()
        {
            TableDrivenJadwal.SimpanJadwal("karyawan2", "TugasY", 3, dataKaryawan);
        }

        [Benchmark]
        public void GetTugasKaryawan()
        {
            TableDrivenJadwal.SimpanJadwal("karyawan1", "TugasDummy", 4, dataKaryawan);
            TableDrivenJadwal.GetTugasKaryawan("karyawan1", dataKaryawan);
        }

        [Benchmark]
        public void SelesaikanTugas()
        {
            string tugas = "TugasSelesai";
            TableDrivenJadwal.SimpanJadwal("karyawan3", tugas, 4, dataKaryawan);
            TableDrivenJadwal.SelesaikanTugas("karyawan3", $"Tugas: {tugas}, Durasi: 4 jam", dataKaryawan);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchmarkTests>();
        }
    }
}
