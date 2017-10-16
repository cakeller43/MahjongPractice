module UseCases

open DomainFunctions

let startGame player =
    createTileSet
    |> shuffle
    |> drawStartingHand player
    |> i
