
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
   
type User = Db.User
let ct = Db.ctx

//let s = Db.cleanDatabase "Abdb"

(* Data for testing *)

let chatId = "Grp1"
let chatDesc = "F# Group 2020"
let cht = Db.newRoom (chatId, chatDesc,"G") ct
let chatId2 = "Grp1"
let chatDesc2 = "sharpApp"
let cht2 = Db.newRoom (chatId2, chatDesc2,"P") ct
let m3 = Db.saveMessage("Grp1" , "","The test has been successful!", "G", "Kimn") ct
let m4 = Db.saveMessage("Grp1" , "","Hi ..", "G", "Shab") ct
let m5 = Db.saveMessage(chatId , "","Hey..", "G", "Abdc") ct
let m6 = Db.saveMessage("Grp1" , "","Hi All....", "G", "Abdb") ct 
let m7 = Db.saveMessage(chatId , "","Check time now....", "G", "Abdc") ct

let config =
  { defaultConfig with homeFolder = Some (Path.GetFullPath  __SOURCE_DIRECTORY__+ @"\public") }

Db.showUsers
|> Seq.iter (fun usr -> printfn "Id: %s Name: %s Pass: %s Admin: %s" usr.UserId usr.UserName usr.Password usr.Admin)
printfn "-----------------------"

Db.displayOldMessages chatId

[<EntryPoint>]
let main argv = 
    startWebServer config app
    0
