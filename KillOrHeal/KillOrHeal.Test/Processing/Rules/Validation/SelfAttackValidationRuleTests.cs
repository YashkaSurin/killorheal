using System.Collections.Generic;
using KillOrHeal.Data.Entities;
using KillOrHeal.Domain.Processing.Rules.Validation;
using Xunit;

namespace KillOrHeal.Test.Processing.Rules.Validation
{
    public class SelfAttackValidationRuleTests
    {
        [Fact]
        public void CanAttackEnemy()
        {
            var rule = new SelfAttackValidationRule();
            var actor = new PlayerState(1, 1, 1000, CombatType.Melee, new List<int>(), new Coordinates(0, 0));
            var target = new PlayerState(2, 1, 1000, CombatType.Melee, new List<int>(), new Coordinates(0, 1));
            Assert.True(rule.ValidateAction(actor, target).IsValid);
        }

        [Fact]
        public void CannotAttackSelf()
        {
            var rule = new SelfAttackValidationRule();
            var actor = new PlayerState(1, 1, 1000, CombatType.Melee, new List<int>(), new Coordinates(0, 0));
            Assert.False(rule.ValidateAction(actor, actor).IsValid);
        }
    }
}
