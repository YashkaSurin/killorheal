using System.Collections.Generic;
using KillOrHeal.Common.Exceptions;
using KillOrHeal.Data.Entities;
using KillOrHeal.Data.Game;
using Moq;

namespace KillOrHeal.Test.Mocks
{
    public static class GameMocks
    {
        public static Mock<IGame> TwoPlayers()
        {
            var game = new Mock<IGame>();
            game.Setup(g => g.GetPlayerStateById(1)).Returns(new PlayerState(1, 1, 1000, CombatType.Ranged, new List<int>(),  new Coordinates(0, 0)));
            game.Setup(g => g.GetPlayerStateById(2)).Returns(new PlayerState(2, 1, 1000, CombatType.Ranged, new List<int>(), new Coordinates(2, 2)));
            return game;
        }

        public static Mock<IGame> TwoPlayersOneDamaged()
        {
            var game = new Mock<IGame>();
            game.Setup(g => g.GetPlayerStateById(1)).Returns(new PlayerState(1, 1, 1000, CombatType.Ranged, new List<int>(), new Coordinates(0, 0)));
            game.Setup(g => g.GetPlayerStateById(2)).Returns(new PlayerState(2, 1, 500, CombatType.Ranged, new List<int>(), new Coordinates(2, 2)));
            return game;
        }

        public static Mock<IGame> TwoPlayersOneDead()
        {
            var game = new Mock<IGame>();
            game.Setup(g => g.GetPlayerStateById(1)).Returns(new PlayerState(1, 1, 1000, CombatType.Ranged, new List<int>(), new Coordinates(0, 0)));
            game.Setup(g => g.GetPlayerStateById(2)).Returns(new PlayerState(2, 1, 0, CombatType.Ranged, new List<int>(), new Coordinates(2, 2)));
            return game;
        }

        public static Mock<IGame> Joinable()
        {
            var game = new Mock<IGame>();
            game.Setup(g => g.SpawnNewPlayer()).Returns(new PlayerState(1, 1, 1000, CombatType.Ranged, new List<int>(), new Coordinates(0, 0)));
            return game;
        }

        public static Mock<IGame> Overcrowded()
        {
            var game = new Mock<IGame>();
            game.Setup(g => g.SpawnNewPlayer()).Throws<GameOvercrowdedException>();
            return game;
        }

        public static Mock<IGame> Empty()
        {
            var game = new Mock<IGame>();
            game.Setup(g => g.GetPlayerStateById(1)).Throws(new PlayerNotFoundException(1));
            game.Setup(g => g.GetPlayerStateById(2)).Throws(new PlayerNotFoundException(2));
            return game;
        }
    }
}