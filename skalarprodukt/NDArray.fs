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

    let inline sub2ind (arr:NDArray<_, 'ndims>) sub =
        (^ndims : (member sub2ind : ^sub -> int) (arr.ndims, sub))

    let inline eachindex (arr:NDArray<_, 'ndims>) =
        let s = sizes arr
        (^ndims : (static member eachindex : ^s -> seq< ^sub >) s)

    let inline create sizes (value: 't) = 
        let ndims = (^ndims : (new : ^s -> ^ndims) sizes)
        let len = (^ndims : (member length: int with get) ndims)
        let data = Array.replicate len value
        { ndims = ndims; data = data }

    let inline zeroCreate sizes : NDArray<'t, 'ndims> =
        create sizes Unchecked.defaultof<'t>

    let inline flip f a b = f b a
    
    let inline get arr sub =
        let i = sub2ind arr sub
        arr.data.[i]

    let inline set arr sub value =
        let i = sub2ind arr sub
        arr.data.[i] <- value

    let inline init (sizes: 's) (initializer: 'sub -> 't) =
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
        let indexr = sub2ind array
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