using HundgrundBot.Lib.Bases;
using HundgrundBot.Lib.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace HundgrundBot
{
    public class HundgrundBotService : BaseService
    {
        protected IFileHandler FileHandler { get; }
        protected IAuthHandler AuthHandler { get; }

        private IRedditBot HundgrundBot;
        private int executionCount;

        public HundgrundBotService(IServiceScopeFactory scopeFactory, IBotConfiguration config, IFileHandler fileHandler, IAuthHandler authHandler)
            : base(scopeFactory, config)
        {
            FileHandler = fileHandler;
            AuthHandler = authHandler;
        }

        /// <summary>
        /// Executing function of bot, runs on every ExecutionTime seconds
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await HundgrundBot.WorkAsync();
            executionCount++;
            Log.Verbose("HundgrundBotService workbatch # {num} finished.", executionCount);
        }

        /// <summary>
        /// Encapsulating method that manages and stops bot runtime execution
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public override async Task StartAsync(CancellationToken stoppingToken)
        {
            await InitBot();
            await base.StartAsync(stoppingToken);
        }

        private async Task InitBot()
        {
            Console.WriteLine("HundgrundBotService starting up.");
            FileHandler.EnsureExists();
            if (!await AuthHandler.Auth())
                throw new UnauthorizedAccessException();

            var (reddit, subreddit) = await AuthHandler.GetAccessPoints();
            HundgrundBot = new HundgrundBot(reddit, subreddit, Configuration, FileHandler);
        }

        /// <summary>
        /// Cleanup function
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("HundgrundBotService shutting down.");
        }
    }
}
