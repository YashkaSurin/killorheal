namespace KillOrHeal.Domain.Dispatching
{
    /// <summary>
    /// Event dispatcher.
    /// Listens the Player Event Queue, routes player events to corresponding processors and puts results in the Server Event Queue.
    /// </summary>
    public interface IServerEventDispatcher
    {
        void Start();
        void Stop();
    }
}