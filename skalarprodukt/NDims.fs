namespace skalarprodukt

module NDims =

    open NDArray    
    
    type NDims1 =
        static member ndims with get() = 1
        static member indexer (arr:NDArray<_, NDims1>) = 
            let indexImpl (i:int) = i
            indexImpl
        static member indices (arr:NDArray<_, NDims1>) =
            let l1 = arr.dims.[0]
            seq { for i in 0 .. l1 - 1 do yield i } 

    type NDims2 =
        static member ndims with get() = 2
        static member indexer (arr:NDArray<_, NDims2>) = 
            let indexImpl (i:int, j:int) = i + j*arr.dims.[1]
            indexImpl
        static member indices (arr:NDArray<_, NDims2>) =
            let l1 = arr.dims.[0]
            let l2 = arr.dims.[1]
            seq { for i in 0 .. l1 - 1 do
                    for j in 0 .. l2 - 1 do
                        yield (i, j)  } 

