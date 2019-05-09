using System;
using KillOrHeal.Common.Exceptions;
using KillOrHeal.Data.Game;
using KillOrHeal.Domain.Communication.Events.Player;
using KillOrHeal.Domain.Communication.Events.Server;
using Microsoft.Extensions.Logging;

namespace KillOrHeal.Domain.Processing
{
    public class JoinEventProcessor: IPlayerEventProcessor
    {
        private readonly IGame _game;
        private readonly ILogger _logger;

        public JoinEventProcessor(IGame game, ILogger logger)
        {
            _game = game;
            _logger = logger;
        }

        public ServerEvent Process(PlayerEvent e)
        {
            if (!(e is JoinEvent))
            {
                _logger.LogError("JoinEventProcessor received an event of wrong type: " + e.GetType());
                return new ErrorEvent("Internal error.", e.SourceId);
            }

            try
            {
                return new PlayerJoinedEvent(_game.SpawnNewPlayer());
            }
            catch (GameOvercrowdedException gameOvercrowdedException)
            {
                return new ErrorEvent(gameOvercrowdedException.Message);
            }
        }
    }
}