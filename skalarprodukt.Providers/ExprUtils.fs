module ExprUtils

open System
open FSharp.Quotations
open FSharp.Reflection

open Microsoft.FSharp.Quotations.Patterns
let rec getMethodInfo = function
| Call(_, mi, _) -> mi
| Lambda(v, e) -> getMethodInfo e

let makeGenericType (baseType : Type) (types : Type list) = 
    if (not baseType.IsGenericTypeDefinition) then
        invalidArg "baseType" "baseType must be a generic type definition."

    baseType.MakeGenericType (types |> List.toArray)

let makeGenericMethod method (types : Type[])= 
        (getMethodInfo method)
            .GetGenericMethodDefinition()
            .MakeGenericMethod(types)

// <@ fun x -> (% <@ x @> ) @> ~ lambda (fun x -> x)
let lambda (f : Expr<'T> -> Expr) : Expr =
    let var = new Var("__temp__", typeof<'T>)
    Expr.Lambda(var,  f (Expr.Cast<_>(Expr.Var var)))

// <@ fun x -> (% <@ x @> ) @> ~ lambda (fun x -> x)
let lambdaT argType (f : Expr -> Expr) : Expr =
    let var = new Var("__temp__", argType)
    Expr.Lambda(var,  f (Expr.Var(var)))

let letT argType value (f : Expr -> Expr) : Expr =
    let var = new Var("__temp__", argType)
    Expr.Let(var, value, f (Expr.Var(var)))

let callT t method args =
    let methodDef = makeGenericMethod method [|t|]
    Expr.Call(methodDef, args)

let callTR t r method args =
    let methodDef = makeGenericMethod method [|t; r|]
    Expr.Call(methodDef, args)