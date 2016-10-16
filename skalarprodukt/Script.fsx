// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "NDArray.fs"
#load "NDims.fs"
#r @"..\..\skalarprodukt\skalarprodukt.Providers\bin\Debug\skalarprodukt.Providers.dll"

open skalarprodukt
open skalarprodukt.Providers

open NDArray
open NDims

type ``N = 2`` = NDims<2>
type Matrix<'t> = NDArray<'t, ``N = 2``>

let m1 : Matrix<_> = NDArray.create (2, 2) 0
NDArray.set m1 (1, 1) 1

