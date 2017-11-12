```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=AMD A6-3400M APU with Radeon(tm) HD Graphics, ProcessorCount=4
Frequency=14318180 ticks, Resolution=69.8413 ns, Timer=HPET
CLR=MS.NET 4.0.30319.42000, Arch=32-bit RELEASE
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1590.0

Type=GetSet  Mode=Throughput  

```

|                        Method | Platform |       Jit |     Median |    StdDev | Scaled | Scaled-SD |    Gen 0 | Gen 1 | Gen 2 | Bytes Allocated/Op |
|------------------------------ |--------- |---------- |----------- |---------- |------- |---------- |--------- |------ |------ |------------------- |
|                 Array2DGetSet |      X64 |    RyuJit | 11.9218 ms | 0.1570 ms |   1.00 |      0.00 |   453.79 |     - |  0.43 |       1 228 638,25 |
|                 NDArrayGetSet |      X64 |    RyuJit | 28.4794 ms | 0.3868 ms |   2.40 |      0.04 | 7,691.62 |     - |  1.84 |      13 503 786,82 |
|     CSharpNaiveMatrix2DGetSet |      X64 |    RyuJit | 14.9240 ms | 0.1250 ms |   1.25 |      0.02 |   894.97 |     - |  0.83 |       2 423 170,74 |
| CSharpOptimizedMatrix2DGetSet |      X64 |    RyuJit |  9.2916 ms | 0.1114 ms |   0.78 |      0.01 |   448.95 |     - |  0.46 |       1 215 564,94 |
|           NDArrayStructGetSet |      X64 |    RyuJit |  9.5377 ms | 0.1090 ms |   0.80 |      0.01 |   448.95 |     - |  0.46 |       1 215 564,96 |
|                 Array2DGetSet |      X86 | LegacyJit |  8.9082 ms | 0.0953 ms |   1.00 |      0.00 |   292.39 |     - |  0.47 |         989 565,53 |
|                 NDArrayGetSet |      X86 | LegacyJit | 33.2888 ms | 0.5474 ms |   3.74 |      0.07 | 5,443.00 |     - |  2.00 |      10 156 994,92 |
|     CSharpNaiveMatrix2DGetSet |      X86 | LegacyJit | 13.8509 ms | 0.2402 ms |   1.55 |      0.03 |   574.07 |     - |  0.93 |       1 942 864,47 |
| CSharpOptimizedMatrix2DGetSet |      X86 | LegacyJit | 10.5811 ms | 0.1196 ms |   1.19 |      0.02 |   294.88 |     - |  0.45 |         998 066,29 |
|           NDArrayStructGetSet |      X86 | LegacyJit | 17.7891 ms | 0.3114 ms |   2.00 |      0.04 |   565.53 |     - |  1.02 |       1 913 655,13 |
