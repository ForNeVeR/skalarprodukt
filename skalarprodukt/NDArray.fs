namespace skalarprodukt

module NDArray =

    type NDArray<'t, 'ndims> =
        {
            dims : int array
            data : 't array
        }

    let inline length arr = arr.data.Length

    let inline ndims (arr:NDArray<_, 'ndims>) =
        (^ndims : (static member n : int with get) ())

    let inline indexer (arr:NDArray<_, 'ndims>) =
        (^ndims : (static member indexer : int array -> _) arr.dims)

    let inline indices (arr:NDArray<_, 'ndims>) = 
        (^ndims : (static member indices : int array -> _) arr.dims)

    let inline get arr ind =
        let index = indexer arr
        arr.data.[index(ind)]

    let inline set arr ind value =
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