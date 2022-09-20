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
    public class TrackingController : ControllerBase
    {
        private readonly ILogger<WebSocketsController> _logger;
        private readonly WebSocketServerConnectionManager _manager;

        public TrackingController(ILogger<WebSocketsController> logger,
             WebSocketServerConnectionManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet("/tracking")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                _logger.Log(LogLevel.Information, "WebSocket connection established");

                string ConnID = _manager.AddSocket(webSocket);
                await SendConnIDAsync(webSocket, ConnID);

                await SendCoordinatesAsync(webSocket);

                await Receive(webSocket, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        string id = _manager.GetAllSockets().FirstOrDefault(s => s.Value == webSocket).Key;  
                        _manager.GetAllSockets().TryRemove(id, out WebSocket sock);

                        await sock.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
                        _logger.Log(LogLevel.Information, "WebSocket is Closed.");

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
        private async Task SendCoordinatesAsync(WebSocket webSocket)
        {
            Random randNum = new Random();
            int[] longitude = Enumerable.Repeat(0, 10).Select(i => randNum.Next(9190, 391000)).ToArray();
            int[] latitude = Enumerable.Repeat(0, 10).Select(i => randNum.Next(8790, 5831000)).ToArray();
            
                for (int i = 0; i < longitude.Length; i++)
                {
                    var buffer = Encoding.UTF8.GetBytes
                    ($"Longitude:{longitude[i]} " +
                    $"Latitude: {latitude[i]} " +
                    $" {DateTime.UtcNow:f} ");

                    await webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                    _logger.Log(LogLevel.Information, "Longitude and Latitude send to Client.");
                    Thread.Sleep(5000);
            }
            
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

    }
}