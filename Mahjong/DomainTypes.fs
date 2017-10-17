module DomainTypes

type Suit = | Bamboo | Dot | Character
    with
        static member suits = [Bamboo; Dot; Character]
type Rank = | One | Two | Three | Four | Five | Six | Seven | Eight | Nine 
    with 
        static member ranks = [One; Two; Three; Four; Five; Six; Seven; Eight; Nine]

type Simple = Suit * Rank
type Wind = | East | South | West | North
    with
        static member winds = [East; South; West; North]
type Dragon = | Red | Green | White
    with
        static member dragons = [Red; Green; White]
type Bonus = | Season | Flower
    with
        static member bonuses = [Season; Flower]

type TileType = | Simple of Simple | Wind of Wind | Dragon of Dragon | Bonus of Bonus
type Tile = { Id: int; TileType: TileType}

type Player = { Hand: Tile Set; Id: int }
    with 
        member this.tryAddTile tile = 
            match this.Hand.Count < 14 with
            | true -> { this with Hand = Set.add tile this.Hand }|> Some
            | false -> None

type Deck = { Deck: Tile list}
    with
        member this.tryTakeTile =
            match this.Deck.IsEmpty with
            | true -> (None, this)
            | false -> (Some this.Deck.Head, { this with Deck = this.Deck.Tail })

        // must be a better way to use the tryTakeTile method but also not need to mutate to pass state along... fold maybe?
        member this.drawStartingHand player =
            let mutable deckMut = this;
            let hand = 
                [ for x in 1..13 do
                    yield match deckMut.tryTakeTile with
                            | (Some t, d) -> 
                                deckMut <- d
                                Some t
                            | (None, _d) -> None ]
            ({ player with Hand = (List.filter (fun (x:Tile option) -> x.IsSome) hand) |> List.map (fun x -> x.Value) |> Set.ofList }, deckMut)
        
type GameState = { Deck: Deck; Discard: Tile list; Players: Player list }
    with 
        static member empty = 
            { Deck = { Deck = List.Empty }; Discard = List.Empty; Players = List.Empty}