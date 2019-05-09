using System;
using System.Collections.Generic;
using System.Linq;
using KillOrHeal.Common.Exceptions;
using KillOrHeal.Data.Entities;

namespace KillOrHeal.Data.Game
{
    public class Game: IGame
    {
        private readonly int _mapSize;
        private readonly int _maxPlayers;
        private readonly int _defaultHealth;
        private readonly Dictionary<int, PlayerState> _players = new Dictionary<int, PlayerState>();
        private readonly List<int> _factions;
        private readonly Random _randomSpawnPlaceGenerator = new Random();
        private readonly Random _randomCombatTypeGenerator = new Random();
        private readonly Random _randomLevelGenerator = new Random();
        private readonly Random _randomFactionNumberGenerator = new Random();

        public Game(int mapSize, int maxPlayers, int defaultHealth, int factionsCount)
        {
            _mapSize = mapSize;
            _maxPlayers = maxPlayers;
            _defaultHealth = defaultHealth;
            _factions = Enumerable.Range(1, factionsCount).ToList();
        }

        public PlayerState GetPlayerStateById(int id)
        {
            if (!_players.TryGetValue(id, out var player))
            {
                throw new PlayerNotFoundException(id);
            }

            return player.Clone();
        }

        public void UpdatePlayerState(PlayerState player)
        {
            if (!_players.TryGetValue(player.PlayerId, out var existingPlayer))
            {
                throw new PlayerNotFoundException(player.PlayerId);
            }

            var factions = player.Factions.Distinct().ToList();
            var invalidFactions = factions.Where(f => !_factions.Contains(f)).ToList();
            if (invalidFactions.Any())
            {
                throw new FactionNotFoundException(invalidFactions.First());
            }

            _players[player.PlayerId] = new PlayerState(player.PlayerId, player.Level, player.Health, player.CombatType, factions, existingPlayer.Coordinates);            
        }
        

        public GameStateDto GetFullState()
        {
            return new GameStateDto(_mapSize, _players.Values.Select(p => p.Clone()), _factions);
        }

        public PlayerState SpawnNewPlayer()
        {
            if (_players.Count == _maxPlayers)
            {
                throw new GameOvercrowdedException();
            }

            var combatType = (CombatType) _randomCombatTypeGenerator.Next(0, 2);
            var faction = _randomFactionNumberGenerator.Next(1, _factions.Count + 1);

            var occupiedPlaces = _players.Values.Select(p => _mapSize * p.Coordinates.Y + p.Coordinates.X - 1);
            var freePlaces = Enumerable.Range(0, _mapSize * _mapSize - 1).Except(occupiedPlaces).ToArray();
            var place = freePlaces[_randomSpawnPlaceGenerator.Next(0, freePlaces.Length - 1)];

            var player = new PlayerState(
                _players.Count == 0 ? 1 : _players.Values.Max(p => p.PlayerId) + 1,
                _randomLevelGenerator.Next(1, 11),
                _defaultHealth,
                combatType,
                new List<int> { faction }, 
                new Coordinates(place % _mapSize + 1, place / _mapSize)
            );

            _players[player.PlayerId] = player;
            return player.Clone();
        }
    }
}