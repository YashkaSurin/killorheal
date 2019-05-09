using System;
using KillOrHeal.Common;
using KillOrHeal.Common.Exceptions;
using KillOrHeal.Data.Entities;
using KillOrHeal.Data.Game;
using KillOrHeal.Domain.Communication.Events.Player;
using KillOrHeal.Domain.Communication.Events.Server;
using Microsoft.Extensions.Logging;

namespace KillOrHeal.Domain.Processing
{
    public class LeaveFactionEventProcessor: IPlayerEventProcessor
    {
        private readonly IGame _game;
        private readonly ILogger _logger;

        public LeaveFactionEventProcessor(IGame game, ILogger logger)
        {
            _game = game;
            _logger = logger;
        }

        public ServerEvent Process(PlayerEvent e)
        {
            if (!(e is LeaveFactionEvent typedEvent))
            {
                _logger.LogError("LeaveFactionEventProcessor received an event of wrong type: " + e.GetType());
                return new ErrorEvent("Internal error.", e.SourceId);
            }

            PlayerState player;

            try
            {
                player = _game.GetPlayerStateById(typedEvent.PlayerId);
            }
            catch (PlayerNotFoundException exception)
            {
                return new ErrorEvent(exception.Message, e.SourceId);
            }

            if (!player.Factions.Contains(typedEvent.FactionId))
            {
                return new ErrorEvent(string.Format(Constants.Messages.NotInThisFactionTemplate, typedEvent.FactionId), typedEvent.PlayerId);
            }

            player.Factions.Remove(typedEvent.FactionId);
            _game.UpdatePlayerState(player);

            return new PlayerLeftFactionEvent(player, typedEvent.FactionId);
        }
    }
}
