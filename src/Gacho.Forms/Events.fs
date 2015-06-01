namespace Gacho.Forms

open System
open System.ComponentModel

type EventHandlerListWrapper<'a when 'a :> EventArgs>(ehl : EventHandlerList, key)  =
    member this.Publish = 
        { new IDelegateEvent<EventHandler<'a>> with
              member x.AddHandler(handler) = 
                  ehl.AddHandler (key, handler)
              
              member x.RemoveHandler(handler) = 
                  ehl.RemoveHandler (key, handler) }

    member this.Trigger (sender, args) =
         match ehl.[key] with
            | :? EventHandler<'a> as hdl -> hdl.Invoke(this, args)
            | _ -> ignore()