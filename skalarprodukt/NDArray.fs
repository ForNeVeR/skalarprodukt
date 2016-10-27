namespace skalarprodukt

type NDArray<'t, 'ndims> =
    {
        ndims :'ndims
        data : 't array
    }

module NDArray =

    let inline length (arr: NDArray<_, 'ndims>) = 
        (^ndims : (member length: int with get) arr.ndims)

    let inline ndims (arr: NDArray<_, 'ndims>) =
        (^ndims : (static member n: int with get) ())

    let inline sizes (arr: NDArray<_, 'ndims>) =
        (^ndims : (member sizes: ^S with get) arr.ndims)

    let inline indexer (arr:NDArray<_, 'ndims>) =
        let s = sizes arr
        fun ind ->
            (^ndims : (static member indexer : ^S * ^I -> int) s, ind)

    let inline eachindex (arr:NDArray<_, 'ndims>) =
        let s = sizes arr
        (^ndims : (static member eachindex : ^S -> seq< ^S >) s)

    let inline create size (value: 't) = 
        let ndims = (^ndims : (new : ^S -> ^ndims) size)
        let len = (^ndims : (member length: int with get) ndims)
        let data = Array.replicate len value
        { ndims = ndims; data = data }

    let inline zeroCreate size : NDArray<'t, 'ndims> =
        create size Unchecked.defaultof<'t>

    let inline get arr ind =
        let index = indexer arr
        arr.data.[index(ind)]

    let inline set arr ind value =
        let index = indexer arr
        arr.data.[index(ind)] <- value

    let inline map mapping array =
        { ndims = array.ndims; data = Array.map mapping array.data}