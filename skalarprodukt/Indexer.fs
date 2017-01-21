namespace skalarprodukt

module Indexer =        

    [<Struct>]
    type DenseVectorIndexer(len:int) =
        static member val ndims = 1

        member this.length = len
        
        member this.sizes = len
        
        member this.sub2ind i = i

        member this.eachindex = 
            let len = this.length
            seq {0 .. (len - 1)}

     [<Struct>]
     type DenseMatrixIndexer(s:int*int) =
        static member val ndims = 2

        member this.length1
            with get () = fst this.sizes

        member this.length2
            with get () = snd this.sizes

        member this.length = this.length1*this.length2

        member this.sizes = s
        
        member this.sub2ind sub = 
            let i = fst sub
            let j = snd sub
            let m = this.length1
            i + m*j

        member this.eachindex = 
            let length1 = this.length1
            let length2 = this.length2
            seq {
                for i in 0 .. length1 - 1 do
                    for j in 0 .. length2 - 1 do
                        yield (i, j) }
        