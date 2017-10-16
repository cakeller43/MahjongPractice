open UseCases

let rec inputPrompt actions message = 
    printfn "%s"  (message: string)

    match stdin.ReadLine () with
    | input -> actions input
    | _ -> inputPrompt actions message

let inputRules input =
    match input with
    | "Q" -> Some "Quit"
    | "1" -> Some "Start"
    | _ -> None


[<EntryPoint>]
let main argv = 
    let input = inputPrompt inputRules "1) Start Game \nQ) Quit"
    match input with 
    | Some "Quit" -> 0
    | Some _x -> 
        let hand,_deck = startGame "Corey"
        printfn "%A" hand
        1
    | None -> -1
