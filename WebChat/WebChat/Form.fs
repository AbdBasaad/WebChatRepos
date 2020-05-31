module Form

open Suave.Form

type ChatUser = {
    UserName : string
    Password : Password
    ConfirmPassword : Password
}

let pattern = passwordRegex @"(\w)[!@#$%^&*]{6,20}"

let passwordsMatch = (fun f -> f.Password = f.ConfirmPassword), "Passwords must match"

let register : Form<ChatUser> = 
    Form ([ TextProp ((fun f -> <@ f.UserName @>), [maxLength 20])

            PasswordProp ((fun f -> <@ f.Password @>), [ pattern ] )

            PasswordProp ((fun f -> <@ f.ConfirmPassword @>), [ pattern ] )

            ],[ passwordsMatch ])