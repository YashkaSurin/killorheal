using System;
using System.Collections.Generic;
using KillOrHeal.Common.Exceptions;
using KillOrHeal.Data.Entities;
using KillOrHeal.Data.Game;
using KillOrHeal.Domain.Communication.Events.Player;
using KillOrHeal.Domain.Communication.Events.Server;
using KillOrHeal.Domain.Processing.Rules.Transformation;
using KillOrHeal.Domain.Processing.Rules.Validation;
using Microsoft.Extensions.Logging;

namespace KillOrHeal.Domain.Processing
{
    public abstract class PlayerHealthChangeEventProcessorBase: IPlayerEventProcessor
    {
        private readonly IGame _game;
        private readonly List<IPlayerActionValidationRule> _validationRules;
        private readonly IEffectTransformationRule<double> _transformationRule;
        private readonly ILogger _logger;

        protected PlayerHealthChangeEventProcessorBase(List<IPlayerActionValidationRule> validationRules, IEffectTransformationRule<double> transformationRule, ILogger logger, IGame game)
        {
            _validationRules = validationRules;
            _transformationRule = transformationRule;
            _logger = logger;
            _game = game;
        }

        public ServerEvent Process(PlayerEvent e)
        {
            if (!(e is PlayerActionEvent actionEvent))
            {
                _logger.LogError("AttackEventProcessor received an event of wrong type: " + e.GetType());
                return new ErrorEvent("Internal error.");
            }

            PlayerState actor;
            PlayerState target;

            try
            {
                actor = _game.GetPlayerStateById(actionEvent.ActorId);
                target = _game.GetPlayerStateById(actionEvent.TargetId);
            }
            catch (PlayerNotFoundException exception)
            {
                return new ErrorEvent(exception.Message, actionEvent.ActorId);
            }

            foreach (var rule in _validationRules)
            {
                var validationResult = rule.ValidateAction(actor, target);
                if (!validationResult.IsValid)
                {
                    return new ErrorEvent(validationResult.ErrorMessage, actionEvent.ActorId);
                }
            }

            var healthChange = CalculateBaseHealthChange();
            if (_transformationRule != null)
            {
                healthChange = _transformationRule.Transform(healthChange, actor, target);
            }

            var newHealth = target.Health + healthChange;
            if (newHealth < 0)
            {
                newHealth = 0;
            }
            else if (newHealth > 1000)
            {
                newHealth = 1000;
            }

            var newState = new PlayerState(target.PlayerId, target.Level, newHealth, target.CombatType, target.Factions, target.Coordinates);
            _game.UpdatePlayerState(newState);
            return CreateResultEvent(actor, target, healthChange, newHealth);
        }

        protected abstract double CalculateBaseHealthChange();
        protected abstract ServerEvent CreateResultEvent(PlayerState actor, PlayerState target, double healthDiff, double newHealth);
    }
}
