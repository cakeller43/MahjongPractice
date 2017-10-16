module DomainFunctions

open DomainTypes

let createTileSet = 
    [ for suit in suits do
        for rank in ranks do
            yield [ for x in 1..4 do yield { Id=0; TileType=Simple (suit,rank) } ]
      for wind in winds do
        yield [ for x in 1..4 do yield { Id=0; TileType=Wind wind } ]
      for dragon in dragons do
        yield [ for x in 1..4 do yield { Id=0; TileType=Dragon dragon } ]
      for bonus in bonuses do
        yield [ for x in 1..4 do yield { Id=0; TileType=Bonus bonus } ]
      for joker in jokers do
        yield [ for x in 1..8 do yield { Id=0; TileType=Joker joker } ] ]
    |> Seq.collect id
    |> Seq.mapi (fun index elem -> { elem with Id = index })
    |> Array.ofSeq

let shuffle (tileSet:Tile[]) =
    let rand = new System.Random()
    //doing some mutation here, maybe this is fine?
    let swap (arr: _[]) first second =
        let tmp = arr.[first]
        arr.[first] <- arr.[second]
        arr.[second] <- tmp
    
    Array.iteri ( fun i _ -> swap tileSet i (rand.Next(i, Array.length tileSet)) ) tileSet
    tileSet

let drawStartingHand player (tileSet:Tile[]): ( Hand * Deck ) =
    let hand,deck = Array.splitAt 13 tileSet
    ( { HandTiles = hand; Player = player }, { DeckTiles = deck } )
    
