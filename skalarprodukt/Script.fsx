// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "NDArray.fs"
#load "NDims.fs"

open skalarprodukt
open NDArray
open NDims

let v1:NDArray<int, NDims1> = 
    {
        dims = [| 3 |]
        data = [| 1; 2; 3|]
    }

let v2 = NDArray.map (fun x -> x*x) v1
let v3 = NDArray.mapi (fun i x -> (i, x)) v1 

let m1:NDArray<int, NDims2> =
    {
        dims = [| 2; 2 |]
        data = [| 1; 2; 3; 4|]
    }

let m2 = NDArray.map (fun x -> x*x) m1
let m3 = NDArray.mapi (fun ind x -> (fst ind + snd ind, x)) m1