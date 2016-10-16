namespace skalarprodukt

type NDArray<'t, 'ndims> =
    {
        ndims :'ndims
        data : 't array
    }

module NDArray =

    let inline length arr = arr.data.Length    

    let inline ndims (arr: NDArray<_, 'ndims>) =
        (^ndims : (static member n : int with get) ())

    let inline size (arr: NDArray<_, 'ndims>) =
        (^ndims : (member size : ^R with get) arr.ndims)

    let inline indexer (arr:NDArray<_, 'ndims>) =
        let s = size arr
        fun ind ->
            (^ndims : (static member indexer : ^R * ^L -> int) s, ind)

    let inline create size (value: 't) = 
        let ndims = (^ndims : (new : ^R -> ^ndims) size)
        let data = Array.replicate 10 value
        { ndims = ndims; data = data }

    let inline get arr ind =
        let index = indexer arr
        arr.data.[index(ind)]

    let inline set arr ind value =
        let index = indexer arr
        arr.data.[index(ind)] <- value