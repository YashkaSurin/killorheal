using System;
using System.Collections.Generic;
using KillOrHeal.Data.Entities;
using KillOrHeal.Data.Game;
using KillOrHeal.Domain.Communication.Events.Server;
using KillOrHeal.Domain.Processing.Rules.Transformation;
using KillOrHeal.Domain.Processing.Rules.Validation;
using Microsoft.Extensions.Logging;

namespace KillOrHeal.Domain.Processing
{
    public class AttackEventProcessor: PlayerHealthChangeEventProcessorBase
    {
        private readonly Random _randomDamageGenerator = new Random();

        public AttackEventProcessor(List<IPlayerActionValidationRule> validationRules, IEffectTransformationRule<double> transformationRule, ILogger logger, IGame game) : base(validationRules, transformationRule, logger, game)
        {
        }

        protected override double CalculateBaseHealthChange()
        {
            return _randomDamageGenerator.Next(-1000, -500);
        }

        protected override ServerEvent CreateResultEvent(PlayerState actor, PlayerState target, double healthDiff, double newHealth)
        {
            return new PlayerDamagedEvent(target.PlayerId, actor.PlayerId, -healthDiff, newHealth);
        }
    }
}