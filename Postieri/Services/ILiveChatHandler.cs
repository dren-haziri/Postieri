using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Postieri.Services
{
    public interface ILiveChatHandler
    {
        Task PushAsync(HttpContext context, WebSocket webSocket);
    }
}
