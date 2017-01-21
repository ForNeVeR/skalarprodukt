namespace skalarprodukt
open Indexer

type NDArray<'t, 'indexer> =
    {
        indexer :'indexer
        data : 't array
    } 

type Vector<'t> = NDArray<'t, DenseMatrixIndexer>
type Matrix<'t> = NDArray<'t, DenseMatrixIndexer>

module NDArray =

    open FSharp.Core
    open System.Runtime.CompilerServices      

    let inline length (arr: NDArray<_, 'indexer>) = 
        (^indexer : (member length: int with get) arr.indexer)

    let inline ndims (arr: NDArray<_, 'indexer>) =
        (^indexer : (static member n: int with get) ())

    let inline sizes (arr: NDArray<_, 'indexer>) =
        (^indexer : (member sizes: ^s with get) arr.indexer)

    let inline sub2ind (arr:NDArray<_, 'indexer>) sub =
        (^indexer : (member sub2ind : ^sub -> int) (arr.indexer, sub))

    let inline eachindex (arr:NDArray<_, 'indexer>) =
        (^indexer : (member eachindex : seq< ^sub > with get) arr.indexer)

    let inline create sizes (value: 't) = 
        let indexer = (^indexer : (new : ^s -> ^indexer) sizes)
        let len = (^indexer : (member length: int with get) indexer)
        let data = Array.replicate len value
        { indexer = indexer; data = data }

    let inline zeroCreate sizes : NDArray<'t, 'indexer> =
        create sizes Unchecked.defaultof<'t>
    
    let inline get arr sub =
        let i = sub2ind arr sub
        arr.data.[i]

    let inline set arr sub value =
        let i = sub2ind arr sub
        arr.data.[i] <- value

    let inline init (sizes: 's) (initializer: 'sub -> 't) =
        let (arr : NDArray<'t, 'indexer>) = zeroCreate sizes
        let indices = eachindex arr
        for i in indices do
            let v = initializer i
            set arr i v
        arr

    let inline map mapping array =
        { indexer = array.indexer; data = Array.map mapping array.data}

    let inline mapi (mapping : 's -> 't -> 'u) (array: NDArray<'t, 'indexer>) : NDArray<'u, 'indexer> =
        let f = OptimizedClosures.FSharpFunc<_,_,_>.Adapt(mapping)
        let s = sizes array
        let indexr = sub2ind array
        let (res : NDArray<'u, 'indexer>) = init s (fun i -> f.Invoke(i, (get array i)))
        res

    let inline iter action array =
        Array.iter action array.data

    let inline iteri (action : 's -> 't -> unit) (array : NDArray<'t, 'indexer>) =
        let indices = eachindex array
        for i in indices do
            let v = get array i
            action i v

    [<Extension>]
    type NDArrayExt () =
        [<Extension>]
        static member inline Item (arr, ind) = 
            get arr ind