module MessageControl

open Newtonsoft.Json
open Suave.Utils.Collections
open Suave.Http

let CheckMessages ()= 
    MemoryStore.GetStore()

let SendMessage (r : HttpRequest)=
    if r.multiPartFields.Length > 0 then
        let _, data = r.multiPartFields.Item(0)
        MemoryStore.AddMessage(data)
    else
        let _, data = r.form.Item(0)
        MemoryStore.AddMessage(data.Value)
    "OK"