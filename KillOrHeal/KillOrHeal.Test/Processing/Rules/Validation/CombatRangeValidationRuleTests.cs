using System.Collections.Generic;
using KillOrHeal.Data.Entities;
using KillOrHeal.Domain.Processing.Rules.Validation;
using Xunit;

namespace KillOrHeal.Test.Processing.Rules.Validation
{
    public class CombatRangeValidationRuleTests
    {
        [Fact]
        public void MeleeFighterCanAttackInsideHisZone()
        {
            var rule = new CombatRangeValidationRule();
            var actor = new PlayerState(1, 1, 1000, CombatType.Melee, new List<int>(), new Coordinates(0, 0));
            var target = new PlayerState(2, 1, 1000, CombatType.Melee, new List<int>(), new Coordinates(1, 1));
            Assert.True(rule.ValidateAction(actor, target).IsValid);
        }

        [Fact]
        public void MeleeFighterCannotAttackOutsideHisZone()
        {
            var rule = new CombatRangeValidationRule();
            var actor = new PlayerState(1, 1, 1000, CombatType.Melee, new List<int>(), new Coordinates(0, 0));
            var target = new PlayerState(2, 1, 1000, CombatType.Melee, new List<int>(), new Coordinates(2, 2));
            Assert.False(rule.ValidateAction(actor, target).IsValid);
        }

        [Fact]
        public void RangerFighterCanAttackInsideHisZone()
        {
            var rule = new CombatRangeValidationRule();
            var actor = new PlayerState(1, 1, 1000, CombatType.Ranged, new List<int>(), new Coordinates(0, 0));
            var target = new PlayerState(2, 1, 1000, CombatType.Melee, new List<int>(), new Coordinates(10, 10));
            Assert.True(rule.ValidateAction(actor, target).IsValid);
        }

        [Fact]
        public void RangerFighterCannotAttackOutsideHisZone()
        {
            var rule = new CombatRangeValidationRule();
            var actor = new PlayerState(1, 1, 1000, CombatType.Ranged, new List<int>(), new Coordinates(0, 0));
            var target = new PlayerState(2, 1, 1000, CombatType.Melee, new List<int>(), new Coordinates(20, 20));
            Assert.False(rule.ValidateAction(actor, target).IsValid);
        }
    }
}