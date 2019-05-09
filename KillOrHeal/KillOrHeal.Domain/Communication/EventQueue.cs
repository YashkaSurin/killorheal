using System;
using System.Collections.Generic;
using System.Linq;
using KillOrHeal.Domain.Communication.Events;
using KillOrHeal.Domain.Communication.Events.Server;

namespace KillOrHeal.Domain.Communication
{
    public class EventQueue<TEvent>: IEventQueue<TEvent> where TEvent: BaseEvent
    {
        private readonly SortedDictionary<DateTime, TEvent> _dictionary = new SortedDictionary<DateTime, TEvent>();

        public void Enqueque(TEvent e)
        {
            _dictionary[e.Timestamp] = e;
        }

        public bool TryDequeue(out TEvent result)
        {
            result = null;
            var pair = _dictionary.FirstOrDefault();
            if (pair.Equals(default(KeyValuePair<DateTime, TEvent>)))
            {
                return false;
            }

            result = pair.Value;
            _dictionary.Remove(pair.Key);
            return true;
        }
    }
}