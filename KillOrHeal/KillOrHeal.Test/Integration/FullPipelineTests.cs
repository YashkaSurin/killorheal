using System;
using System.Threading;
using KillOrHeal.Common;
using KillOrHeal.Data.Game;
using KillOrHeal.Domain.Communication;
using KillOrHeal.Domain.Communication.Events.Player;
using KillOrHeal.Domain.Communication.Events.Server;
using KillOrHeal.Domain.Dispatching;
using KillOrHeal.Test.DI;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using Xunit;

namespace KillOrHeal.Test.Integration
{
    /// <summary>
    /// These integration tests are not comprehensive, they are just a demonstration of the approach.
    /// </summary>
    public class FullPipelineTests: IDisposable
    {
        private readonly Container _container;

        public FullPipelineTests()
        {
            _container = DiContainer.New();
            StartGame();
        }

        [Fact]
        public void PlayerActionEventShouldPass()
        {
            var inputQueue = _container.GetInstance<IEventQueue<PlayerEvent>>();
            var outputQueue = _container.GetInstance<IEventQueue<ServerEvent>>();
            inputQueue.Enqueque(new HealingEvent(1, 1));
            Thread.Sleep(500);
            Assert.True(outputQueue.TryDequeue(out var e));
            var typedEvent = Assert.IsType<PlayerHealedEvent>(e);
            Assert.Equal(Constants.ServerEventTypes.PlayerHealed, typedEvent.Type);
            Assert.Equal(1, typedEvent.PlayerId);
        }

        [Fact]
        public void JoinEventShouldPass()
        {
            var inputQueue = _container.GetInstance<IEventQueue<PlayerEvent>>();
            var outputQueue = _container.GetInstance<IEventQueue<ServerEvent>>();
            inputQueue.Enqueque(new JoinEvent());
            Thread.Sleep(500);
            Assert.True(outputQueue.TryDequeue(out var e));
            var typedEvent = Assert.IsType<PlayerJoinedEvent>(e);
            Assert.Equal(Constants.ServerEventTypes.PlayerJoined, typedEvent.Type);
        }

        [Fact]
        public void JoinFactionEventShouldPass()
        {
            var inputQueue = _container.GetInstance<IEventQueue<PlayerEvent>>();
            var outputQueue = _container.GetInstance<IEventQueue<ServerEvent>>();
            inputQueue.Enqueque(new JoinEvent());
            Thread.Sleep(500);
            outputQueue.TryDequeue(out var e);
            var joinedEvent = (PlayerJoinedEvent) e;
            var playerId = joinedEvent.Player.PlayerId;
            var newFaction = joinedEvent.Player.Factions[0] == 1 ? 2 : 1;
            inputQueue.Enqueque(new JoinFactionEvent(playerId, newFaction));
            Thread.Sleep(500);
            outputQueue.TryDequeue(out e);
            var typedEvent = Assert.IsType<PlayerJoinedFactionEvent>(e);
            Assert.Equal(Constants.ServerEventTypes.PlayerJoinedFaction, typedEvent.Type);
            Assert.Equal(newFaction, typedEvent.FactionId);
        }

        [Fact]
        public void LeaveFactionEventShouldPass()
        {
            var inputQueue = _container.GetInstance<IEventQueue<PlayerEvent>>();
            var outputQueue = _container.GetInstance<IEventQueue<ServerEvent>>();
            inputQueue.Enqueque(new JoinEvent());
            Thread.Sleep(500);
            outputQueue.TryDequeue(out var e);
            var joinedEvent = (PlayerJoinedEvent)e;
            var playerId = joinedEvent.Player.PlayerId;
            var faction = joinedEvent.Player.Factions[0];
            inputQueue.Enqueque(new LeaveFactionEvent(playerId, faction));
            Thread.Sleep(500);
            outputQueue.TryDequeue(out e);
            var typedEvent = Assert.IsType<PlayerLeftFactionEvent>(e);
            Assert.Equal(Constants.ServerEventTypes.PlayerLeftFaction, typedEvent.Type);
            Assert.Equal(faction, typedEvent.FactionId);
        }

        [Fact]
        public void InvalidEventShouldCauseError()
        {
            var inputQueue = _container.GetInstance<IEventQueue<PlayerEvent>>();
            var outputQueue = _container.GetInstance<IEventQueue<ServerEvent>>();
            inputQueue.Enqueque(new AttackEvent(500, 200));
            Thread.Sleep(1000);
            Assert.True(outputQueue.TryDequeue(out var e));
            var typedEvent = Assert.IsType<ErrorEvent>(e);
            Assert.Equal("Error", typedEvent.Type);
        }

        private void StartGame()
        {
            var config = _container.GetInstance<IConfiguration>();
            var game = _container.GetInstance<IGame>();
            var maxPlayers = config.GetValue<int>("Game:MaxPlayers");
            for (var i = 0; i < maxPlayers / 2; i++)
            {
                game.SpawnNewPlayer();
            }
            _container.GetInstance<IServerEventDispatcher>().Start();
        }

        public void Dispose()
        {
            _container.GetInstance<IServerEventDispatcher>().Stop();
            _container?.Dispose();
        }
    }
}
