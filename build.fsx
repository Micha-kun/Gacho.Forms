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

Target "Default" (fun _ ->
    trace "Hello World from FAKE"
)

"Clean"
    ==> "BuildLib"
    ==> "BuildTest"
    ==> "Test"
    ==> "Default"

RunTargetOrDefault "Default"