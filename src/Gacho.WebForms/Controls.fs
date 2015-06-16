module Gacho.WebForms.Controls

open System.Web.UI

type Control with
    member this.FindControl<'a when 'a :> Control> id =
        match this.FindControl(id) with
            | Some(ctrl : 'a) -> Some(ctrl)
            | _ -> None

let findControl<'a when 'a :> Control> id (ctl : Control) =
    ctl.FindControl<'a> id