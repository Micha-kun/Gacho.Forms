// Include FAKE lib
#r "packages/FAKE/tools/FakeLib.dll"
open Fake

RestorePackages()

let buildDir = "./build/"
let testDir = "./test/"

Target "Clean" (fun _ ->
    CleanDir buildDir
)

Target "BuildLib" (fun _ ->
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
             ToolPath = "./packages/NUnit.Runners.2.6.3/tools"
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
            //ToolPath = "./packages/NUnit.Runners.2.6.3/tools"
            AccessKey = getBuildParamOrDefault "nugetkey" ""
            Publish = hasBuildParam "nugetkey"
        }) ("nuget/Gacho.Forms.nuspec")
)

Target "Default" DoNothing

"Clean"
    ==> "BuildLib"
    ==> "BuildTest"
    ==> "Test"
    ==> "NuGet"
    ==> "Default"

RunTargetOrDefault "Default"
