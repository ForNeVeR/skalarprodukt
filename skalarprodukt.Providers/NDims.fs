module NDims

open ProviderImplementation.ProvidedTypes
open Microsoft.FSharp.Core.CompilerServices
open System.Reflection
open FSharp.Quotations

let genIndexer n dims : Expr<int array -> int> = 

    let indVar = Var("ind", typeof<int array>)
    let ind : Expr<int array> = Expr.Var(indVar) |> Expr.Cast

    let inline impl (dims:Expr<int array>) (ind:Expr<int array>) = 
        let last = n - 1
        let mutable ex = <@ (%ind).[last] @>
        for i = last - 1 downto 0 do
            ex <- <@ (%ind).[i] + (%dims).[i]*(%ex) @>
        ex
    
    let func = Expr.Lambda(indVar, impl dims ind) |> Expr.Cast
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

        let nProp = ProvidedProperty("n", typeof<int>, [], IsStatic = true, GetterCode = fun args -> <@@ n @@>)
        let indexerMethod = ProvidedMethod("indexer", 
                                parameters = [ProvidedParameter("dims", typeof<int array>)], 
                                returnType = typeof<int array -> int>,
                                IsStaticMethod = true,
                                InvokeCode = (fun args -> 
                                    let dims = args.[0] |> Expr.Cast
                                    <@@ %(genIndexer n dims) @@>))

        let indicesMethod = ProvidedMethod("indices",
                                parameters = [ProvidedParameter("dims", typeof<int array>)],
                                returnType = typeof< seq<int array> >,
                                IsStaticMethod = true,
                                InvokeCode = (fun args -> <@@ raise (System.NotImplementedException("")) @@>))
           
        provider.AddMember(nProp)
        provider.AddMember(indexerMethod);
        provider.AddMember(indicesMethod);
        provider)

    do
        this.AddNamespace(ns, [ndimsProvider])

[<assembly:TypeProviderAssembly>]
do ()


