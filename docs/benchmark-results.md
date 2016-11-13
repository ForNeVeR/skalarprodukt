Benchmark results
=================

We're measuring skalarprodukt 2D matrix type performance versus the F# standard
`Array2D` module and naive C# matrix implementation (based on 2D array).

GetSet
------

```
Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7 CPU 870 2.93GHz, ProcessorCount=8
Frequency=2873502 ticks, Resolution=348.0074 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=32-bit RELEASE
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=GetSet  Mode=Throughput
```

                    Method | Platform |       Jit |     Median |    StdDev | Scaled | Scaled-SD |    Gen 0 | Gen 1 | Gen 2 | Bytes Allocated/Op |
-------------------------- |--------- |---------- |----------- |---------- |------- |---------- |--------- |------ |------ |------------------- |
             Array2DGetSet |      X64 |    RyuJit |  7.2879 ms | 0.0867 ms |   1.00 |      0.00 |   417.48 |     - |  0.35 |       7,321,827.87 |
             NDArrayGetSet |      X64 |    RyuJit | 29.7625 ms | 0.7470 ms |   4.05 |      0.11 | 2,143.40 |     - |  0.93 |      37,361,354.27 |
 CSharpNaiveMatrix2DGetSet |      X64 |    RyuJit |  8.5565 ms | 0.0917 ms |   1.17 |      0.02 |   438.17 |     - |  0.24 |       7,684,691.02 |
             Array2DGetSet |      X86 | LegacyJit |  4.7885 ms | 0.0543 ms |   1.00 |      0.00 |   116.90 |     - |  0.12 |       2,152,232.39 |
             NDArrayGetSet |      X86 | LegacyJit | 62.3879 ms | 0.6826 ms |  13.01 |      0.20 | 2,024.00 |     - |  2.00 |      36,872,207.54 |
 CSharpNaiveMatrix2DGetSet |      X86 | LegacyJit |  7.6719 ms | 0.1127 ms |   1.61 |      0.03 |   231.79 |     - |  0.22 |       4,267,600.15 |

MatrixMapComparison
-------------------

```
Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7 CPU 870 2.93GHz, ProcessorCount=8
Frequency=2873500 ticks, Resolution=348.0077 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=32-bit RELEASE
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=MatrixMapComparison  Mode=Throughput
```

     Method |    M |             Median |            StdDev |
----------- |----- |------------------- |------------------ |
 Array2DMap |    1 |        460.4795 ns |         7.8193 ns |
 NDArrayMap |    1 |         16.5862 ns |         0.4396 ns |
 Array2DMap |    2 |        551.9338 ns |        17.7544 ns |
 NDArrayMap |    2 |         21.2846 ns |         0.7272 ns |
 Array2DMap |   32 |     25,667.1365 ns |       632.6966 ns |
 NDArrayMap |   32 |      1,704.7966 ns |        38.5505 ns |
 Array2DMap |  100 |    248,312.4687 ns |    11,890.3537 ns |
 NDArrayMap |  100 |     15,749.8741 ns |       335.9436 ns |
 Array2DMap | 1000 | 25,620,628.1538 ns | 1,006,538.0405 ns |
 NDArrayMap | 1000 |  2,739,240.3102 ns |    42,846.3636 ns |

MatrixMapiComparison
--------------------

```
Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7 CPU 870 2.93GHz, ProcessorCount=8
Frequency=2873500 ticks, Resolution=348.0077 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=32-bit RELEASE
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=MatrixMapiComparison  Mode=Throughput
```

      Method |    M |              Median |             StdDev |
------------ |----- |-------------------- |------------------- |
 Array2DMapi |    1 |         470.0634 ns |         10.8956 ns |
 NDArrayMapi |    1 |       1,951.6315 ns |         38.2321 ns |
 Array2DMapi |    2 |         549.7645 ns |         22.2139 ns |
 NDArrayMapi |    2 |       5,030.7593 ns |         72.5182 ns |
 Array2DMapi |   32 |      29,089.7782 ns |        998.9287 ns |
 NDArrayMapi |   32 |     435,357.9177 ns |     15,355.9600 ns |
 Array2DMapi |  100 |     278,707.0632 ns |      5,931.9032 ns |
 NDArrayMapi |  100 |   3,922,312.7284 ns |    114,022.7973 ns |
 Array2DMapi | 1000 |  28,081,194.5363 ns |    828,435.1225 ns |
 NDArrayMapi | 1000 | 393,107,577.8663 ns | 10,661,166.4661 ns |
