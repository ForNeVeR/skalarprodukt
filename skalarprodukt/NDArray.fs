namespace skalarprodukt
open Impl

type NDArray<'t, 'impl> =
    {
        impl :'impl
        data : 't array
    } 

type Vector<'t> = NDArray<'t, DenseVectorImpl>
type Matrix<'t> = NDArray<'t, DenseMatrixImpl>

module NDArray =

    open FSharp.Core
    open System.Runtime.CompilerServices      

    let inline length (arr: NDArray<_, 'impl>) = 
        (^impl : (member length: int with get) arr.impl)

    let inline ndims (arr: NDArray<_, 'impl>) =
        (^impl : (static member n: int with get) ())

    let inline sizes (arr: NDArray<_, 'impl>) =
        (^impl : (member sizes: ^s with get) arr.impl)

    let inline sub2ind (arr:NDArray<_, 'impl>) sub =
        (^impl : (member sub2ind : ^sub -> int) (arr.impl, sub))

    let inline eachindex (arr:NDArray<_, 'impl>) =
        (^impl : (member eachindex : seq< ^sub > with get) arr.impl)

    let inline create sizes (value: 't) = 
        let impl = (^impl : (new : ^s -> ^impl) sizes)
        let len = (^impl : (member length: int with get) impl)
        let data = Array.replicate len value
        { impl = impl; data = data }

    let inline zeroCreate sizes : NDArray<'t, 'impl> =
        create sizes Unchecked.defaultof<'t>
    
    let inline get arr sub =
        let i = sub2ind arr sub
        arr.data.[i]

    let inline set arr sub value =
        let i = sub2ind arr sub
        arr.data.[i] <- value

    let inline init (sizes: 's) (initializer: 'sub -> 't) =
        let (arr : NDArray<'t, 'impl>) = zeroCreate sizes
        let indices = eachindex arr
        for i in indices do
            let v = initializer i
            set arr i v
        arr

    let inline map mapping array =
        { impl = array.impl; data = Array.map mapping array.data}

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