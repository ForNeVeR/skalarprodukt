namespace skalarprodukt

module DenseMatrix =

    type DenseMatrix<'t, 's when 's : (static member NumRows : int with get)
                             and 's : (static member NumCols : int with get)> =
        {
            Data : 't array
        }

        static member inline nrows (x : DenseMatrix<'a, ^s>) = 
            (^s : (static member NumRows : int with get) ())

        static member inline ncols (x : DenseMatrix<'a, ^s>) = 
            (^s : (static member NumCols : int with get) ())

        static member inline map2 f (x:DenseMatrix<'a, 's>) (y:DenseMatrix<'b, 's>) = 
            let sum : DenseMatrix<'a, 's> = { Data = Array.map2 f x.Data y.Data }
            sum

        static member inline (+) (x, y) = DenseMatrix<_, _>.map2 (+) x y

    type DenseMatrix<'t> = 
        {
            NumRows : int
            NumCols : int
            Data : 't array
        }

        static member inline nrows x = x.NumRows

        static member inline ncols x = x.NumCols

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

    let inline nrows (x: ^a) =
        (^a : (static member nrows : 'a -> int) (x))

    let inline ncols (x: ^a) =
        (^a : (static member ncols : 'a -> int) (x))