module UseCases

open DomainFunctions
open DomainTypes
open UIFunctions

// I want this method to be in domain functions, but it is coupled with the ui and I do not want ui functions in the domain. hmm
let rec private midTurn (gameState:GameState) = 
    let input = getPlayerInput gameState.getCurrentPlayer.Hand "Quit(Q) or Discard Tile By Id(#)"
    match input with
    | "Q" -> quitGame gameState
    | i ->
        let tileToDiscard = gameState.getCurrentPlayer.Hand |> Set.toList |> List.tryFind (fun x -> x.Id = (i |> int)) // casting to int here throws an exception if a non int is given. validate somewhere.
        let curPlayer = gameState.getCurrentPlayer
    
        match tileToDiscard with
        | Some t -> 
            let players = { curPlayer with Hand = curPlayer.tryDiscardTile t } |> gameState.updatePlayer
            { gameState with Discard = t :: gameState.Discard; Players = players }
        | None -> midTurn gameState


let private startGame = createDeck >> drawStartingHands
let private takeTurn = startTurn >> midTurn >> endTurn

// game loop
let rec private runGame (prevGameState:GameState) = 
    let gameState = takeTurn prevGameState
    match gameState.GameStatus with
    | InProgress -> runGame gameState
    | Ending -> gameState

let private playGame = startGame >> runGame
let play = GameState.empty |> playGame