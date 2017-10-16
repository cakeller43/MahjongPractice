module DomainTypes

type Suit = | Bamboo | Dot | Character
type Rank = | One | Two | Three | Four | Five | Six | Seven | Eight | Nine 
    //with 
    //    static member ranks = [One; Two; Three; Four; Five; Six; Seven; Eight; Nine]

//[<CompilationRepresentation (CompilationRepresentationFlags.ModuleSuffix)>]
//module Rank = 
//    let ranks = [One; Two; Three; Four; Five; Six; Seven; Eight; Nine]

type Simple = Suit * Rank
type Wind = | East | South | West | North
type Dragon = | Red | Green | White
type Bonus = | Season | Flower
type Joker = | Joker

type TileType = | Simple of Simple | Wind of Wind | Dragon of Dragon | Bonus of Bonus | Joker of Joker
type Tile = { Id: int; TileType: TileType}

type Hand = { HandTiles: Tile[]; Player: string }  // Constrain to 14?

//type Player = private { Hand: Set<Tile>; }
//    with 
//        static member tryAddTile tile player =
//            match player.Hand.Count < 14 with
//            | true -> { player with Hand = Set.add tile player.Hand } |> Some
//            | false -> None

type Deck = { DeckTiles: Tile[]; }



// better way to iterate over DU choices?
let suits = [Bamboo; Dot; Character]
let ranks = [One; Two; Three; Four; Five; Six; Seven; Eight; Nine]
let winds = [East; South; West; North]
let dragons = [Red; Green; White]
let bonuses = [Season; Flower]
let jokers = [Joker]

//examples
let t = { Id=1; TileType = Simple (Bamboo, Three) }
let x = { Id=1; TileType = Wind East }
let y = { Id=1; TileType = Dragon Red }
let z = { Id=1; TileType = Bonus Flower }
let c = { Id=1; TileType = Joker Joker.Joker }


