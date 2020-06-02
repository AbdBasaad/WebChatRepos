
open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open System.IO
open Newtonsoft.Json

type HelloMessage =
    {
        Msg : string
    }

let sayHello name =
    { Msg = "Hello " + name}
    |> JsonConvert.SerializeObject //serialze the data into text so that we can send it over HTTP
    |> OK

let app =
  choose
    [ GET >=> choose
        [ path "/" >=> Files.browseFileHome "index.html"
          path "/login" >=> OK View.login
          path "/register" >=> OK View.register
          path "/hello" >=> OK "Hello GET"
          path "/goodbye" >=> OK "Good bye GET" ] 
      POST >=> choose
        [ pathScan "/hello/%s" sayHello
          path "/goodbye" >=> OK "Good bye POST" ] 
    ]

let config =
  { defaultConfig with homeFolder = Some (Path.GetFullPath  __SOURCE_DIRECTORY__+ @"\public") }

Db.showUsers
|> Seq.iter (fun usr -> printfn "Id: %s Name: %s Pass: %s Admin: %s" usr.UserId usr.UserName usr.Password usr.Admin)
printfn "-----------------------"

[<EntryPoint>]
let main argv = 
    startWebServer config app
    0
