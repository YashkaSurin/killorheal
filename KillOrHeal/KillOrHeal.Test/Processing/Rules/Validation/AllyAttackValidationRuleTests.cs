using System.Collections.Generic;
using KillOrHeal.Data.Entities;
using KillOrHeal.Domain.Processing.Rules.Validation;
using Xunit;

namespace KillOrHeal.Test.Processing.Rules.Validation
{
    public class AllyAttackValidationRuleTests
    {
        [Fact]
        public void CanAttackEnemy()
        {
            var rule = new AllyAttackValidationRule();
            var actor = new PlayerState(1, 1, 1000, CombatType.Melee, new List<int>(1), new Coordinates(0, 0));
            var target = new PlayerState(2, 1, 1000, CombatType.Melee, new List<int>(2), new Coordinates(0, 1));
            Assert.True(rule.ValidateAction(actor, target).IsValid);
        }

        [Fact]
        public void CannotAttackAlly()
        {
            var rule = new AllyAttackValidationRule();
            var actor = new PlayerState(1, 1, 1000, CombatType.Melee, new List<int> { 1, 2 }, new Coordinates(0, 0));
            var target = new PlayerState(1, 1, 1000, CombatType.Melee, new List<int> { 2, 3 }, new Coordinates(0, 0));
            Assert.False(rule.ValidateAction(actor, target).IsValid);
        }
    }
}
