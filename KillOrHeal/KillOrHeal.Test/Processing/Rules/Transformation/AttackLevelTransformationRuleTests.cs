using System.Collections.Generic;
using KillOrHeal.Data.Entities;
using KillOrHeal.Domain.Processing.Rules.Transformation;
using Xunit;

namespace KillOrHeal.Test.Processing.Rules.Transformation
{
    public class AttackLevelTransformationRuleTests
    {
        [Fact]
        public void DamageShouldDecreaseIfDefenderIsCool()
        {
            var rule = new AttackLevelTransformationRule();
            var actor = new PlayerState(1, 5, 1000, CombatType.Melee, new List<int>(), new Coordinates(0, 0));
            var target = new PlayerState(2, 10, 1000, CombatType.Melee, new List<int>(), new Coordinates(2, 2));
            var transformedDamage = rule.Transform(5, actor, target);
            Assert.Equal(2.5, transformedDamage);
        }

        [Fact]
        public void DamageShouldNotChangeIfDefenderIsOkay()
        {
            var rule = new AttackLevelTransformationRule();
            var actor = new PlayerState(1, 5, 1000, CombatType.Melee, new List<int>(), new Coordinates(0, 0));
            var target = new PlayerState(2, 9, 1000, CombatType.Melee, new List<int>(), new Coordinates(2, 2));
            var transformedDamage = rule.Transform(5, actor, target);
            Assert.Equal(5, transformedDamage);
        }

        [Fact]
        public void DamageShouldIncreaseIfDefenderSucks()
        {
            var rule = new AttackLevelTransformationRule();
            var actor = new PlayerState(1, 10, 1000, CombatType.Melee, new List<int>(), new Coordinates(0, 0));
            var target = new PlayerState(2, 5, 1000, CombatType.Melee, new List<int>(), new Coordinates(2, 2));
            var transformedDamage = rule.Transform(5, actor, target);
            Assert.Equal(7.5, transformedDamage);
        }
    }
}
