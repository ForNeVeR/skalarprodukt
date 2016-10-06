// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#r @"..\..\skalarprodukt\skalarprodukt\bin\Debug\skalarprodukt.dll"
open skalarprodukt
open skalarprodukt.DenseMatrix
open skalarprodukt.MatrixSize.Provided

let m0 : DenseMatrix<int, Size<2, 2>> = { Data = [|1; 0; 0; 1|] }
let m1 : DenseMatrix<int, Size<2, 2>> = { Data = [|1; 0; 0; 1|] }

let m2 = m0 + m1

let n = DenseMatrix.nrows m2