using KillOrHeal.Domain.Communication.Events;

namespace KillOrHeal.Domain.Communication
{
    /// <summary>
    /// Events queue with priority on Timestamp.
    /// </summary>
    public interface IEventQueue<TEvent> where TEvent: BaseEvent
    {
        void Enqueque(TEvent e);
        bool TryDequeue(out TEvent result);
    }
}
