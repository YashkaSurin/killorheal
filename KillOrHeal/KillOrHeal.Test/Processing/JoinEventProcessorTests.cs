using KillOrHeal.Common;
using KillOrHeal.Domain.Communication.Events.Player;
using KillOrHeal.Domain.Communication.Events.Server;
using KillOrHeal.Domain.Processing;
using KillOrHeal.Test.Mocks;
using Xunit;

namespace KillOrHeal.Test.Processing
{
    public class JoinEventProcessorTests
    {
        [Fact]
        public void PlayerShouldJoin()
        {
            var game = GameMocks.Joinable();
            var processor = new JoinEventProcessor(game.Object, new MockLogger());
            var joinedEvent = (PlayerJoinedEvent) processor.Process(new JoinEvent());
            Assert.NotNull(joinedEvent.Player);
        }

        [Fact]
        public void PlayerShouldNotJoinToOvercrowdedGame()
        {
            var game = GameMocks.Overcrowded();
            var processor = new JoinEventProcessor(game.Object, new MockLogger());
            var errorEvent = (ErrorEvent) processor.Process(new JoinEvent());
            Assert.Equal(Constants.Messages.GameOvercrowded, errorEvent.Message);
        }
    }
}