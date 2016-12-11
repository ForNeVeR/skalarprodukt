```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7 CPU 870 2.93GHz, ProcessorCount=8
Frequency=2873503 ticks, Resolution=348.0073 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=32-bit RELEASE
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=MatrixMapiComparison  Mode=Throughput  

```
                      Method |   M |            Median |         StdDev |
---------------------------- |---- |------------------ |--------------- |
                 **Array2DMapi** |   **1** |       **497.9675 ns** |     **13.6553 ns** |
                 NDArrayMapi |   1 |     1,883.7018 ns |     54.6214 ns |
     CSharpNaiveMatrix2DMapi |   1 |       186.0601 ns |      3.1789 ns |
 CSharpOptimizedMatrix2DMapi |   1 |        43.9420 ns |      1.3979 ns |
                 **Array2DMapi** |   **2** |       **577.5659 ns** |     **17.3601 ns** |
                 NDArrayMapi |   2 |     5,813.9417 ns |    357.2733 ns |
     CSharpNaiveMatrix2DMapi |   2 |       234.1899 ns |     11.6817 ns |
 CSharpOptimizedMatrix2DMapi |   2 |        65.3425 ns |      3.8699 ns |
                 **Array2DMapi** |  **32** |    **29,203.9377 ns** |    **990.7337 ns** |
                 NDArrayMapi |  32 |   492,402.7915 ns | 11,292.1510 ns |
     CSharpNaiveMatrix2DMapi |  32 |    12,227.7494 ns |    625.1434 ns |
 CSharpOptimizedMatrix2DMapi |  32 |     7,489.3096 ns |    241.3284 ns |
                 **Array2DMapi** | **100** |   **283,935.2080 ns** |  **4,615.1845 ns** |
                 NDArrayMapi | 100 | 4,458,400.2740 ns | 63,624.1472 ns |
     CSharpNaiveMatrix2DMapi | 100 |   117,404.4574 ns |  2,469.5666 ns |
 CSharpOptimizedMatrix2DMapi | 100 |    70,907.2506 ns |  1,046.6860 ns |
