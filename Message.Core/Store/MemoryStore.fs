module MemoryStore

let private Store : string array = Array.zeroCreate 20
let mutable index : int = -1


let public GetStore (): List<string> =
    if index = -1 then 
        Store |> Array.toList |> List.filter( fun x -> x <> null && x.Length > 0 )
    else
        let tempStore : string array = Array.zeroCreate 20
        let offset = 19 - index
        for pos in 0..19 do
            tempStore.SetValue(Store.[pos], (pos + offset)%20)
        tempStore |> Array.toList |> List.filter( fun x -> x <> null && x.Length > 0 )

let public AddMessage (message : string) =
    index <- (index + 1)%20
    Store.SetValue(message, index)