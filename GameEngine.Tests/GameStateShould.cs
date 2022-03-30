using System;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    public class GameStateShould : IClassFixture<GameStateFixture>
    {
        // Creating an instance of the GameStateFixture
        private readonly GameStateFixture _gameStateFixture;
        private readonly ITestOutputHelper _output;

        public GameStateShould(GameStateFixture gameStateFixture, ITestOutputHelper output)
        {
            _gameStateFixture = gameStateFixture;
            _output = output;
        }


        [Fact]
        public void DamageAllPlayersWhenEarthquake()
        {
            _output.WriteLine($"GameState Id = {_gameStateFixture.State.Id}");

            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();

            _gameStateFixture.State.Players.Add(player1);
            _gameStateFixture.State.Players.Add(player2);

            var expectedHealthAfterHearthquake = player1.Health - GameState.EarthquakeDamage;

            // As we are sharing the same instance on several tests, we have to make sure that it wont break any other tests.
            _gameStateFixture.State.Earthquake();

            Assert.Equal(expectedHealthAfterHearthquake, player1.Health);
            Assert.Equal(expectedHealthAfterHearthquake, player2.Health);


        }

        [Fact]
        public void Reset()
        {
            _output.WriteLine($"GameState Id = {_gameStateFixture.State.Id}");

            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();

            _gameStateFixture.State.Players.Add(player1);
            _gameStateFixture.State.Players.Add(player2);

            _gameStateFixture.State.Reset();

            Assert.Empty(_gameStateFixture.State.Players);

        }
    }
}
