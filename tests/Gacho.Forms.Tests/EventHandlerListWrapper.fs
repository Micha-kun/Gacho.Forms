module Tests.EventHandlerListWrapper

open NUnit.Framework
open FsUnit

[<Test>]
let ``Return false`` () =
    true |> should equal true