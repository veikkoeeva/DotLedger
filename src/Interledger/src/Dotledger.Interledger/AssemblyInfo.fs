namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Dotledger.Interledger")>]
[<assembly: AssemblyProductAttribute("Dotledger.Interledger")>]
[<assembly: AssemblyDescriptionAttribute("A Payment Protocol Inspired by IP.")>]
[<assembly: AssemblyVersionAttribute("0.1")>]
[<assembly: AssemblyFileVersionAttribute("0.1")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.1"
    let [<Literal>] InformationalVersion = "0.1"
