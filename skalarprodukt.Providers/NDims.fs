module NDims

open ProviderImplementation.ProvidedTypes
open Microsoft.FSharp.Core.CompilerServices
open System.Reflection

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
                                returnType = typeof<int>,
                                InvokeCode = (fun args -> <@@ raise (System.NotImplementedException("")) @@>))

        let indicesMethod = ProvidedMethod("indices",
                                parameters = [ProvidedParameter("dims", typeof<int array>)],
                                returnType = typeof< seq<int array> >,
                                InvokeCode = (fun args -> <@@ raise (System.NotImplementedException("")) @@>))
           
        provider.AddMember(nProp)
        provider.AddMember(indexerMethod);
        provider.AddMember(indicesMethod);
        provider)

    do
        this.AddNamespace(ns, [ndimsProvider])

[<assembly:TypeProviderAssembly>]
do ()


