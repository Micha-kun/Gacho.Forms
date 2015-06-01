module Tests.EventHandlerListWrapper

open NUnit.Framework
open FsUnit

[<Test>]
let ``Return false`` () =
    false |> should equal true