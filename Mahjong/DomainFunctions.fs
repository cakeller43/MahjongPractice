module DomainFunctions

open DomainTypes

let createTileSet = 
    [ for suit in Suit.suits do
        for rank in Rank.ranks do
            yield [ for x in 1..4 do yield { Id=0; TileType=Simple (suit,rank) } ]
      for wind in Wind.winds do
        yield [ for x in 1..4 do yield { Id=0; TileType=Wind wind } ]
      for dragon in Dragon.dragons do
        yield [ for x in 1..4 do yield { Id=0; TileType=Dragon dragon } ]
      for bonus in Bonus.bonuses do
        yield [ for x in 1..4 do yield { Id=0; TileType=Bonus bonus } ] ]
    |> Seq.collect id
    |> Seq.mapi (fun index elem -> { elem with Id = index })
    |> Array.ofSeq

let shuffle (tileSet:Tile[]) =
    let rand = new System.Random()
    let swap (arr: _[]) first second =
        let tmp = arr.[first]
        arr.[first] <- arr.[second]
        arr.[second] <- tmp
    
    Array.iteri ( fun i _ -> swap tileSet i (rand.Next(i, Array.length tileSet)) ) tileSet
    tileSet

let createPlayers : Player list =
    [| for x in 1..4 do
        yield { Hand = Set.empty; Id = x }|] |> List.ofArray 

let createDeck (gameState:GameState) =
    let tiles = createTileSet |> shuffle |> List.ofArray 
    { gameState with Deck = { Deck = tiles } }

let drawStartingHands gameState =
    let players,newDeck = createPlayers |> List.mapFold ( fun (state:Deck) el -> state.drawStartingHand el ) gameState.Deck
    { gameState with Players = players; Deck = newDeck }

// Summary: Draw Tile then update the players Hand state.
let startTurn gameState =
    let tile,deck = gameState.Deck.tryTakeTile
    let currentPlayer = gameState.getCurrentPlayer
    let playerState =
        match tile with
        | Some t -> 
            match currentPlayer.tryAddTile t with
            | Some p -> p
            | None -> currentPlayer
        | None -> currentPlayer
    let playersState = gameState.updatePlayer playerState
    { gameState with Deck = deck; Players = playersState }

let endTurn (gameState:GameState) =
    gameState.advanceCurrentPlayer

let quitGame gameState = 
    { gameState with GameStatus = Ending }