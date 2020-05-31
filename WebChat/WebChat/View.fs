module View

open Suave.Form
open Suave.Html
open Suave
open Form

let index =
    html [] [
        head [] [
            title [] "Web-based Chat"
        ]

        body [] [
            div ["id", "header"] [
                tag "h1" [] [
                    a "/" [] [Text "F# Suave Chat"]
                ]
            ]

            div ["id", "footer"] [
                Text "built with "
                a "http://fsharp.org" [] [Text "F#"]
                Text " and "
                a "http://suave.io" [] [Text "Suave.IO"]
            ]
        ]
    ]
    |> htmlToString

