module View

open Suave.Form
open Suave.Html
open Suave
open Form
open Db

let login =
    html [] [
        head [] [
            title [] "Web-based Chat"
           // cssLink "/Website.css"
        ]

        body [] [
            div ["id", "header"] [
                    
                tag "form" ["method", "POST"]  [
                       
                        tag "fieldset" [] [
                            div["class", "editor-label"] [
                                Text "Username : "
                            ]
                            div["class", "editor-field"] [
                                Suave.Form.input (fun f -> <@ f.UserName @>) [] Form.register
                            ]
                            div["class", "editor-label"] [
                                Text "Password : "
                            ]
                            div["class", "editor-field"] [
                                Suave.Form.input (fun f -> <@ f.Password @>) [] Form.register
                            ]
                        ]
                        //input["type", "submit"; "value", "Login"]
                ]                   
            ]

            div ["id", "footer"] [
                a "/register" [] [Text "Register"]
            ]
        ]
    ]
    |> htmlToString


let register =
    html [] [
        head [] [
            title [] "Web-based Chat"
            //cssLink "/Website.css"
        ]
        body [] [
            div ["id", "header"] [
                  
                tag "form" ["method", "POST"]  [
                     
                    tag "fieldset" [] [
                        div["class", "editor-label"] [
                            Text "Enter Username : "
                        ]
                        div["class", "editor-field"] [
                            Suave.Form.input (fun f -> <@ f.UserName @>) [] Form.register
                        ]
                        div["class", "editor-label"] [
                            Text "Enter Password : "
                        ]
                        div["class", "editor-field"] [
                            Suave.Form.input (fun f -> <@ f.Password @>) [] Form.register
                        ]
                    ]
                    //input["type", "submit"; "value", "Register"]
                ]                   
            ]
            div ["id", "footer"] [
                a "/login" [] [Text "Login"]
            ]
        ]
    ]
    |> htmlToString
(*
let chatroomList (chatroom : Db.Cht) = [
    html [] [
        tag "h2" [] ["chatroom title here"]
        div ["id", "chatroom list"] [
            for (caption,t) in [ "Chat room: ", chatroom] ->
                p [] [
                    tag "em" [] [Text caption] 
                    Text t
                ]       
            yield p ["class", "button"] [
                a  [] [Text "Enter chat"]
            ]
        ]
    ]
    |>htmlToString
]

let chatRoom (chtroomMsg : Db.openedChatRooms) = [
    html [] [
        tag "h2" [] ["Welcome to the chat room"]
        div ["id", "chatroom messaging"] [
            li [chtroomMsg]
                  
            yield p ["class", "button"] [
                a  [] [Text "Send message"]
            ]
        ]
    ]
    |>htmlToString
]
*)