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

let makeIndexer n size ind = 
    let get = getNTupleVal n
    let impl size ind = 
        let last = n - 1
        let mutable ex = ind |> get last
        for i = last - 1 downto 0 do
            ex <- <@@ %%(ind |> get i) + %%(size |> get i)*(%%ex) @@>
        ex
    
    impl size ind

let makeLength n size  = 
    let get = getNTupleVal n
    let impl size = 
        let last = n - 1
        let mutable ex = Expr.Value(1)
        for i = last downto 0 do
            ex <- <@@ %%(size |> get i)*(%%ex) @@>
        ex
    
    impl size

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
        let nTupleType = makeNTupleType n typeof<int>
        let ndims = ProvidedTypeDefinition(asm, ns, typeName, Some typeof<obj>, IsErased = false)

        let nProp = ProvidedProperty("n", typeof<int>, [], IsStatic = true, 
                                        GetterCode = fun args -> <@@ n @@>)

        let sizeField = ProvidedField("_size", nTupleType);
        let sizeProp = ProvidedProperty("size", nTupleType, [], IsStatic = false,
                                        GetterCode = (fun [this] -> Expr.FieldGet(this, sizeField)))
    
        let lengthProp = ProvidedProperty("length", typeof<int>, [], IsStatic = false,
                                        GetterCode = (fun [this] -> 
                                                        let size = Expr.FieldGet(this, sizeField)
                                                        makeLength n size))

        ndims.AddMember(nProp)
        ndims.AddMember(sizeField)
        ndims.AddMember(sizeProp)
        ndims.AddMember(lengthProp)

        let defaultCtor = ProvidedConstructor(parameters = [], 
                                                InvokeCode = (fun [this] -> 
                                                    Expr.FieldSet(this, sizeField, 
                                                        Expr.NewTuple(Expr.Value(0) |> List.replicate n)
                                                    )))
        ndims.AddMember(defaultCtor)
       
        let ctor = ProvidedConstructor(parameters = [ProvidedParameter("size", nTupleType)],
                                        InvokeCode = (fun [this; size] -> (Expr.FieldSet(this, sizeField, size))))
        ndims.AddMember(ctor)

        let indexer = ProvidedMethod("indexer", 
                                parameters = [ProvidedParameter("size", nTupleType);
                                              ProvidedParameter("ind", nTupleType)], 
                                returnType = typeof<int>,
                                IsStaticMethod = true,
                                InvokeCode = (fun [size; ind] -> makeIndexer n size ind))

        ndims.AddMember(indexer)
        tempAsmNested.AddTypes [ndims]
        ndims)

    do
        this.RegisterRuntimeAssemblyLocationAsProbingFolder config
        tempAsm.AddTypes [ndimsProvider]
        this.AddNamespace(ns, [ndimsProvider])

[<assembly:TypeProviderAssembly>]
do ()


