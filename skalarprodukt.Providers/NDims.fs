module NDims

open ProviderImplementation.ProvidedTypes
open Microsoft.FSharp.Core.CompilerServices
open System.Reflection
open FSharp.Quotations
open FSharp.Reflection

let makeNTupleType n t =
    if n = 1 then typeof<int>
    else FSharpType.MakeTupleType(t |> Array.replicate n)

let getNTupleVal n ind t =
    if n = 1 then t
    else Expr.TupleGet (t, ind)


let makeIndexer n (dims:Expr<int array>) = 
    let nTupleType = makeNTupleType n typeof<int>
    let indsVar = Var("inds", nTupleType)
    let inds = Expr.Var(indsVar)

    let get = getNTupleVal n
    let impl (dims:Expr<int array>) inds = 
        let last = n - 1
        let mutable ex = inds |> get last
        for i = last - 1 downto 0 do
            ex <- <@@ %%(inds |> get i) + (%dims).[i]*(%%ex) @@>
        ex
    
    let func = Expr.Lambda(indsVar, impl dims inds)
    func 

[<TypeProvider>]
type NDimsProvider (config : TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces ()

    let ns = "skalarprodukt.Providers"
    let asm = Assembly.GetExecutingAssembly()

    let ndimsProvider = ProvidedTypeDefinition(asm, ns, "NDims", Some(typeof<obj>))

    let parameters = [ProvidedStaticParameter("n", typeof<int>)]
    do ndimsProvider.DefineStaticParameters(parameters, fun typeName args ->
        let n = args.[0] :?> int
        let provider = ProvidedTypeDefinition(asm, ns, typeName, Some typeof<obj>, HideObjectMethods = true)

        let nProp = ProvidedProperty("n", typeof<int>, [], IsStatic = true, 
                                        GetterCode = fun args -> <@@ n @@>)

        let nTupleType = makeNTupleType n typeof<int>
        let indexerType = FSharpType.MakeFunctionType(nTupleType, typeof<int>)
        let indexer = ProvidedMethod("indexer", 
                                parameters = [ProvidedParameter("dims", typeof<int array>)], 
                                returnType = indexerType,
                                IsStaticMethod = true,
                                InvokeCode = (fun args -> 
                                    let dims = args.[0] |> Expr.Cast
                                    makeIndexer n dims))
        
        provider.AddMember(nProp)
        provider.AddMember(indexer);
        provider)

    do
        this.AddNamespace(ns, [ndimsProvider])

[<assembly:TypeProviderAssembly>]
do ()


