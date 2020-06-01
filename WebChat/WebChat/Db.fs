module Db

open FSharp.Data.Sql
open System.Net.Http
open Suave
open System

let [<Literal>] resolutionPath = "./packages/System.Data.SQLite.Core.1.0.112.2/lib/net40/System.Data.SQLite.dll"
let [<Literal>] connectionString = "Data Source=" + __SOURCE_DIRECTORY__+ @"/sharpApp.db;Version=3"

type sql = SqlDataProvider< 
              ConnectionString = connectionString,
              DatabaseVendor = Common.DatabaseProviderTypes.SQLITE,
              ResolutionPath = resolutionPath,
              IndividualsAmount = 1000,
              UseOptionTypes = true>

type DbContext = sql.dataContext

let ctx = sql.GetDataContext()

type User     = DbContext.``main.UsersEntity``
type Cht      = DbContext.``main.ChatEntity``
type UsrCht   = DbContext.``main.User_chatEntity``
type Messages = DbContext.``main.MessagesEntity``

// Create new user. Stop onConflict (command usr.OnConflict <- FSharp.Data.Sql.Common.OnConflict.Update)
let newUser (admin, password, userid, username) (ctx: DbContext) =
    let cnt = 
        query {
            for user in ctx.Main.Users do
            where (user.UserId = userid)
            count
        }
    match cnt with
    |0 -> let usr = ctx.Main.Users.Create(admin, password, userid, username)
          ctx.SubmitUpdates()
          printfn "User account has been created."
    |_ -> printfn "User id has been already used!.."  
    
// Create new chat room
let newChat (chatId, chatDesc) (ctx: DbContext) =
    let cnt = 
        query {
            for cht in ctx.Main.Chat do
            where (cht.ChatId = chatId)
            count
        }
    match cnt with
    |0 -> let cht = ctx.Main.Chat.Create(chatDesc, chatId)
          ctx.SubmitUpdates()
          printfn "Chat has been created."
    |_ -> printfn "Chat id has been already used!.."  

// Message parsing
let messageParse (msg: String) :bool = 
    match msg with
    |a when ((msg.Length > 0) && (System.String.IsNullOrWhiteSpace msg = false)) -> true
    |_ -> false

// Save messages in the database
let saveMessage (chatid, filePath, messageText, messageType, userid) (ctx: DbContext) =
    match messageParse messageText with
    |true -> let row = ctx.Main.Messages.Create(chatid, filePath, messageText, messageType, userid)
             ctx.SubmitUpdates()
             printfn "1 Message has been saved.."
    |false -> printfn "Invalid message!."

// Show all users
let showUsers =
    query {
        for user in ctx.Main.Users do
            select user
    }

// Login form
let login userid password (ctx: DbContext) :User option=
    query {
        for user in ctx.Main.Users do
            where (user.UserId = userid && user.Password = password)
            select user
    }|> Seq.tryHead

// Display old chats
let displayOldMessages =
    query {
        for msg in ctx.Main.Messages do
            select msg
    }|>Seq.toList 
      
// Return the old messages of a specific chat room
let openedChatRooms = 
    query {
        for cht in ctx.Main.Chat do
        select cht
    }|>Seq.toList

// Delete all messages of a specific chat room
let deleteHistoryRoom chId = 
    query {
        for msg in ctx.Main.Messages do
        where (msg.ChatId = chId) 
        select msg
    } |>Seq.iter (function x -> x.Delete())
    ctx.SubmitUpdates()

// Admin functions --------------------------------------
let cleanDatabase user =
    let lst = 
        query {
            for usr in ctx.Main.Users do
            where (usr.UserId = user && usr.Admin="Y")
            select usr
        }
        |>Seq.toList

    match lst with
    |a when lst.Length > 0 ->   query {
                                    for msg in ctx.Main.Messages do
                                    select msg
                                }
                                |>Seq.iter (function tbl -> tbl.Delete())

                                query {
                                    for uc in ctx.Main.UserChat do
                                    select uc
                                }
                                |>Seq.iter (function tbl -> tbl.Delete())

                                query {
                                    for c in ctx.Main.Chat do
                                    select c
                                }
                                |>Seq.iter (function tbl -> tbl.Delete())

                                query {
                                    for u in ctx.Main.Users do
                                    where (u.Admin = "N")
                                    select u
                                }
                                |>Seq.iter (function tbl -> tbl.Delete())
                                ctx.SubmitUpdates()
                                printfn"Database is cleaned successfully.."
    |_ -> printfn"insufficient privileges to complete the operation!."
    

