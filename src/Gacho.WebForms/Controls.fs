module Gacho.WebForms.Controls

open System.Web.UI

let findControl id (ctl : Control) : 'a option when 'a :> Control = 
    match ctl.FindControl(id) with
        | :? 'a as x -> Some(x)
        | _ -> None
