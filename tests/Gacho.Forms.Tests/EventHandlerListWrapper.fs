module Tests.EventHandlerListWrapper

open NUnit.Framework
open FsUnit
open Gacho.Forms.Events
open System
open System.ComponentModel

[<Test>]
let ``Assert that we can create an EventHandlerListWrapper instance without error`` () =
    let evl = new EventHandlerList()
    let event = new EventHandlerListWrapper<EventArgs>( evl, "test")
    true |> should be True

