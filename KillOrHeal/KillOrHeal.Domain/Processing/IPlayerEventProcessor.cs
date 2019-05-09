using KillOrHeal.Domain.Communication.Events.Player;
using KillOrHeal.Domain.Communication.Events.Server;

namespace KillOrHeal.Domain.Processing
{
    public interface IPlayerEventProcessor
    {
        ServerEvent Process(PlayerEvent e);
    }
}
