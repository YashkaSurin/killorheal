namespace KillOrHeal.Common
{
    public static class Constants
    {
        public static class Messages
        {
            public const string PlayerNotFoundTemplate = "Player not found: {0}.";            
            public const string GameOvercrowded = "The game is overcrowded, please try again later or restart the game.";
            public const string HealedTemplate = "Player {0} healed Player {1}.";
            public const string CannotTouchDead = "You cannot do anything with dead people.";
            public const string AttackedTemplate = "Player {0} attacked Player {1}.";
            public const string KilledTemplate = "Player {0} has been killed.";
            public const string CannotAttackSelf = "You cannot attack yourself.";
            public const string CannotAttackAlly = "You cannot attack allies.";
            public const string CannotHealEnemies = "You cannot heal enemies.";
            public const string PlayerJoinedTemplate = "{0} joined the game.";
            public const string TargetIsTooFar = "The target is too far.";

            public const string FactionNotFoundTemplate = "Faction not found: {0}.";
            public const string AlreadyInThisFactionTemplate = "You are already in the faction: {0}.";
            public const string NotInThisFactionTemplate = "You are not in the faction: {0}.";
            public const string PlayerJoinedFactionTemplate = "{0} joined {1}.";
            public const string PlayerLeftFactionTemplate = "{0} left {1}.";
        }

        public static class ServerEventTypes
        {
            public const string Error = "Error";
            public const string PlayerDamaged = "PlayerWasDamaged";
            public const string PlayerHealed = "PlayerWasHealed";
            public const string PlayerJoined = "NewPlayerJoined";
            public const string PlayerJoinedFaction = "PlayerJoinedFaction";
            public const string PlayerLeftFaction = "PlayerLeftFaction";
        }
    }
}