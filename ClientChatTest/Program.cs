﻿using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

Console.WriteLine("Press Enter to Continue...");
Console.ReadLine();

using(ClientWebSocket client = new ClientWebSocket())
{
    Uri serviceUri = new Uri("ws://localhost:5134/ws");
    var cancellationToken = new CancellationTokenSource();
    cancellationToken.CancelAfter(TimeSpan.FromSeconds(120));
    try
    {
        await client.ConnectAsync(serviceUri, cancellationToken.Token);

        while(client.State == WebSocketState.Open)
        {
            Console.WriteLine("Enter message.");
            string message = Console.ReadLine();
            if (!string.IsNullOrEmpty(message))
            {
                ArraySegment<byte> byteToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                await client.SendAsync(byteToSend, WebSocketMessageType.Text, true, cancellationToken.Token);
                var resposeBuffer = new byte[1024];
                var offset = 0;
                var packet = 1024;
                while(true)
                {
                    ArraySegment<byte> byteRecieved = new ArraySegment<byte>(resposeBuffer, offset, packet);
                    WebSocketReceiveResult response = await client.ReceiveAsync(byteRecieved, cancellationToken.Token);
                    var responseMessage = Encoding.UTF8.GetString(resposeBuffer, offset, response.Count);
                    Console.WriteLine(responseMessage);
                    if (response.EndOfMessage) break;
                }
            }
        }
    }
    catch(WebSocketException e)
    {
        Console.WriteLine(e.Message);
    }

    Console.ReadLine();
}
