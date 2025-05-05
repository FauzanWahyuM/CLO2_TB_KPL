```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3775)
12th Gen Intel Core i5-12450HX, 1 CPU, 12 logical and 8 physical cores
  [Host]     : .NET Framework 4.8.1 (4.8.9300.0), X86 LegacyJIT [AttachedDebugger]
  Job-FTOBUC : .NET Framework 4.8.1 (4.8.9300.0), X86 LegacyJIT

InvocationCount=1  UnrollFactor=1  

```
| Method                 | Mean       | Error     | StdDev     | Median     | Allocated |
|----------------------- |-----------:|----------:|-----------:|-----------:|----------:|
| TambahTugas            | 2,741.7 ns | 437.36 ns | 1,261.9 ns | 2,200.0 ns |         - |
| AmbilTugas             | 1,415.3 ns | 280.25 ns |   817.5 ns | 1,200.0 ns |         - |
| CekTugasTersedia       |   438.5 ns |  95.73 ns |   276.2 ns |   300.0 ns |         - |
| HapusTugas             | 1,021.9 ns | 176.67 ns |   509.7 ns |   900.0 ns |         - |
| GetSemuaTugas          | 1,225.0 ns | 244.92 ns |   706.7 ns | 1,000.0 ns |         - |
| TambahTugasUntukStatus | 3,217.9 ns | 386.67 ns | 1,109.4 ns | 2,900.0 ns |         - |
| HapusTugasDariStatus   |   703.1 ns | 171.01 ns |   496.1 ns |   500.0 ns |         - |
| GetTugasDariStatus     |   845.7 ns | 192.30 ns |   548.6 ns |   600.0 ns |         - |
| GetStatusDariTugas     | 5,081.3 ns | 700.50 ns | 2,021.1 ns | 4,700.0 ns |         - |
| SimpanJadwal           | 2,173.2 ns | 416.24 ns | 1,194.3 ns | 1,750.0 ns |         - |
| GetTugasKaryawan       | 2,220.2 ns | 260.13 ns |   720.8 ns | 2,000.0 ns |         - |
| SelesaikanTugas        | 3,950.0 ns | 500.48 ns | 1,459.9 ns | 3,700.0 ns |         - |
