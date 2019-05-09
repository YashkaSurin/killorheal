using KillOrHeal.Data.Entities;

namespace KillOrHeal.Domain.Processing.Rules.Validation
{
    /// <summary>
    /// Base interface for player action validation rules.
    /// </summary>
    public interface IPlayerActionValidationRule
    {
        /// <summary>
        /// Validate action of one player ("actor") against another player ("target").
        /// </summary>
        ValidationResult ValidateAction(PlayerState actor, PlayerState target);
    }
}
