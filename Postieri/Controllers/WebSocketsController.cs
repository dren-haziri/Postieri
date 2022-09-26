using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Postieri.Services;
using System.Linq;
using Newtonsoft.Json;

namespace Postieri.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebSocketsController : ControllerBase
    {
        private readonly ILogger<WebSocketsController> _logger;
        private readonly WebSocketServerConnectionManager _manager; 

        public WebSocketsController(ILogger<WebSocketsController> logger,
             WebSocketServerConnectionManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet("/ws")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                _logger.Log(LogLevel.Information, "WebSocket connection established");

                string ConnID = _manager.AddSocket(webSocket);
                await SendConnIDAsync(webSocket, ConnID);

                await Receive(webSocket, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        Console.WriteLine($"Receive->Text");
                        Console.WriteLine($"Message: {Encoding.UTF8.GetString(buffer, 0, result.Count)}");
                        await RouteJSONMessageAsync(Encoding.UTF8.GetString(buffer, 0, result.Count));
                        return;
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        string id = _manager.GetAllSockets().FirstOrDefault(s => s.Value == webSocket).Key;
                        Console.WriteLine($"Receive->Close");

                        _manager.GetAllSockets().TryRemove(id, out WebSocket sock);
                        Console.WriteLine("Managed Connections: " + _manager.GetAllSockets().Count.ToString());

                        await sock.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);

                        return;
                    }
                });
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        } 
        //Send back connection ID to client -------------------------------------
        private async Task SendConnIDAsync(WebSocket webSocket, string connID)
        {
            var buffer = Encoding.UTF8.GetBytes("ConnID: " + connID);
            await webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }
        //-----------------------------------------------------------------
        private async Task Receive(WebSocket webSocket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                    cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
        //---------------------------------------------------------
 
        private async Task RouteJSONMessageAsync(string message)
        {
            var routeOb = JsonConvert.DeserializeObject<dynamic>(message);

            if(Guid.TryParse(routeOb.To.ToString(), out Guid guidOutput))
            {
                Console.WriteLine("Targeted");
                var sock = _manager.GetAllSockets().FirstOrDefault(s => s.Key == routeOb.To.ToString());
                if(sock.Value != null)
                {
                    if(sock.Value.State == WebSocketState.Open)
                    {
                        await sock.Value.SendAsync(Encoding.UTF8.GetBytes(routeOb.Message.ToString()),
                            WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Recipient");
            } 
           
        }
        //-----------------------------------

    }
}