// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#r @"..\..\skalarprodukt\skalarprodukt\bin\Release\skalarprodukt.dll"

open skalarprodukt
open NDArray

let mat : Matrix<_> = NDArray.init (3, 3) (fun (i, j) -> i + j)
