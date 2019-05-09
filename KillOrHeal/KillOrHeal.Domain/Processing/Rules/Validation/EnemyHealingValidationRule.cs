using System.Linq;
using KillOrHeal.Common;
using KillOrHeal.Data.Entities;

namespace KillOrHeal.Domain.Processing.Rules.Validation
{
    public class EnemyHealingValidationRule: IPlayerActionValidationRule
    {
        public ValidationResult ValidateAction(PlayerState actor, PlayerState target)
        {
            if (actor.PlayerId != target.PlayerId && !actor.Factions.Intersect(target.Factions).Any())
            {
                return ValidationResult.Error(Constants.Messages.CannotHealEnemies);
            }

            return ValidationResult.Success();
        }
    }
}