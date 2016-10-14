// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "NDArray.fs"
#load "NDims.fs"
#r @"..\..\skalarprodukt\skalarprodukt.Providers\bin\Debug\skalarprodukt.Providers.dll"

open skalarprodukt
open skalarprodukt.Providers

open NDArray
open NDims

let m1:NDArray<int, NDims<2>> = 
    {
        dims = [| 2; 2 |]
        data = [| 1; 2; 3|]
    }

let n = NDArray.ndims m1
let index = NDArray.indexer m1
index (1, 1)