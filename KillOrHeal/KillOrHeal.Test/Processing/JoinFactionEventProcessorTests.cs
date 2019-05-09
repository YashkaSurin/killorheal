using System;
using System.Collections.Generic;
using System.Text;
using KillOrHeal.Domain.Communication.Events.Player;
using KillOrHeal.Domain.Communication.Events.Server;
using KillOrHeal.Domain.Processing;
using KillOrHeal.Test.Mocks;
using Xunit;

namespace KillOrHeal.Test.Processing
{
    public class JoinFactionEventProcessorTests
    {
        [Fact]
        public void PlayerCanJoinFaction()
        {
            var game = GameMocks.Joinable();
            var processor = new JoinFactionEventProcessor(game.Object);
            var joinedEvent = (PlayerJoinedEvent)processor.Process(new JoinEvent());
            Assert.NotNull(joinedEvent.Player);
        }
    }
}
