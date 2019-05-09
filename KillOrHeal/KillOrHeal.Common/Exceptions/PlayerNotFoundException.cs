using System;

namespace KillOrHeal.Common.Exceptions
{
    public class PlayerNotFoundException: Exception
    {
        public PlayerNotFoundException(int playerId)
        {
            PlayerId = playerId;
        }

        public int PlayerId { get; }
        public override string Message => string.Format(Constants.Messages.PlayerNotFoundTemplate, PlayerId);
    }
}