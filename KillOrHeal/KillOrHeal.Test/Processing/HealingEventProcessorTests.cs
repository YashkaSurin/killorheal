using System.Collections.Generic;
using KillOrHeal.Common;
using KillOrHeal.Data.Game;
using KillOrHeal.Domain.Communication.Events.Player;
using KillOrHeal.Domain.Communication.Events.Server;
using KillOrHeal.Domain.Processing;
using KillOrHeal.Domain.Processing.Rules.Validation;
using KillOrHeal.Test.Mocks;
using Moq;
using Xunit;

namespace KillOrHeal.Test.Processing
{
    public class HealingEventProcessorTests
    {
        [Fact]
        public void HealingShouldIncreaseHealth()
        {
            var game = GameMocks.TwoPlayersOneDamaged();
            var processor = CreateProcessor(game);
            var result = (PlayerHealedEvent) processor.Process(new HealingEvent(2, 2));

            Assert.Equal(2, result.PlayerId);
            Assert.InRange(result.Health, 600, 700);
            Assert.Equal(string.Format(Constants.Messages.HealedTemplate, 2, 2), result.Message);
        }

        [Fact]
        public void HealingShouldBeLimited()
        {
            var game = GameMocks.TwoPlayers();
            var processor = CreateProcessor(game);
            var result = (PlayerHealedEvent) processor.Process(new HealingEvent(1, 1));

            Assert.Equal(1, result.PlayerId);
            Assert.Equal(1000, result.Health);
            Assert.Equal(string.Format(Constants.Messages.HealedTemplate, 1, 1), result.Message);
        }

        // TODO uncomment when we have factions
        /*[Fact]
        public void DeadCannotBeHealed()
        {
            var game = GameMocks.TwoPlayersOneDead();
            var processor = CreateProcessor(game);
            var result = (ErrorEvent) processor.Process(new HealingEvent(1, 2));
            Assert.Equal(Constants.Messages.CannotTouchDead, result.Message);
        }*/

        [Fact]
        public void IncorrectParametersShouldProduceError()
        {
            var game = GameMocks.Empty();
            var processor = CreateProcessor(game);
            var result = (ErrorEvent) processor.Process(new HealingEvent(1, 2));
            Assert.Equal(string.Format(Constants.Messages.PlayerNotFoundTemplate, 1), result.Message);
        }

        private static HealingEventProcessor CreateProcessor(IMock<IGame> game)
        {
            var healingValidationRules = new List<IPlayerActionValidationRule>
            {
                new DeadTargetValidationRule(),
                new EnemyHealingValidationRule()
            };

            return new HealingEventProcessor(healingValidationRules, null, new MockLogger(), game.Object);
        }
    }
}