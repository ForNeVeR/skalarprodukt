// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open skalarprodukt
open skalarprodukt.Providers

open NDArray

open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running

type MatrixMapComparison () =

    let mutable arr1 : int [,] = Array2D.zeroCreate 1 1
    let mutable arr2 : Matrix<int> = NDArray.zeroCreate (1, 1)

    [<Params (2, 32)>]
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

    [<Params (2, 32)>]
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

let defaultSwitch () = BenchmarkSwitcher [| typeof<MatrixMapComparison>; typeof<MatrixMapiComparison>  |]

[<EntryPoint>]
let main argv = 
    defaultSwitch().Run argv 
    0
