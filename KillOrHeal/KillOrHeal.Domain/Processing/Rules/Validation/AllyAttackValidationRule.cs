using System;
using System.Linq;
using KillOrHeal.Common;
using KillOrHeal.Data.Entities;

namespace KillOrHeal.Domain.Processing.Rules.Validation
{
    public class AllyAttackValidationRule: IPlayerActionValidationRule
    {
        public ValidationResult ValidateAction(PlayerState actor, PlayerState target)
        {
            if (actor.Factions.Intersect(target.Factions).Any())
            {
                return ValidationResult.Error(Constants.Messages.CannotAttackAlly);
            }

            return ValidationResult.Success();
        }
    }
}
