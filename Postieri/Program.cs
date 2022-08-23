global using Postieri.Interfaces;
global using Postieri.DTOs;
using Postieri.Data;
using Microsoft.EntityFrameworkCore;
using Postieri.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MimeKit;
using NuGet.Common;
using Swashbuckle.AspNetCore.Filters;
using System.Net.WebSockets;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
string connString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
//----------------------
app.UseRouting();

var wsOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(120)
};
app.UseWebSockets(wsOptions);
app.Run(async (context) =>
{
    if(context.Request.Path == "/send")
    {
        if(context.WebSockets.IsWebSocketRequest)
        {
            using(WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync())
            {
                await Send(context, webSocket);
            }
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
});


 async Task Send(HttpContext context, WebSocket webSocket)
{
    var buffer = new byte[1024 * 4];
    WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer),
            System.Threading.CancellationToken.None);
    if(result != null)
    {
        while(!result.CloseStatus.HasValue)
        {
            string msg = Encoding.UTF8.GetString(new ArraySegment<byte>(buffer, 0, result.Count));
            Console.WriteLine($"client says: {msg}");
            await webSocket.SendAsync(new ArraySegment<byte>
                    (Encoding.UTF8.GetBytes($"Server says: {DateTime.UtcNow:f} ")),
                    result.MessageType, result.EndOfMessage, System.Threading.CancellationToken.None);
            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer),
                System.Threading.CancellationToken.None);
            //Console.WriteLine(result);
        }
    }
    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription,
            System.Threading.CancellationToken.None);
}
//-----------------------------------------
app.MapControllers();

app.Run();
