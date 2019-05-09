using System;
using System.Collections.Generic;
using KillOrHeal.Common;
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
    public class HealingEventProcessor: PlayerHealthChangeEventProcessorBase
    {
        private readonly Random _randomHealthBoostGenerator = new Random();

        public HealingEventProcessor(List<IPlayerActionValidationRule> validationRules, IEffectTransformationRule<double> transformationRule, ILogger logger, IGame game) : base(validationRules, transformationRule, logger, game)
        {
        }

        protected override double CalculateBaseHealthChange()
        {
            return _randomHealthBoostGenerator.Next(100, 200);
        }

        protected override ServerEvent CreateResultEvent(PlayerState actor, PlayerState target, double healthDiff, double newHealth)
        {
            return new PlayerHealedEvent(target.PlayerId, actor.PlayerId, healthDiff, newHealth);
        }
    }
}