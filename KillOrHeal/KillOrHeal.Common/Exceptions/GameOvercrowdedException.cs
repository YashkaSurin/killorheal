using System;

namespace KillOrHeal.Common.Exceptions
{
    public class GameOvercrowdedException: Exception
    {
        public override string Message => Constants.Messages.GameOvercrowded;
    }
}