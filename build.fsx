// Include FAKE lib
#r "packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile

let buildDir = "./build/"
let testDir = "./test/"

let versionNumber =
    match buildServer with
    | TeamCity -> buildVersion
    | _ -> "0.1.0.0"

let baseAttributes = [
    Attribute.Product "Gacho.Forms"
    Attribute.Company "Michael-Jorge Gómez Campos"
    Attribute.Copyright "Copyright © 2015 Michael-Jorge Gómez"
    Attribute.Version versionNumber
    Attribute.FileVersion versionNumber
]

Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir]
    RestorePackages()
)

Target "BuildLib" (fun _ ->
    CreateFSharpAssemblyInfo "src/Gacho.Forms/AssemblyInfo.fs"
        [
            yield Attribute.Title "Gacho.Forms"
            yield Attribute.Description "A small library with types and helper tools for WinForms and WebForms developers in F#."
            yield Attribute.Guid "FB326BAF-C0EB-48E6-B3E6-E3432D95BD16"
            yield Attribute.InternalsVisibleTo "Gacho.Forms.Tests"
            yield! baseAttributes
        ]

    !! "src/**/*.fsproj"
        |> MSBuildRelease buildDir "Build"
        |> Log "LibBuild-Output: "
)

Target "BuildTest" (fun _ ->
    !! "tests/**/*.fsproj"
        |> MSBuildDebug testDir "Build"
        |> Log "TestBuild-Output: "
)

Target "Test" (fun _ ->
    !! (testDir + "/Gacho.Forms.Tests.dll")
      |> NUnit (fun p ->
          {p with
             ToolPath = "./packages/NUnit.Runners.2.6.4/tools"
             DisableShadowCopy = true;
             OutputFile = testDir + "TestResults.xml" })
)

Target "NuGet" (fun _ ->
    NuGet (fun p -> 
        {p with
            Authors = ["Michael-Jorge Gómez Campos"]
            Project = "Gacho.Forms"
            Summary = ""
            Description = "A small library with types and helper tools for WinForms and WebForms developers in F#."
            Version = "0.1.0.0"
            ReleaseNotes = ""
            Tags = "F#"
            OutputPath = "nuget"
            AccessKey = getBuildParamOrDefault "nugetkey" ""
            Publish = hasBuildParam "nugetkey"
            Files = [
                        ("""..\build\*.dll""", Some "lib", None)
                        ("""..\build\*.xml""", Some "lib", None)
                    ]
        }) "nuget/Gacho.Forms.nuspec"
)

Target "Default" DoNothing

"Clean"
    ==> "BuildLib"
    ==> "BuildTest"
    ==> "Test"
    ==> "NuGet"
    ==> "Default"

RunTargetOrDefault "Default"
