using System;

namespace KillOrHeal.Domain.Communication.Events
{
    public class BaseEvent
    {
        public BaseEvent(): this(DateTime.UtcNow)
        {
        }

        public BaseEvent(DateTime timestamp)
        {
            Timestamp = timestamp;
        }

        public DateTime Timestamp { get; protected set; }
    }
}