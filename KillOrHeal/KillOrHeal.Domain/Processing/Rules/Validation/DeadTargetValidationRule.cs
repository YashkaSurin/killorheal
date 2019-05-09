using KillOrHeal.Common;
using KillOrHeal.Data.Entities;

namespace KillOrHeal.Domain.Processing.Rules.Validation
{
    public class DeadTargetValidationRule: IPlayerActionValidationRule
    {
        public ValidationResult ValidateAction(PlayerState actor, PlayerState target)
        {
            if (!target.IsAlive)
            {
                return ValidationResult.Error(Constants.Messages.CannotTouchDead);
            }

            return ValidationResult.Success();
        }
    }
}