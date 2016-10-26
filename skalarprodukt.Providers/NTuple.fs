module NTuple

open System
open FSharp.Quotations
open FSharp.Reflection

type NTuple<'a> =
    {
        Length : int
        Type : Type
        Expr : Expr
    }

let makeType<'a> n =
    if n = 1 then typeof<'a>
    else FSharpType.MakeTupleType(typeof<'a> |> Array.replicate n)

let getTypeLength (tupleType:Type) =
    let n = tupleType.GetGenericArguments().Length
    if n = 0 then 1 else n

let toExpr t = t.Expr

let fromExpr<'a> (e:Expr) : NTuple<'a> =
    {Length = getTypeLength e.Type; Type = e.Type; Expr = e}

let init (elements : Expr<'a> list) : NTuple<'a> =
    let n = elements.Length
    let rawElements = List.map (fun (e:Expr<'a>) -> e.Raw) elements
    let expr = if n = 1 then List.head rawElements
               else Expr.NewTuple rawElements
    {Length = n; Type = makeType<'a> n; Expr = expr}
    
let get ind (t:NTuple<'a>) : Expr<'a> =
    let n = t.Length
    if n = 1 then t.Expr |> Expr.Cast
    else Expr.TupleGet (t.Expr, ind) |> Expr.Cast

let head t = get 0 t

let tail t = 
    let n = t.Length
    let elements = [for i in 1..n-1 -> get i t]
    init elements

let append t v =
    let n = t.Length
    let elements = [for i in 0..n-1 -> get i t]
    init (v::elements)