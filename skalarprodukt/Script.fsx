// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "NDArray.fs"
#load "NDims.fs"
#r @"..\..\skalarprodukt\skalarprodukt.Providers\bin\Debug\skalarprodukt.Providers.dll"

open skalarprodukt
open skalarprodukt.Providers

open NDArray
open NDims

let v1:NDArray<int, NDims1> = 
    {
        dims = [| 3 |]
        data = [| 1; 2; 3|]
    }

let v2 = NDArray.map (fun x -> x*x) v1
let v3 = NDArray.mapi (fun (ind:int array) x -> (ind.[0], x)) v1 

let m1:NDArray<int, NDims2> =
    {
        dims = [| 2; 2 |]
        data = [| 1; 2; 3; 4|]
    }

let m2 = NDArray.map (fun x -> x*x) m1
let m3 = NDArray.mapi (fun (ind:int array) x -> (ind.[0] + ind.[1], x)) m1