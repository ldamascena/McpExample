using MCPServer.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;

var builder = Host.CreateApplicationBuilder();

builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(options =>
{
    options.SingleLine = true;
    options.TimestampFormat = "HH:mm:ss ";
});

var serverInfo = new Implementation { Name = "DotnetMCPServer", Version = "1.0.0" };

builder.Services
   .AddMcpServer(mcp =>
   {
       mcp.ServerInfo = serverInfo;
   })
   .WithStdioServerTransport()
   .WithToolsFromAssembly();

builder.Services.AddHttpClient<StudentsApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7007/api/");
});

var app = builder.Build();
await app.RunAsync();
