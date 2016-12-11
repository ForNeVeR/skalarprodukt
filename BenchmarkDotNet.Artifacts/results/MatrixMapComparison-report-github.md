```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7 CPU 870 2.93GHz, ProcessorCount=8
Frequency=2873503 ticks, Resolution=348.0073 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=32-bit RELEASE
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=MatrixMapComparison  Mode=Throughput  

```
                     Method |   M |          Median |        StdDev |
--------------------------- |---- |---------------- |-------------- |
                 **Array2DMap** |   **1** |     **473.4305 ns** |    **19.7540 ns** |
                 NDArrayMap |   1 |      16.9355 ns |     0.7611 ns |
     CSharpNaiveMatrix2DMap |   1 |     185.4349 ns |     5.8411 ns |
 CSharpOptimizedMatrix2DMap |   1 |      43.4014 ns |     1.2932 ns |
                 **Array2DMap** |   **2** |     **535.3380 ns** |    **11.7636 ns** |
                 NDArrayMap |   2 |      21.2091 ns |     0.9070 ns |
     CSharpNaiveMatrix2DMap |   2 |     219.3923 ns |     4.2583 ns |
 CSharpOptimizedMatrix2DMap |   2 |      63.9361 ns |     1.7548 ns |
                 **Array2DMap** |  **32** |  **26,447.7471 ns** |   **704.0641 ns** |
                 NDArrayMap |  32 |   1,696.4141 ns |    34.3238 ns |
     CSharpNaiveMatrix2DMap |  32 |  11,060.5581 ns |   179.7705 ns |
 CSharpOptimizedMatrix2DMap |  32 |   6,300.4316 ns |   261.5000 ns |
                 **Array2DMap** | **100** | **261,446.0863 ns** | **4,001.3596 ns** |
                 NDArrayMap | 100 |  16,216.4145 ns |   656.5581 ns |
     CSharpNaiveMatrix2DMap | 100 | 108,744.7173 ns | 1,830.1916 ns |
 CSharpOptimizedMatrix2DMap | 100 |  59,229.7197 ns | 1,493.3286 ns |
