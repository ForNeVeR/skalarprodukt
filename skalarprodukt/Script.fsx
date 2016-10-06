// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "DenseMatrix.fs"
open skalarprodukt

let m1 = DenseMatrix.ofArray 2 2 [|1; 0; 0; 1|]
let m2 = DenseMatrix.ofArray 2 2 [|0; -1; -1; 0|]

let m3 = m1 + m2
let m4 = m1 .* m2