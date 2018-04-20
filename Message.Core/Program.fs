open System
open System.Threading
open Suave
open Routing

[<EntryPoint>]
let main argv = 
    let cts = new CancellationTokenSource()
    let port = 8080
    let local = Http.HttpBinding.createSimple HTTP "10.0.0.10" port
    let conf = { defaultConfig with cancellationToken = cts.Token; bindings = [local]; }


    let _, server = startWebServerAsync conf appRoute

    Async.Start(server, cts.Token)
    Console.ReadKey true |> ignore

    cts.Cancel()

    0
  