using System;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    public class GameStateShould
    {
        [Fact]
        public void DamageAllPlayersWhenEarthquake()
        {
            var sut = new GameState();

            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();

            sut.Players.Add(player1);
            sut.Players.Add(player2);


        }
    }
}
