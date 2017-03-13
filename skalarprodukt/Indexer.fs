namespace skalarprodukt

module Indexer =        

    type DenseVectorIndexer =
        struct
            val length1: int
            new (l1) = {length1 = l1}

            static member ndims = 2

            member this.length = this.length1

            member this.sizes = this.length1

            member this.sub2ind i = i

            member this.eachindex = 
                let length1 = this.length1
                seq { for i in 0 .. length1 - 1 do yield i }
        end

     type DenseMatrixIndexer =
        struct
            val length1: int
            val length2: int
            new ((l1, l2)) = {length1 = l1; length2 = l2}

            static member ndims = 2

            member this.length = this.length1 * this.length2

            member this.sizes = (this.length1, this.length2)

            member this.sub2ind struct(i, j) = 
                let m = this.length1
                i + m*j

            member this.eachindex = 
                let length1 = this.length1
                let length2 = this.length2
                seq {
                    for i in 0 .. length1 - 1 do
                        for j in 0 .. length2 - 1 do
                            yield struct(i, j) }
        end

        