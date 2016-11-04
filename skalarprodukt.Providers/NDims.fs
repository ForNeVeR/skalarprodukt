module NDims

open ProviderImplementation.ProvidedTypes
open Microsoft.FSharp.Core.CompilerServices
open System.Reflection
open FSharp.Quotations
open FSharp.Reflection

open NTuple
open ExprUtils

module Seq =
    let concatMap<'T, 'U> (mapping: 'T -> seq<'U>) (source: seq<'T>) =
        source
        |> Seq.map mapping
        |> Seq.concat

let indexer (sizes:NTuple<int>) (ind:NTuple<int>) = 
    let n = sizes.Length
    let impl sizes ind = 
        let s = NTuple.fromExpr<int> sizes
        let i = NTuple.fromExpr<int> ind 
        let last = n - 1
        let mutable ex = NTuple.get last i
        for j = last - 1 downto 0 do
            ex <- <@ %(NTuple.get j i) + %(NTuple.get j s)*(%ex) @>
        ex.Raw
        
    let tupleType = sizes.Type
    let res = letT tupleType (NTuple.toExpr sizes) (fun s -> 
        letT tupleType (NTuple.toExpr ind) (fun i ->
            impl s i))

    res

let length (sizes:NTuple<int>)  = 
    let n = sizes.Length
    let impl sizes = 
        let last = n - 1
        let mutable ex = Expr.Value(1) |> Expr.Cast
        for i = last downto 0 do
            ex <- <@ %(NTuple.get i sizes)*(%ex) @>
        ex
    
    impl sizes

let rec eachindex (sizes : NTuple<int>) =
    let n = sizes.Length
    if n = 1 then
        let x = NTuple.head sizes
        <@@ seq { 0..%x-1 } @@>
    else
        let x = NTuple.head sizes
        let xs = NTuple.tail sizes
        let expr = eachindex xs 

        let init count initializer = callT sizes.Type <@@ Seq.init @@> [count; initializer]
        let concatMap mapping source = callTR xs.Type sizes.Type <@@ Seq.concatMap @@> [mapping; source]
        
        expr |> concatMap (lambdaT xs.Type 
                    (fun smallTupleExpr -> 
                        let smallTuple = NTuple.fromExpr<int> smallTupleExpr
                        init x (lambda (fun i -> 
                            NTuple.append smallTuple i |> NTuple.toExpr) )))

[<TypeProvider>]
type NDimsProvider (config : TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces()

    let ns = "skalarprodukt.Providers"
    let asm = Assembly.LoadFrom(config.RuntimeAssembly)
    let tempAsmPath = System.IO.Path.ChangeExtension(System.IO.Path.GetTempFileName(), ".dll")
    let tempAsm = ProvidedAssembly tempAsmPath

    let ndimsProvider = ProvidedTypeDefinition(asm, ns, "NDims", Some(typeof<obj>), IsErased = false)

    let parameters = [ProvidedStaticParameter("n", typeof<int>)]
    do ndimsProvider.DefineStaticParameters(parameters, fun typeName args ->

        let tempAsmPathNested = System.IO.Path.ChangeExtension(System.IO.Path.GetTempFileName(), ".dll")    
        let tempAsmNested = ProvidedAssembly tempAsmPathNested

        let n = args.[0] :?> int
        let nTupleType = NTuple.makeType<int> n
        let ndims = ProvidedTypeDefinition(asm, ns, typeName, Some typeof<obj>, IsErased = false)

        let nProp = ProvidedProperty("n", typeof<int>, [], IsStatic = true, 
                                        GetterCode = fun args -> <@@ n @@>)

        let sizesField = ProvidedField("_sizes", nTupleType);
        let sizesProp = ProvidedProperty("sizes", nTupleType, [], IsStatic = false,
                                        GetterCode = (fun [this] -> Expr.FieldGet(this, sizesField)))
    
        let lengthProp = ProvidedProperty("length", typeof<int>, [], IsStatic = false,
                                        GetterCode = (fun [this] -> 
                                                        let sizesExpr = Expr.FieldGet(this, sizesField)
                                                        let sizes = NTuple.fromExpr<int> sizesExpr
                                                        let impl = length sizes
                                                        impl.Raw))

        ndims.AddMember(nProp)
        ndims.AddMember(sizesField)
        ndims.AddMember(sizesProp)
        ndims.AddMember(lengthProp)

        let defaultCtor = ProvidedConstructor(parameters = [], 
                                                InvokeCode = (fun [this] ->
                                                    let sizes = NTuple.init (<@ 0 @> |> List.replicate n)
                                                    Expr.FieldSet(this, sizesField, sizes.Expr)))
        ndims.AddMember(defaultCtor)
       
        let ctor = ProvidedConstructor(parameters = [ProvidedParameter("sizes", nTupleType)],
                                        InvokeCode = (fun [this; size] -> (Expr.FieldSet(this, sizesField, size))))
        ndims.AddMember(ctor)

        let indexer = ProvidedMethod("indexer", 
                                    parameters = [ProvidedParameter("ind", nTupleType)], 
                                    returnType = typeof<int>,
                                    InvokeCode = (fun [this; indExpr] -> 
                                                    let sizes = NTuple.fromExpr<int> <| Expr.FieldGet(this, sizesField)
                                                    let ind = NTuple.fromExpr<int> indExpr
                                                    let impl = indexer sizes ind
                                                    impl))

        ndims.AddMember(indexer)

        let seqOfNTuplesType = makeGenericType typedefof<seq<_>> [nTupleType]
        let eachindex = ProvidedMethod("eachindex",
                                        parameters = [ProvidedParameter("sizes", nTupleType)],
                                        returnType = seqOfNTuplesType,
                                        IsStaticMethod = true,
                                        InvokeCode = (fun [sizesExpr] ->
                                            let sizes = NTuple.fromExpr<int> sizesExpr
                                            let impl = eachindex sizes
                                            impl
                                            ))
        ndims.AddMember(eachindex)

        tempAsmNested.AddTypes [ndims]
        ndims)

    do
        this.RegisterRuntimeAssemblyLocationAsProbingFolder config
        tempAsm.AddTypes [ndimsProvider]
        this.AddNamespace(ns, [ndimsProvider])

[<assembly:TypeProviderAssembly>]
do ()


