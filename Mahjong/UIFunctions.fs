module UIFunctions

let rec inputPrompt prompt = 
    printfn "%s"  (prompt: string)
    match stdin.ReadLine () with
    | input -> input

let getPlayerInput displayData prompt = 
    printfn "%A"  displayData
    inputPrompt prompt