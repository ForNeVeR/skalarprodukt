namespace skalarprodukt

module NDims =

    open System
    open NDArray    

    type NDims1 =
        static member ndims with get() = 1
        static member indexer (dims: int array) = 
            let indexImpl (ind:int array) = ind.[0]
            indexImpl
        static member indices (dims: int array) =
            let l1 = dims.[0]
            seq { for i in 0 .. l1 - 1 do yield [|i|] } 

    type NDims2 =
        static member ndims with get() = 2
        static member indexer (dims: int array) = 
            let indexImpl (ind:int array) = ind.[0] + ind.[1]*dims.[1]
            indexImpl
        static member indices (dims: int array) =
            let l1 = dims.[0]
            let l2 = dims.[1]
            seq { for i in 0 .. l1 - 1 do
                    for j in 0 .. l2 - 1 do
                        yield [|i; j|] } 

