using System.Collections.Generic;
using System.Net.Http.Formatting;
using KillOrHeal.Data.Game;
using KillOrHeal.Domain.Communication;
using KillOrHeal.Domain.Dispatching;
using KillOrHeal.Domain.Processing;
using KillOrHeal.Domain.Services;
using KillOrHeal.Test.Mocks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SimpleInjector;

namespace KillOrHeal.Test.DI
{
    public class DiContainer
    {
        private DiContainer()
        {
        }

        public static Container New()
        {
            var container = new Container();
            container.RegisterSingleton<ILogger, MockLogger>();
            container.RegisterSingleton(typeof(ILogger<>), typeof(MockLogger<>));
            container.RegisterSingleton<IConfiguration>(() =>            
                new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Game:MapSize", "10" },
                    { "Game:MaxPlayers", "5" },
                    { "Game:DefaultHealth", "1000" },
                    { "Game:FactionsCount", "5" },
                }).Build()
            );
            container.RegisterSingleton<IGame>(() => new Data.Game.Game(10, 5, 1000, 5));
            container.RegisterSingleton<IServerEventDispatcher, ServerEventDispatcher>();
            container.RegisterSingleton(typeof(IEventQueue<>), typeof(EventQueue<>));
            container.RegisterSingleton<IPlayerEventProcessorResolver, PlayerEventProcessorResolver>();
            container.Register<IGameService, GameService>();
            container.Register(() =>
            {
                var formatter = new JsonMediaTypeFormatter
                {
                    SerializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
                };
                return formatter;
            });

            return container;
        }
    }
}