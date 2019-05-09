using System;
using KillOrHeal.Data.Game;
using KillOrHeal.Domain.Dispatching;
using KillOrHeal.Web.WebSockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace KillOrHeal.Web.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void StartNewGame(this IApplicationBuilder applicationBuilder)
        {
            var appServices = applicationBuilder.ApplicationServices;
            var game = appServices.GetService<IGame>();
            var configuration = appServices.GetService<IConfiguration>();
            var maxPlayers = configuration.GetValue<int>("Game:MaxPlayers");
            for (var i = 0; i < maxPlayers; i++)
            {
                game.SpawnNewPlayer();
            }

            appServices.GetService<IServerEventQueueListener>().Start();
            appServices.GetService<IServerEventDispatcher>().Start();
        }

        public static void UseErrorHandling(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception e)
                {
                    var logger = app.ApplicationServices.GetService<ILogger<Program>>();
                    logger.LogError(e, "Unhandled exception");

                    // Here we return 500 for all errors, for simplification (as the task was not concentrated on API conformance).
                    // In real application we would have to determine proper status code based on exception type.
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new { message = e.Message })).ConfigureAwait(false);
                }
            });
        }

    }
}
