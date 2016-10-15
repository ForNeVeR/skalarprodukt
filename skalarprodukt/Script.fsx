// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "NDArray.fs"
#load "NDims.fs"
#r @"..\..\skalarprodukt\skalarprodukt.Providers\bin\Debug\skalarprodukt.Providers.dll"

open skalarprodukt
open skalarprodukt.Providers

open NDArray
open NDims

let v1 = NDArray.create<_, NDims<1>> [| 3 |] 1
let nv = NDArray.ndims v1
let indexv = NDArray.indexer v1
let indv = indexv 3

let m1 = NDArray.create<_, NDims<2>> [| 2; 2 |] 1
let nm = NDArray.ndims m1
let indexm = NDArray.indexer m1
let indm = indexm (1, 1)