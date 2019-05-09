using System;
using System.Threading;
using System.Threading.Tasks;
using KillOrHeal.Domain.Communication;
using KillOrHeal.Domain.Communication.Events.Player;
using KillOrHeal.Domain.Communication.Events.Server;
using KillOrHeal.Domain.Processing;

namespace KillOrHeal.Domain.Dispatching
{
    public class ServerEventDispatcher: IServerEventDispatcher
    {
        private readonly IEventQueue<ServerEvent> _serverEventQueue;
        private readonly IEventQueue<PlayerEvent> _playerEventQueue;
        private readonly IPlayerEventProcessorResolver _playerEventProcessorResolver;
        private CancellationTokenSource _cancellationTokenSource;

        public ServerEventDispatcher(IEventQueue<ServerEvent> serverEventQueue, IEventQueue<PlayerEvent> playerEventQueue, IPlayerEventProcessorResolver playerEventProcessorResolver)
        {
            _serverEventQueue = serverEventQueue;
            _playerEventQueue = playerEventQueue;
            _playerEventProcessorResolver = playerEventProcessorResolver;
        }

        public void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Task.Run(() =>
            {
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    if (!_playerEventQueue.TryDequeue(out var ev))
                    {
                        Thread.Sleep(100);
                        continue;
                    }

                    var processor = _playerEventProcessorResolver.Resolve(ev.GetType());
                    var result = processor.Process(ev);
                    _serverEventQueue.Enqueque(result);
                }
            });
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}