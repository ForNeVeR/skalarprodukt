namespace skalarprodukt

module NDArray =

    type NDArray<'t, 'ndims> =
        {
            dims : int array
            data : 't array
        }

    let inline length (arr:NDArray<_, 'ndims>) = arr.data.Length

    let inline ndims (arr:NDArray<_, 'ndims>) =
        (^ndims : (static member ndims : int with get) ())

    let inline indexer (arr:NDArray<_, 'ndims>) =
        (^ndims : (static member indexer : NDArray<_, ^ndims> -> _) arr)

    let inline indices (arr:NDArray<_, 'ndims>) = 
        (^ndims : (static member indices : NDArray<_, ^ndims> -> _) arr)

    let inline get (arr:NDArray<_, 'ndims>) ind =
        let index = indexer arr
        arr.data.[index(ind)]

    let inline set (arr:NDArray<_, 'ndims>) ind value =
        let index = indexer arr
        arr.data.[index(ind)] <- value

    let inline map (f: 't -> 'u) (arr:NDArray<'t, 'ndims>) =
        let res:NDArray<'u, 'ndims>  = 
            {
                dims = arr.dims.Clone() :?> int array
                data = Array.map f arr.data
            }
        res

    let inline mapi (f: 'i -> 't -> 'u) (arr:NDArray<'t, 'ndims>) =
        let res:NDArray<'u, 'ndims> =
            {
                dims = arr.dims.Clone() :?> int array
                data = Array.zeroCreate<'u> arr.data.Length
            }
        let index = indexer arr
        for ind in (indices arr) do 
            let i = index ind
            res.data.[i] <- f ind arr.data.[i]
        res