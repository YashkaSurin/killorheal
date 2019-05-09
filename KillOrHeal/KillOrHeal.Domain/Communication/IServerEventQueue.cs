using KillOrHeal.Domain.Communication.Events.Server;

namespace KillOrHeal.Domain.Communication
{
    /// <summary>
    /// Queue for the server events (change of the game state).
    /// </summary>
    public interface IServerEventQueue: IEventQueue<ServerEvent>
    {
    }
}
