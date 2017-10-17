module UseCases

open DomainFunctions
open DomainTypes

let createDeck (gameState:GameState) =
    let tiles = createTileSet |> shuffle |> List.ofArray 
    { gameState with Deck = { Deck = tiles } }

let drawStartingHands gameState =
    let players,newDeck = createPlayers |> List.mapFold ( fun (state:Deck) el -> state.drawStartingHand el ) gameState.Deck
    { gameState with Players = players; Deck = newDeck }

let startGame = createDeck >> drawStartingHands

// game loop
let runGame gameState = 
    gameState

let playGame = 
    GameState.empty |> startGame |> runGame

