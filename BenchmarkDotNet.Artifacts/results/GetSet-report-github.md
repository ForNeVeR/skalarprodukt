```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7 CPU 870 2.93GHz, ProcessorCount=8
Frequency=2873503 ticks, Resolution=348.0073 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=32-bit RELEASE
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=GetSet  Mode=Throughput  

```
                        Method | Platform |       Jit |     Median |    StdDev | Scaled | Scaled-SD |    Gen 0 | Gen 1 | Gen 2 | Bytes Allocated/Op |
------------------------------ |--------- |---------- |----------- |---------- |------- |---------- |--------- |------ |------ |------------------- |
                 Array2DGetSet |      X64 |    RyuJit |  7.2936 ms | 0.1205 ms |   1.00 |      0.00 |   313.86 |     - |  0.16 |       7,820,500.56 |
                 NDArrayGetSet |      X64 |    RyuJit | 30.1818 ms | 2.4451 ms |   4.21 |      0.34 | 1,331.92 |     - |  0.42 |      32,732,629.56 |
     CSharpNaiveMatrix2DGetSet |      X64 |    RyuJit |  8.7819 ms | 0.1980 ms |   1.20 |      0.03 |   308.29 |     - |  0.19 |       7,681,848.87 |
 CSharpOptimizedMatrix2DGetSet |      X64 |    RyuJit | 16.6018 ms | 0.2725 ms |   2.26 |      0.05 |   596.74 |     - |  0.43 |      14,869,078.84 |
                 Array2DGetSet |      X86 | LegacyJit |  4.8216 ms | 0.1093 ms |   1.00 |      0.00 |    79.95 |     - |  0.13 |       2,115,551.24 |
                 NDArrayGetSet |      X86 | LegacyJit | 65.6375 ms | 3.6576 ms |  13.56 |      0.79 | 1,448.00 |     - |  2.00 |      37,589,085.84 |
     CSharpNaiveMatrix2DGetSet |      X86 | LegacyJit |  7.6370 ms | 0.1248 ms |   1.58 |      0.04 |   164.50 |     - |  0.17 |       4,354,222.76 |
 CSharpOptimizedMatrix2DGetSet |      X86 | LegacyJit | 16.3131 ms | 0.5885 ms |   3.35 |      0.14 |   322.64 |     - |  0.35 |       8,539,793.71 |
