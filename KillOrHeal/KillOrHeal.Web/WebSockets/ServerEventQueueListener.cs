using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KillOrHeal.Domain.Communication;
using KillOrHeal.Domain.Communication.Events.Server;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace KillOrHeal.Web.WebSockets
{
    public class ServerEventQueueListener: IServerEventQueueListener
    {
        private readonly IEventQueue<ServerEvent> _queue;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly IConfiguration _configuration;
        private readonly JsonMediaTypeFormatter _jsonFormatter;

        public ServerEventQueueListener(IEventQueue<ServerEvent> queue, IConfiguration configuration, JsonMediaTypeFormatter jsonFormatter)
        {
            _queue = queue;
            _configuration = configuration;
            _jsonFormatter = jsonFormatter;
        }

        public void Start()
        {
            Task.Run(async () =>
            {
                while (!_cts.IsCancellationRequested)
                {
                    if (_queue.TryDequeue(out var e))
                    {
                        var client = new HttpClient();
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        await client.PostAsync(_configuration["Communication:EventPublishUrl"], e, _jsonFormatter);
                    }

                    Thread.Sleep(100);
                }
            });
        }

        public void Stop()
        {
            _cts.Cancel();
        }
    }
}
