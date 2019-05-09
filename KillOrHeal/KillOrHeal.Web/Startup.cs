using System.Net.Http.Formatting;
using KillOrHeal.Data.Game;
using KillOrHeal.Domain.Communication;
using KillOrHeal.Domain.Dispatching;
using KillOrHeal.Domain.Processing;
using KillOrHeal.Domain.Services;
using KillOrHeal.Web.Extensions;
using KillOrHeal.Web.WebSockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KillOrHeal.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(b => { b.AddConsole(); });
            services.AddMvc();
            services.AddSingleton<IServerEventDispatcher, ServerEventDispatcher>();
            services.AddSingleton(typeof(IEventQueue<>), typeof(EventQueue<>));
            services.AddSingleton<IPlayerEventProcessorResolver, PlayerEventProcessorResolver>();
            services.AddSingleton<IServerEventQueueListener, ServerEventQueueListener>();
            services.AddSingleton(typeof(IGame), _ => new Game(
                Configuration.GetValue<int>("Game:MapSize"),
                Configuration.GetValue<int>("Game:MaxPlayers"),
                Configuration.GetValue<int>("Game:DefaultHealth"),
                Configuration.GetValue<int>("Game:FactionsCount"))
            );
            services.AddTransient<IGameService, GameService>();
            services.AddTransient(typeof(JsonMediaTypeFormatter), _ =>
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

            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {   
            app.UseErrorHandling();
            if (env.IsDevelopment())
            {
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<EventHub>("events");
            });

            app.StartNewGame();
        }
    }
}
