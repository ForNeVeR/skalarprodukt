// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#r @"..\..\skalarprodukt\skalarprodukt\bin\Release\skalarprodukt.dll"

open skalarprodukt
open NDArray

let v1 : Vector<_> = NDArray.init 3 id
v1 |> NDArray.mapi (+)
v1 |> NDArray.iteri (fun i v -> printf "(%d) = %d\n" i v)

let m1 : Matrix<_> = NDArray.init (2, 2) (fun (i, j) -> if i = j then 1 else 0)
m1 |> NDArray.mapi (fun (i, j) v -> if i = j then -v else v)
m1 |> NDArray.iteri (fun (i, j) v -> printf "(%d, %d) = %d\n" i j v)