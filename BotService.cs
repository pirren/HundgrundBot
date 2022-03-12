using HundgrundBot.Lib.Bases;
using HundgrundBot.Lib.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HundgrundBot
{
    public class BotService : BaseService
    {
        protected IFileHandler FileHandler { get; }
        protected IAuthHandler AuthHandler { get; }

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
            executionCount++;

            // Here we need to add executing code for bot

            // example
            //var comments = await accountHandler.GetComments(10);
            //foreach(var comment in comments)
            //{
            //    Console.WriteLine(comment.AuthorName);
            //}
            Console.WriteLine($"Bot Service is working. Execution Count: #{executionCount}, ");
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

            // todo: we need to handle Reddit and Subreddit from accountHandler here. now we need a structure for it
            var (reddit, subreddit) = await AuthHandler.GetAccessPoints();

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
