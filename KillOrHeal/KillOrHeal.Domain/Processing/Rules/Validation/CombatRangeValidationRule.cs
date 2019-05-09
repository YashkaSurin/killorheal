using System;
using KillOrHeal.Common;
using KillOrHeal.Data.Entities;

namespace KillOrHeal.Domain.Processing.Rules.Validation
{
    public class CombatRangeValidationRule: IPlayerActionValidationRule
    {
        public ValidationResult ValidateAction(PlayerState actor, PlayerState target)
        {
            var maxRange = actor.CombatType == CombatType.Melee ? 2 : 20;
            var range = Math.Sqrt(Math.Pow(target.Coordinates.X - actor.Coordinates.X, 2)
                                  + Math.Pow(target.Coordinates.Y - actor.Coordinates.Y, 2));

            if (range > maxRange)
            {
                return ValidationResult.Error(Constants.Messages.TargetIsTooFar);
            }

            return ValidationResult.Success();
        }
    }
}
