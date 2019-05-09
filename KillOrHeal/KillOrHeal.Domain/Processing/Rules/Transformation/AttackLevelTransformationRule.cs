using KillOrHeal.Data.Entities;

namespace KillOrHeal.Domain.Processing.Rules.Transformation
{
    public class AttackLevelTransformationRule: IEffectTransformationRule<double>
    {
        public double Transform(double value, PlayerState actor, PlayerState target)
        {
            var levelDiff = actor.Level - target.Level;
            if (levelDiff >= 5)
            {
                return value * 1.5;
            }
            else if (levelDiff <= -5)
            {
                return value / 2;
            }

            return value;
        }
    }
}