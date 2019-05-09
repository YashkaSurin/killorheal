using System.Collections.Generic;

namespace KillOrHeal.Data.Entities
{
    public class PlayerState
    {
        public int PlayerId { get; }
        public int Level { get; }
        public double Health { get; }
        public CombatType CombatType { get; }
        public Coordinates Coordinates { get; }
        public List<int> Factions { get; }
        public bool IsAlive => Health > 0;

        public PlayerState(int playerId, int level, double health, CombatType combatType, List<int> factions, Coordinates coordinates)
        {
            PlayerId = playerId;
            Level = level;
            Health = health;
            Factions = new List<int>(factions);
            Coordinates = coordinates;
            CombatType = combatType;
        }


        public PlayerState Clone()
        {
            return new PlayerState(PlayerId, Level, Health, CombatType, new List<int>(Factions),  Coordinates);
        }
    }
}