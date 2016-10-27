// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "NDArray.fs"
#r @"..\..\skalarprodukt\skalarprodukt.Providers\bin\Debug\skalarprodukt.Providers.dll"

open skalarprodukt
open skalarprodukt.Providers

open NDArray
open NDims

type ``N = 1`` = NDims<1>
type Vector<'t> = NDArray<'t, ``N = 1``>
let v1 : Vector<_> = NDArray.zeroCreate (3)
NDArray.set v1 2 1
NDArray.eachindex v1 |> Seq.toArray

type ``N = 2`` = NDims<2>
type Matrix<'t> = NDArray<'t, ``N = 2``>

let m1 : Matrix<_> = NDArray.zeroCreate (2, 2)
NDArray.set m1 (1, 1) 1
NDArray.eachindex m1 |> Seq.toArray

