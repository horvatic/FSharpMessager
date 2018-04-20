module Routing

open Suave
open Suave.Operators
open Suave.Successful
open Suave.Filters
open MessageControl
open Errors
open System
open Newtonsoft.Json
open Newtonsoft.Json.Serialization

let ConvertToJson v =
  let jsonSerializerSettings = new JsonSerializerSettings()
  jsonSerializerSettings.ContractResolver <- new CamelCasePropertyNamesContractResolver()

  JsonConvert.SerializeObject(v, jsonSerializerSettings)
  |> OK
  >=> Writers.setMimeType "application/json; charset=utf-8"

let appRoute : WebPart = 
    choose [
        path "/" >=> choose [
          GET  >=> Files.file "C:/git/Message/Message.View/index.html"
          RequestErrors.NOT_FOUND "Found no handlers" ]
        path "/message.js" >=> choose [
          GET  >=> Files.file "C:/git/Message/Message.View/message.js"
          RequestErrors.NOT_FOUND "Found no handlers" ]
        path "/m" >=> choose [
          GET  >=> request (fun r -> CheckMessages() |> ConvertToJson)
          POST >=> request (fun r -> OK (SendMessage(r)))
          RequestErrors.NOT_FOUND "Found no handlers" ]
    ]