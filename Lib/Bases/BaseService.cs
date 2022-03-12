using HundgrundBot.Lib.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundgrundBot.Lib.Bases
{
    public abstract class BaseService : IHostedService
    {
        public AutoResetEvent AutoResetEvent = new(false);
        public TimeSpan ShutdownTimeout { get; set; } = TimeSpan.FromSeconds(1);
        public TimeSpan ExecutionTime { get; set; } = TimeSpan.FromSeconds(10);

        public BaseService(IServiceScopeFactory scopeFactory, IBotConfiguration config)
        {
            ScopeFactory = scopeFactory;
            Configuration = config;
        }

        public IServiceScopeFactory ScopeFactory { get; }
        public IBotConfiguration Configuration { get; }

        /// <summary>
        /// Executing task that must be implemented
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public abstract Task ExecuteAsync(CancellationToken stoppingToken);

        public virtual async Task StartAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Run(async () =>
                {
                    await ExecuteAsync(stoppingToken);
                }, stoppingToken);
                AutoResetEvent.WaitOne(ExecutionTime);
            }
        }

        public virtual Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
