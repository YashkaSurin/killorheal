using Microsoft.AspNetCore.Mvc;
using KillOrHeal.Data.Entities;
using KillOrHeal.Data.Game;
using KillOrHeal.Web.WebSockets;
using Microsoft.AspNetCore.SignalR;

namespace KillOrHeal.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/game")]
    public class GameController : Controller
    {
        private readonly IGame _game;
        

        public GameController(IGame game)
        {
            _game = game;
        }

        [HttpGet]
        public GameStateDto Get()
        {
            return _game.GetFullState();
        }
    }
}