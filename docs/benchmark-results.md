Benchmark results
=================

We're measuring skalarprodukt 2D matrix type performance versus the F# standard
`Array2D` module.

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
