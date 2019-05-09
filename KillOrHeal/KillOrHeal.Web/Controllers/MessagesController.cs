using System.IO;
using System.Threading.Tasks;
using KillOrHeal.Web.WebSockets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace KillOrHeal.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/messages")]
    public class MessagesController : Controller
    {
        private readonly IHubContext<EventHub> _hubContext;

        public MessagesController(IHubContext<EventHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task Publish()
        {
            using (var sr = new StreamReader(Request.Body))
            {
                await _hubContext.Clients.All.InvokeAsync("broadcast", sr.ReadToEnd());
            }
        }
    }
}