using System;
using System.Collections.Generic;
using KillOrHeal.Data.Game;
using KillOrHeal.Domain.Communication.Events.Player;
using KillOrHeal.Domain.Processing.Rules.Transformation;
using KillOrHeal.Domain.Processing.Rules.Validation;
using Microsoft.Extensions.Logging;

namespace KillOrHeal.Domain.Processing
{
    public class PlayerEventProcessorResolver: IPlayerEventProcessorResolver
    {
        private readonly Dictionary<Type, Func<IPlayerEventProcessor>> _processors;

        public PlayerEventProcessorResolver(IGame game, ILogger<PlayerEventProcessorResolver> logger)
        {
            var attackValidationRules = new List<IPlayerActionValidationRule>
            {
                new SelfAttackValidationRule(),
                new AllyAttackValidationRule(),
                new DeadTargetValidationRule(),
                new CombatRangeValidationRule()
            };

            var attackTransformationRule = new AttackLevelTransformationRule();

            var healingValidationRules = new List<IPlayerActionValidationRule>
            {
                new DeadTargetValidationRule(),
                new EnemyHealingValidationRule()
            };

            _processors =
                new Dictionary<Type, Func<IPlayerEventProcessor>>
                {
                    [typeof(AttackEvent)] = () => new AttackEventProcessor(attackValidationRules, attackTransformationRule, logger, game),
                    [typeof(HealingEvent)] = () => new HealingEventProcessor(healingValidationRules, null, logger, game),
                    [typeof(JoinEvent)] = () => new JoinEventProcessor(game, logger),
                    [typeof(JoinFactionEvent)] = () => new JoinFactionEventProcessor(game, logger),
                    [typeof(LeaveFactionEvent)] = () => new LeaveFactionEventProcessor(game, logger)
                };
        }

        public IPlayerEventProcessor Resolve(Type eventType)
        {
            return _processors[eventType]();
        }
    }
}