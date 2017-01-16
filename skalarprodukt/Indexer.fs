namespace skalarprodukt

module Indexer =

    type NDimsVec(length) =
        static member ndims = 1

        member this.length = length
        
        member this.sizes = length
        
        member this.indexer i = i

        static member eachindex length = seq {0 .. length}

     type NDimsMat(length1, length2) =
        static member ndims = 2

        member this.length1
            with get () = fst this.sizes

        member this.length2
            with get () = snd this.sizes

        member this.length = this.length1*this.length2

        member this.sizes = (length1, length2)
        
        member this.indexer i = i

        static member eachindex sizes = 
            let length1 = fst sizes
            let length2 = snd sizes
            seq {
                for i in 0 .. length1 do
                    for j in 0 .. length2 do
                        yield i + length1*j }
        