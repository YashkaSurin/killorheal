using KillOrHeal.Data.Entities;

namespace KillOrHeal.Domain.Processing.Rules.Transformation
{
    /// <summary>
    /// Interface for transformation rules. Makes changes to the damage value if the conditions are met.
    /// </summary>
    public interface IEffectTransformationRule<T>
    {
        T Transform(T value, PlayerState actor, PlayerState target);
    }
}