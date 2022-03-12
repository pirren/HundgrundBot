using HundgrundBot;
using HundgrundBot.Configuration;
using HundgrundBot.Lib.Handlers;
using HundgrundBot.Lib.Helpers;
using HundgrundBot.Lib.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

LogoHelper.Print();

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("botconfig.json")
            .AddEnvironmentVariables()
            .Build();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console()
            .WriteTo.File("Logs/log_.log",
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
            .CreateLogger();

        Log.Verbose("Starting Bot.");

        services.AddSingleton<IBotConfiguration>(config.GetRequiredSection("BotConfiguration").Get<BotConfiguration>());
        services.AddScoped<IFileHandler, FileHandler>();
        services.AddScoped<IAuthHandler, AuthHandler>();

        services.AddHostedService<BotService>();
    })
    .Build();

await host.RunAsync();