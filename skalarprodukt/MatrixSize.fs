module MatrixSize

open skalarprodukt.DenseMatrix
open ProviderImplementation.ProvidedTypes
open Microsoft.FSharp.Core.CompilerServices
open System.Reflection

[<TypeProvider>]
type MatrixSizeProvider (config : TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces ()

    let ns = "skalarprodukt.MatrixSize.Provided"
    let asm = Assembly.GetExecutingAssembly()

    let matrixSizeProvider = ProvidedTypeDefinition(asm, ns, "Size", Some(typeof<obj>))

    let parameters = [ProvidedStaticParameter("NumRows", typeof<int>)
                      ProvidedStaticParameter("NumCols", typeof<int>)]

    do matrixSizeProvider.DefineStaticParameters(parameters, fun typeName args ->
        let nrows = args.[0] :?> int
        let ncols = args.[1] :?> int
        let provider = ProvidedTypeDefinition(asm, ns, typeName, Some typeof<obj>, HideObjectMethods = true)

        let numRowsProp = ProvidedProperty("NumRows", typeof<int>, [], IsStatic = true, GetterCode = fun args -> <@@ nrows @@>)
        let numColsProp = ProvidedProperty("NumCols", typeof<int>, [], IsStatic = true, GetterCode = fun args -> <@@ ncols @@>)
           
        provider.AddMember(numRowsProp)
        provider.AddMember(numColsProp)
        provider)

    do
        this.AddNamespace(ns, [matrixSizeProvider])

[<assembly:TypeProviderAssembly>]
do ()