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
    //doing some mutation here, maybe this is fine?
    let swap (arr: _[]) first second =
        let tmp = arr.[first]
        arr.[first] <- arr.[second]
        arr.[second] <- tmp
    
    Array.iteri ( fun i _ -> swap tileSet i (rand.Next(i, Array.length tileSet)) ) tileSet
    tileSet

let createPlayers : Player list =
    [| for x in 1..4 do
        yield { Hand = Set.empty; Id = x }|] |> List.ofArray 

