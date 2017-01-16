namespace skalarprodukt
open Indexer

type NDArray<'t, 'ndims> =
    {
        ndims :'ndims
        data : 't array
    } 

type Vector<'t> = NDArray<'t, NDimsVec>
type Matrix<'t> = NDArray<'t, NDimsMat>

module NDArray =

    open FSharp.Core
    open System.Runtime.CompilerServices      

    let inline length (arr: NDArray<_, 'ndims>) = 
        (^ndims : (member length: int with get) arr.ndims)

    let inline ndims (arr: NDArray<_, 'ndims>) =
        (^ndims : (static member n: int with get) ())

    let inline sizes (arr: NDArray<_, 'ndims>) =
        (^ndims : (member sizes: ^s with get) arr.ndims)

    let inline indexer (arr:NDArray<_, 'ndims>) =
        fun ind ->
            (^ndims : (member indexer : ^i -> int) arr.ndims, ind)

    let inline eachindex (arr:NDArray<_, 'ndims>) =
        let s = sizes arr
        (^ndims : (static member eachindex : ^S -> seq< ^S >) s)

    let inline create sizes (value: 't) = 
        let ndims = (^ndims : (new : ^S -> ^ndims) sizes)
        let len = (^ndims : (member length: int with get) ndims)
        let data = Array.replicate len value
        { ndims = ndims; data = data }

    let inline zeroCreate sizes : NDArray<'t, 'ndims> =
        create sizes Unchecked.defaultof<'t>

    let inline get arr ind =
        let index = indexer arr
        arr.data.[index(ind)]

    let inline set arr ind value =
        let index = indexer arr
        arr.data.[index(ind)] <- value

    let inline init (sizes: 's) (initializer: 's -> 't) =
        let (arr : NDArray<'t, 'ndims>) = zeroCreate sizes
        let indices = eachindex arr
        for i in indices do
            let v = initializer i
            set arr i v
        arr

    let inline map mapping array =
        { ndims = array.ndims; data = Array.map mapping array.data}

    let inline mapi (mapping : 's -> 't -> 'u) (array: NDArray<'t, 'ndims>) : NDArray<'u, 'ndims> =
        let f = OptimizedClosures.FSharpFunc<_,_,_>.Adapt(mapping)
        let s = sizes array
        let indexr = indexer array
        let (res : NDArray<'u, 'ndims>) = init s (fun i -> f.Invoke(i, (get array i)))
        res

    let inline iter action array =
        Array.iter action array.data

    let inline iteri (action : 's -> 't -> unit) (array : NDArray<'t, 'ndims>) =
        let indices = eachindex array
        for i in indices do
            let v = get array i
            action i v

    [<Extension>]
    type NDArrayExt () =
        [<Extension>]
        static member inline Item (arr, ind) = 
            get arr ind