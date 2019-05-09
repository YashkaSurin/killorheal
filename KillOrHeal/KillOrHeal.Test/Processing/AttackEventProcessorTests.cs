using System.Collections.Generic;
using KillOrHeal.Common;
using KillOrHeal.Data.Game;
using KillOrHeal.Domain.Communication.Events.Player;
using KillOrHeal.Domain.Communication.Events.Server;
using KillOrHeal.Domain.Processing;
using KillOrHeal.Domain.Processing.Rules.Transformation;
using KillOrHeal.Domain.Processing.Rules.Validation;
using KillOrHeal.Test.Mocks;
using Moq;
using Xunit;

namespace KillOrHeal.Test.Processing
{
    public class AttackEventProcessorTests
    {
        [Fact]
        public void AttackShouldCauseDamage()
        {
            var game = GameMocks.TwoPlayers();
            var processor = CreateProcessor(game);
            var result = (PlayerDamagedEvent) processor.Process(new AttackEvent(1, 2));
            Assert.Equal(2, result.PlayerId);
            Assert.InRange(result.Health, 0, 500);
            Assert.StartsWith(string.Format(Constants.Messages.AttackedTemplate, 1, 2), result.Message);
        }

        [Fact]
        public void KillingShouldProduceCorrectMessage()
        {
            var game = GameMocks.TwoPlayersOneDamaged();
            var processor = CreateProcessor(game);

            var result = (PlayerDamagedEvent)processor.Process(new AttackEvent(1, 2));
            Assert.Equal(0, result.Health);
            Assert.Equal(string.Format(Constants.Messages.AttackedTemplate, 1, 2) + " " + string.Format(Constants.Messages.KilledTemplate, 2), result.Message);
        }

        [Fact]
        public void SelfAttackShouldProduceError()
        {
            var game = GameMocks.TwoPlayers();
            var processor = CreateProcessor(game);
            var result = (ErrorEvent) processor.Process(new AttackEvent(1, 1));
            Assert.Equal(Constants.Messages.CannotAttackSelf, result.Message);
        }

        [Fact]
        public void IncorrectParametersShouldProduceError()
        {
            var game = GameMocks.Empty();
            var processor = CreateProcessor(game);
            var result = (ErrorEvent) processor.Process(new AttackEvent(2, 1));
            Assert.Equal(string.Format(Constants.Messages.PlayerNotFoundTemplate, 2), result.Message);
            result = (ErrorEvent) processor.Process(new AttackEvent(1, 2));
            Assert.Equal(string.Format(Constants.Messages.PlayerNotFoundTemplate, 1), result.Message);
        }

        private static AttackEventProcessor CreateProcessor(IMock<IGame> game)
        {
            var attackValidationRules = new List<IPlayerActionValidationRule>
            {
                new SelfAttackValidationRule(),
                new DeadTargetValidationRule(),
                new CombatRangeValidationRule()
            };

            var attackTransformationRule = new AttackLevelTransformationRule();

            return new AttackEventProcessor(attackValidationRules, attackTransformationRule, new MockLogger(), game.Object);
        }
    }
}
