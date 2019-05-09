using System.Collections.Generic;

namespace KillOrHeal.Data.Entities
{
    public class GameStateDto
    {
        public int MapSize { get; }
        public List<PlayerState> Players { get; }
        public List<int> Factions { get; }

        public GameStateDto(int mapSize, IEnumerable<PlayerState> players, IEnumerable<int> factions)
        {
            MapSize = mapSize;
            Players = new List<PlayerState>(players);
            Factions = new List<int>(factions);
        }
    }
}