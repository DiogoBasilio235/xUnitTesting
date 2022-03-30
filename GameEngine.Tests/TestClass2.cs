using System;
using Xunit;
using Xunit.Abstractions;

// Class created to show the use of the same Instance of GameStateFixture accross several classes with XUnit
namespace GameEngine.Tests
{
    // When we use the Collection attribute, we dont need to implement a specific Interface in our test class.
    // We just need to apply the Collection attribute
    // Before of the tests, Xunit will create an instance of GameStateFixture and supply it to TestClass1 and TestClass2
    // and reuse the same GameStateFixture and consequently the same GameState
    [Collection("GameState collection")]
    public class TestClass2
    {

        private readonly GameStateFixture _gameStateFixture;
        private readonly ITestOutputHelper _output;

        public TestClass2(GameStateFixture gameStateFixture, ITestOutputHelper output)
        {
            _gameStateFixture = gameStateFixture;
            _output = output;
        }

        [Fact]
        public void Test3()
        {
            _output.WriteLine($"GameState Id = {_gameStateFixture.State.Id}");
        }

        [Fact]
        public void Test4()
        {
            _output.WriteLine($"GameState Id = {_gameStateFixture.State.Id}");
        }
    }
}
