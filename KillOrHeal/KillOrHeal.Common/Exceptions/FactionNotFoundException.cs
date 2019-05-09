using System;

namespace KillOrHeal.Common.Exceptions
{
    public class FactionNotFoundException: Exception
    {
        public FactionNotFoundException(int factionId)
        {
            FactionId = factionId;
        }

        public int FactionId { get; }
        public override string Message => string.Format(Constants.Messages.FactionNotFoundTemplate, FactionId);
    }
}
