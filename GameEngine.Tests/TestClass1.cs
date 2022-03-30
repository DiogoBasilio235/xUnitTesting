using System;
using Xunit;
using Xunit.Abstractions;

// Class created to show the use of the same Instance of GameStateFixture accross several classes with XUnit
namespace GameEngine.Tests
{
    // When we use the Collection attribute, we dont need to implement a specific Interface in our test class.
    // We just need to apply the Collection attribute
    [Collection("GameState collection")]
    public class TestClass1
    {

        private readonly GameStateFixture _gameStateFixture;
        private readonly ITestOutputHelper _output;

        public TestClass1(GameStateFixture gameStateFixture, ITestOutputHelper output)
        {
            _gameStateFixture = gameStateFixture;
            _output = output;
        }

        [Fact]
        public void Test1()
        {
            _output.WriteLine($"GameState Id = {_gameStateFixture.State.Id}");
        }

        [Fact]
        public void Test2()
        {
            _output.WriteLine($"GameState Id = {_gameStateFixture.State.Id}");
        }
    }
}
