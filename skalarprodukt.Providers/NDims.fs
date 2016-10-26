module NDims

open ProviderImplementation.ProvidedTypes
open Microsoft.FSharp.Core.CompilerServices
open System.Reflection
open FSharp.Quotations
open FSharp.Reflection

open NTuple

let makeIndexer (sizes:NTuple<int>) (ind:NTuple<int>) = 
    let n = sizes.Length
    let impl sizes ind = 
        let last = n - 1
        let mutable ex = NTuple.get last ind
        for i = last - 1 downto 0 do
            ex <- <@ %(NTuple.get i ind) + %(NTuple.get i sizes)*(%ex) @>
        ex
    
    impl sizes ind

let makeLength (sizes:NTuple<int>)  = 
    let n = sizes.Length
    let impl sizes = 
        let last = n - 1
        let mutable ex = Expr.Value(1) |> Expr.Cast
        for i = last downto 0 do
            ex <- <@ %(NTuple.get i sizes)*(%ex) @>
        ex
    
    impl sizes

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
                                                        let impl = makeLength sizes
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
                                parameters = [ProvidedParameter("sizes", nTupleType);
                                              ProvidedParameter("ind", nTupleType)], 
                                returnType = typeof<int>,
                                IsStaticMethod = true,
                                InvokeCode = (fun [sizesExpr; indExpr] -> 
                                                let sizes = NTuple.fromExpr<int> sizesExpr
                                                let ind = NTuple.fromExpr<int> indExpr
                                                let impl = makeIndexer sizes ind
                                                impl.Raw))

        ndims.AddMember(indexer)
        tempAsmNested.AddTypes [ndims]
        ndims)

    do
        this.RegisterRuntimeAssemblyLocationAsProbingFolder config
        tempAsm.AddTypes [ndimsProvider]
        this.AddNamespace(ns, [ndimsProvider])

[<assembly:TypeProviderAssembly>]
do ()


