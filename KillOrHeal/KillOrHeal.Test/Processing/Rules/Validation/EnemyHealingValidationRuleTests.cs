using System.Collections.Generic;
using KillOrHeal.Data.Entities;
using KillOrHeal.Domain.Processing.Rules.Validation;
using Xunit;

namespace KillOrHeal.Test.Processing.Rules.Validation
{
    public class EnemyHealingValidationRuleTests
    {
        [Fact]
        public void CannotHealEnemy()
        {
            var rule = new EnemyHealingValidationRule();
            var actor = new PlayerState(1, 1, 1000, CombatType.Melee, new List<int>(), new Coordinates(0, 0));
            var target = new PlayerState(2, 1, 1000, CombatType.Melee, new List<int>(), new Coordinates(0, 1));
            Assert.False(rule.ValidateAction(actor, target).IsValid);
        }

        [Fact]
        public void CanHealSelf()
        {
            var rule = new EnemyHealingValidationRule();
            var actor = new PlayerState(1, 1, 1000, CombatType.Melee, new List<int>(), new Coordinates(0, 0));
            Assert.True(rule.ValidateAction(actor, actor).IsValid);
        }

        [Fact]
        public void CanHealAlly()
        {
            var rule = new EnemyHealingValidationRule();
            var actor = new PlayerState(1, 1, 1000, CombatType.Melee, new List<int> { 1, 2 }, new Coordinates(0, 0));
            var target = new PlayerState(2, 1, 1000, CombatType.Melee, new List<int> { 2, 3}, new Coordinates(0, 1));
            Assert.True(rule.ValidateAction(actor, target).IsValid);
        }
    }
}
