namespace System
open System.Reflection
open System.Runtime.CompilerServices
open System.Runtime.InteropServices

[<assembly: AssemblyTitleAttribute("Gacho.Forms")>]
[<assembly: AssemblyDescriptionAttribute("A small library with types and helper tools for WinForms and WebForms developers in F#.")>]
[<assembly: GuidAttribute("FB326BAF-C0EB-48E6-B3E6-E3432D95BD16")>]
[<assembly: InternalsVisibleToAttribute("Gacho.Forms.Tests")>]
[<assembly: AssemblyProductAttribute("Gacho.Forms")>]
[<assembly: AssemblyCompanyAttribute("Michael-Jorge Gómez Campos")>]
[<assembly: AssemblyCopyrightAttribute("Copyright © 2015 Michael-Jorge Gómez")>]
[<assembly: AssemblyVersionAttribute("0.1.0.0")>]
[<assembly: AssemblyFileVersionAttribute("0.1.0.0")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.1.0.0"
