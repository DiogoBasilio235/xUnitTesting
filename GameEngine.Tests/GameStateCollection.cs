using System;
using Xunit;

namespace GameEngine.Tests
{
    // Preparing Xunit to use several instances of GameStateFixture.
    // We just use this class to create the CollectionDefinition
    [CollectionDefinition("GameState collection")]
    public class GameStateCollection : ICollectionFixture<GameStateFixture> { }
}
