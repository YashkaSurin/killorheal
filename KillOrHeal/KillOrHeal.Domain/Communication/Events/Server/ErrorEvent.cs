using KillOrHeal.Common;

namespace KillOrHeal.Domain.Communication.Events.Server
{
    public sealed class ErrorEvent: ServerEvent
    {
        public ErrorEvent(string message, int? to = null): base(message)
        {
            To = to;
        }

        public override string Type => Constants.ServerEventTypes.Error;
    }
}