module View

open Suave.Form
open Suave.Html
open Form

let h2 s = tag "h2" [] [Text s]
let cssLink href = link [ "href", href; " rel", "stylesheet"; " type", "text/css" ]

let login =
    html [] [
        head [] [
            title [] "Web-based Chat"
            cssLink "Site.css"
        ]

        body [] [
            h2 "SharpApp"

            div ["id", "header"] [
                    
                tag "form" ["method", "POST"]  [                      
                        tag "fieldset" [] [
                            div["class", "editor-label"] [
                                Text "Username : "
                            ]
                            div["class", "editor-field"] [
                                Suave.Form.input (fun f -> <@ f.UserName @>) [] Form.login
                            ]
                            div["class", "editor-label"] [
                                Text "Password : "
                            ]
                            div["class", "editor-field"] [
                                Suave.Form.input (fun f -> <@ f.Password @>) [] Form.login
                            ]
                        ]
                        input["type", "submit"; "value", "Login"]
                ]                   
            ]

            div ["id", "footer"] [
                a "/register" [] [Text "Don't have an account? Register here"]
            ]
            cssLink "Site.css"
        ]
    ]
    |> htmlToString


let register =
    html [] [
        head [] [
            title [] "Web-based Chat"
            cssLink "/Website.css"
        ]
        tag "h2" [] [Text "Create a account"]
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
                    input["type", "submit"; "value", "Register"]
                ]                   
            ]
            div ["id", "footer"] [
                a "/login" [] [Text "Already have an account? Login here"]
            ]
        ]
    ]
    |> htmlToString
(*
let chatroomList (chatroom : Db.Cht) = [
    html [] [
        tag "h2" [] [Text "chatroom title here"]
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

let chatRoom (chtroomMsg : Db.UsrCht) = [
    html [] [
        tag "h2" [] [Text "Welcome to the chat room"]
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