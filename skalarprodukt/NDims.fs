namespace skalarprodukt

module NDims =

    open System
    open NDArray    

    type NDims1 =
        static member ndims with get() = 1
        static member indexer (dims: int array) = 
            let indexImpl (ind:int array) = ind.[0]
            indexImpl

    type NDims2 =
        static member ndims with get() = 2
        static member indexer (dims: int array) = 
            let indexImpl (ind:int array) = ind.[0] + ind.[1]*dims.[1]
            indexImpl

