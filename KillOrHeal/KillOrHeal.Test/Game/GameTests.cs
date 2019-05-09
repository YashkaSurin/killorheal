using System.Collections.Generic;
using System.Linq;
using KillOrHeal.Common.Exceptions;
using KillOrHeal.Data.Entities;
using KillOrHeal.Data.Game;
using Xunit;

namespace KillOrHeal.Test.Game
{
    public class GameTests
    {
        [Fact]
        public void GameShouldSpawnNewPlayer()
        {
            var game = CreateNewGame(5);
            var player = game.SpawnNewPlayer();
            Assert.Equal(1, player.PlayerId);
            Assert.Equal(1000, player.Health);
            Assert.Equal(1, player.Factions.Count);
            Assert.InRange(player.Coordinates.X, 0, 9);
            Assert.InRange(player.Coordinates.Y, 0, 9);
        }

        [Fact]
        public void GameShouldNotSpawnNewPlayerOverLimit()
        {
            var game = CreateNewGame(0);
            Assert.Throws<GameOvercrowdedException>(() => game.SpawnNewPlayer());
        }

        [Fact]
        public void GameShouldSpawnNewPlayerAtFreePlace()
        {
            var game = CreateNewGame(20);
            for (var i = 0; i < 20; i++)
            {
                game.SpawnNewPlayer();
            }

            var fullState = game.GetFullState();
            Assert.Equal(fullState.Players.Count, fullState.Players.GroupBy(p => p.Coordinates).Count());
        }

        [Fact]
        public void GameShouldReturnFullState()
        {
            var game = CreateNewGame(2);
            game.SpawnNewPlayer();
            game.SpawnNewPlayer();
            var state = game.GetFullState();
            Assert.Equal(10, state.MapSize);
            Assert.Equal(2, state.Players.Count);

            var player1 = state.Players[0];
            Assert.Equal(1, player1.PlayerId);
            Assert.Equal(1000, player1.Health);

            var player2 = state.Players[1];
            Assert.Equal(2, player2.PlayerId);
            Assert.Equal(1000, player2.Health);
        }

        [Fact]
        public void GameShouldReturnPlayerById()
        {
            var game = CreateNewGame(5);
            game.SpawnNewPlayer();
            game.SpawnNewPlayer();
            var player2 = game.GetPlayerStateById(2);
            Assert.Equal(2, player2.PlayerId);
        }

        [Fact]
        public void GameShouldThrowPlayerNotFoundException()
        {
            var game = CreateNewGame(5);
            Assert.Throws<PlayerNotFoundException>(() => game.GetPlayerStateById(1));
        }

        [Fact]
        public void GameShouldUpdatePlayerState()
        {
            var game = CreateNewGame(1);
            var player = game.SpawnNewPlayer();
            game.UpdatePlayerState(new PlayerState(player.PlayerId, 2, 500, CombatType.Melee, new List<int>(), player.Coordinates));
            player = game.GetPlayerStateById(1);
            Assert.Equal(500, player.Health);
            Assert.Equal(2, player.Level);
        }

        [Fact]
        public void PlayerCanJoinFaction()
        {
            var game = CreateNewGame(1);
            var player = game.SpawnNewPlayer();
            var faction = player.Factions[0] == 1 ? 2 : 1;
            game.UpdatePlayerState(new PlayerState(player.PlayerId, 1, 1000, CombatType.Melee, new List<int>{ player.Factions[0], faction }, player.Coordinates));
            player = game.GetPlayerStateById(1);
            Assert.Equal(2, player.Factions.Count);
        }

        [Fact]
        public void PlayerCannotJoinFactionTwice()
        {
            var game = CreateNewGame(1);
            var player = game.SpawnNewPlayer();
            game.UpdatePlayerState(new PlayerState(player.PlayerId, 1, 1000, CombatType.Melee, new List<int> { player.Factions[0], player.Factions[0] }, player.Coordinates));
            player = game.GetPlayerStateById(1);
            Assert.Equal(1, player.Factions.Count);
        }

        [Fact]
        public void PlayerCanLeaveFaction()
        {
            var game = CreateNewGame(1);
            var player = game.SpawnNewPlayer();
            game.UpdatePlayerState(new PlayerState(player.PlayerId, 1, 1000, CombatType.Melee, new List<int>(), player.Coordinates));
            player = game.GetPlayerStateById(1);
            Assert.Equal(0, player.Factions.Count);
        }

        private IGame CreateNewGame(int maxPlayersCount)
        {
            return new Data.Game.Game(10, maxPlayersCount, 1000, 5);
        }
    }
}

