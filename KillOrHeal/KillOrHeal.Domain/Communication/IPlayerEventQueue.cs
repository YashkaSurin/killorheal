using KillOrHeal.Domain.Communication.Events.Player;

namespace KillOrHeal.Domain.Communication
{
    /// <summary>
    /// Queue for the player events (those triggered by player).
    /// </summary>
    public interface IPlayerEventQueue: IEventQueue<PlayerActionEvent>
    {
    }
}
