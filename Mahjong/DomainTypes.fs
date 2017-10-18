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
        member this.tryDiscardTile tile = 
            this.Hand.Remove tile
        member this.tryFind id =
            this.Hand |> Set.toList |> List.tryFind (fun x -> x.Id = id)

type Deck = { Deck: Tile list}
    with
        member this.tryTakeTile =
            match this.Deck.IsEmpty with
            | true -> (None, this)
            | false -> (Some this.Deck.Head, { this with Deck = this.Deck.Tail })

        // Question: Seems like there must be a better way to use the tryTakeTile method but also not need to mutate to pass state along... fold maybe?
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

type GameStatus = InProgress | Ending

type GameState = { Deck: Deck; Discard: Tile list; Players: Player list; GameStatus: GameStatus ; CurrentPlayer: int}
    with 
        member this.getCurrentPlayer =
            this.Players.[this.CurrentPlayer]
        member this.advanceCurrentPlayer =
            let nextPlayer = 
                match this.CurrentPlayer with
                | 3 -> 0
                | i -> i + 1 
            { this with CurrentPlayer = nextPlayer }
        member this.updatePlayer player =
            List.map (fun x -> if player.Id = x.Id then player else x ) this.Players
        static member empty = 
            { Deck = { Deck = List.Empty }; Discard = List.Empty; Players = List.empty; GameStatus = InProgress; CurrentPlayer = 0}