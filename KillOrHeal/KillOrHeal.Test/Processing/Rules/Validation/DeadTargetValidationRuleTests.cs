using System.Collections.Generic;
using KillOrHeal.Data.Entities;
using KillOrHeal.Domain.Processing.Rules.Validation;
using Xunit;

namespace KillOrHeal.Test.Processing.Rules.Validation
{
    public class DeadTargetValidationRuleTests
    {
        [Fact]
        public void CannotActOnDeadTarget()
        {
            var rule = new DeadTargetValidationRule();
            var actor = new PlayerState(1, 1, 1000, CombatType.Melee, new List<int>(), new Coordinates(0, 0));
            var target = new PlayerState(2, 1, 0, CombatType.Melee, new List<int>(), new Coordinates(0, 1));
            Assert.False(rule.ValidateAction(actor, target).IsValid);
        }

        [Fact]
        public void CanActOnAliveTarget()
        {
            var rule = new DeadTargetValidationRule();
            var actor = new PlayerState(1, 1, 1000, CombatType.Melee, new List<int>(), new Coordinates(0, 0));
            var target = new PlayerState(2, 1, 1000, CombatType.Melee, new List<int>(), new Coordinates(0, 1));
            Assert.True(rule.ValidateAction(actor, target).IsValid);
        }
    }
}