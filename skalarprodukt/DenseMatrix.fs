namespace skalarprodukt

module DenseMatrix =
    type DenseMatrix<'t> = 
        {
            NumRows : int
            NumCols : int
            Data : 't array
        }

        static member inline map2 f x y =
            if x.NumCols <> y.NumCols || x.NumRows <> y.NumRows
            then raise(System.ArithmeticException())
            {
                NumRows = x.NumRows
                NumCols = x.NumCols
                Data = Array.map2 f x.Data y.Data
            }

        static member inline (+) (x, y) = 
            DenseMatrix<_>.map2 (+) x y

        static member inline (-) (x, y) =
            DenseMatrix<_>.map2 (-) x y

        static member inline (.*) (x, y) =
            DenseMatrix<_>.map2 (*) x y

        static member inline (./) (x, y) =
            DenseMatrix<_>.map2 (/) x y

    let ofArray nrows ncols data =
        {
            NumRows = nrows
            NumCols = ncols
            Data = data
        }

    let inline (.*) (x, y) = x .* y
    let inline (./) (x, y) = x ./ y
