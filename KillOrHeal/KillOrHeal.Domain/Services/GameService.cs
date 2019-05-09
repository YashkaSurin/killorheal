using KillOrHeal.Data.Entities;
using KillOrHeal.Data.Game;
using KillOrHeal.Domain.Communication;
using KillOrHeal.Domain.Communication.Events.Server;

namespace KillOrHeal.Domain.Services
{
    public class GameService: IGameService
    {
        private readonly IGame _game;
        private readonly IEventQueue<ServerEvent> _serverEventQueue;

        public GameService(IGame game, IEventQueue<ServerEvent> serverEventQueue)
        {
            _game = game;
            _serverEventQueue = serverEventQueue;
        }

        public PlayerState SpawnNewPlayer()
        {
            var player = _game.SpawnNewPlayer(); // TODO handle exceptions
            _serverEventQueue.Enqueque(new PlayerJoinedEvent(player));
            return player;
        }
    }
}
