// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open skalarprodukt
open skalarprodukt.Providers

open NDArray

open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running
open BenchmarkDotNet.Configs
open BenchmarkDotNet.Jobs

#if MONO
#else
open BenchmarkDotNet.Diagnostics.Windows
#endif

/// Configuration for a given benchmark
type ArrayPerfConfig () =
    inherit ManualConfig()
    do 
        base.Add Job.RyuJitX64
        base.Add Job.LegacyJitX86 // If you want to also test 32bit. It will run tests on both if both of these are here.
        #if MONO
        #else
        base.Add(new MemoryDiagnoser())  // To get GC and allocation data
        #endif

[<Config (typeof<ArrayPerfConfig>)>]
type GetSet () =

    let mutable arr1 : int [,] = Array2D.zeroCreate 1 1
    let mutable arr2 : Matrix<int> = NDArray.zeroCreate (1, 1)

    member val public N = 1000 with get, set

    [<Setup>]
    member self.SetupData () =
        arr1 <- Array2D.init self.N self.N (fun i j -> i + j)
        arr2 <- NDArray.init (self.N, self.N) (fun (i, j) -> i + j)

    [<Benchmark(Baseline=true)>]
    member self.Array2DGetSet () =
        let last = self.N-1
        for i in 0..last do
            for j in 0..last do
                let v = Array2D.get arr1 i j
                Array2D.set arr1 i j v

    [<Benchmark>]
    member self.NDArrayGetSet () =
        let indexr = NDArray.indexer arr2
        let data = arr2.data
        let last = self.N - 1
        for i in 0..last do
            for j in 0..last do
                let ind = indexr (i, j)
                let v = data.[ind]
                data.[ind] <- v


type MatrixMapComparison () =

    let mutable arr1 : int [,] = Array2D.zeroCreate 1 1
    let mutable arr2 : Matrix<int> = NDArray.zeroCreate (1, 1)    

    [<Params (1, 2, 32, 100)>]
    member val public M = 0 with get, set

    [<Setup>]
    member self.SetupData () =
        arr1 <- Array2D.init self.M self.M (fun i j -> i + j)
        arr2 <- NDArray.init (self.M, self.M) (fun (i, j) -> i + j)

    [<Benchmark>]
    member self.Array2DMap () =
        arr1 |> Array2D.map (fun v -> v*v)

    [<Benchmark>]
    member self.NDArrayMap () =
        arr2 |> NDArray.map (fun v -> v*v)

type MatrixMapiComparison () =

    let mutable arr1 : int [,] = Array2D.zeroCreate 1 1
    let mutable arr2 : Matrix<int> = NDArray.zeroCreate (1, 1)

    [<Params (1, 2, 32, 100)>]
    member val public M = 0 with get, set

    [<Setup>]
    member self.SetupData () =
        arr1 <- Array2D.init self.M self.M (fun i j -> i + j)
        arr2 <- NDArray.init (self.M, self.M) (fun (i, j) -> i + j)

    [<Benchmark>]
    member self.Array2DMapi () =
        arr1 |> Array2D.mapi (fun i j v -> (i - j)*v)

    [<Benchmark>]
    member self.NDArrayMapi () =
        arr2 |> NDArray.mapi (fun (i, j) v -> (i - j)*v)

let defaultSwitch () = BenchmarkSwitcher [| typeof<GetSet>; typeof<MatrixMapComparison>; typeof<MatrixMapiComparison>  |]

[<EntryPoint>]
let main argv =
    defaultSwitch().Run argv
    0
