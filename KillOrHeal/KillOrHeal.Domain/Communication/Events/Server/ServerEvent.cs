﻿using System;

namespace KillOrHeal.Domain.Communication.Events.Server
{
    /// <summary>
    /// Base class for all events that are generated by server and broadcasted to the players.
    /// </summary>
    public class ServerEvent: BaseEvent
    {
        public ServerEvent(string message)
        {
            Message = message;
        }

        public ServerEvent(string message, DateTime timestamp)
        {
            Message = message;
            Timestamp = timestamp;
        }

        public string Message { get; }
        public virtual string Type => "Info";
        public virtual int? To { get; protected set; }
    }
}