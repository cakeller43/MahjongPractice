open UseCases

// To test just run main and input id's of tiles to discard(play) them. 
// If everything is working... 
//      all the players should have 13 tiles 
//      the discard pile should contain all the discarded tiles
//      the deck should also be reducing whenever a tile is drawn.
// I have just been setting break points and inspecting the GameState to test for now.
[<EntryPoint>]
let main argv = 
    let endGameState = play
    0
