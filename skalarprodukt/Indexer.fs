namespace skalarprodukt

module Indexer =        

    [<Struct>]
    type NDimsVec(len:int) =
        static member ndims = 1

        member this.length = len
        
        member this.sizes = len
        
        member this.sub2ind i = i

        static member eachindex len = seq {0 .. (len - 1)}

     [<Struct>]
     type NDimsMat(sizes_:int*int) =
        static member ndims = 2

        member this.length1
            with get () = fst this.sizes

        member this.length2
            with get () = snd this.sizes

        member this.length = this.length1*this.length2

        member this.sizes = sizes_
        
        member this.sub2ind (sub:int*int) = 
            let i = fst sub
            let j = snd sub
            let m = this.length1
            i + m*j

        static member eachindex sizes = 
            let length1 = fst sizes
            let length2 = snd sizes
            seq {
                for i in 0 .. length1 - 1 do
                    for j in 0 .. length2 - 1 do
                        yield (i, j) }
        