using KillOrHeal.Common;
using KillOrHeal.Data.Entities;

namespace KillOrHeal.Domain.Processing.Rules.Validation
{
    public class SelfAttackValidationRule: IPlayerActionValidationRule
    {
        public ValidationResult ValidateAction(PlayerState actor, PlayerState target)
        {
            if (actor.PlayerId == target.PlayerId)
            {
                return ValidationResult.Error(Constants.Messages.CannotAttackSelf);
            }

            return ValidationResult.Success();
        }
    }
}