﻿using Newtonsoft.Json;

namespace KillOrHeal.Domain.Communication.Events.Player
{
    /// <summary>
    /// Base class for all events that are generated by players.
    /// </summary>
    public class PlayerEvent: BaseEvent
    {
        /// <summary>
        /// Source id (for now, it's a player id). Used to route response event in case it's not public (all events but error are public currently).
        /// </summary>
        [JsonIgnore]
        public virtual int? SourceId => null;
    }
}