using HundgrundBot.Lib.Bases;
using HundgrundBot.Lib.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace HundgrundBot
{
    public class BotService : BaseService
    {
        protected IFileHandler FileHandler { get; }
        protected IAuthHandler AuthHandler { get; }

        private IRedditBot HundgrundBot;
        private int executionCount;

        public BotService(IServiceScopeFactory scopeFactory, IBotConfiguration config, IFileHandler fileHandler, IAuthHandler accountHandler)
            : base(scopeFactory, config)
        {
            FileHandler = fileHandler;
            AuthHandler = accountHandler;
        }

        /// <summary>
        /// Executing function of bot, runs on every ExecutionTime seconds
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            foreach(var comment in await HundgrundBot.GetComments())
            {
                Log.Verbose($"{comment.Body}, Posted by AuthorName: {comment.AuthorName}");
            }

            executionCount++;
            Log.Verbose($"Bot Service workbatch count: #{executionCount}");
        }

        /// <summary>
        /// Encapsulating method that manages and stops bot runtime execution
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public override async Task StartAsync(CancellationToken stoppingToken)
        {
            FileHandler.EnsureExists();
            if (!await AuthHandler.Auth())
                throw new UnauthorizedAccessException();

            var (reddit, subreddit) = await AuthHandler.GetAccessPoints();
            HundgrundBot = new HundgrundBot(reddit, subreddit, Configuration);

            await base.StartAsync(stoppingToken);
        }

        /// <summary>
        /// Cleanup function
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("BotService shutting down.");
        }
    }
}
