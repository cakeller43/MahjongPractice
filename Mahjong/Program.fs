open UseCases

let rec inputPrompt actions message = 
    printfn "%s"  (message: string)

    match stdin.ReadLine () with
    | input -> actions input
    | _ -> inputPrompt actions message

[<EntryPoint>]
let main argv = 
    let x = playGame
    0
