using HundgrundBot.Lib.Bases;
using HundgrundBot.Lib.Interfaces;
using RedditSharp;
using RedditSharp.Things;
using Serilog;

namespace HundgrundBot.Lib.Handlers
{
    public class AuthHandler : BaseHandler, IAuthHandler
    {
        public Reddit Reddit { get; set; }
        public Subreddit Subreddit { get; set; }

        private BotWebAgent _agent;

        public AuthHandler(IBotConfiguration config) : base(config)
        {
        }

        public Task<bool> Auth()
        {
            try
            {
                Log.Information("Attempting connection to Reddit...");

                _agent = new(Configuration.Username,
                    Configuration.Password,
                    Configuration.ClientId,
                    Configuration.ClientSecret,
                    Configuration.RedirectUri);

                Log.Information("Bot successfully connected to Reddit.");

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Log.Fatal("Unable to connect. Message: {msg}", ex.Message);
                return Task.FromResult(false);
            }
        }

        public async Task<(Reddit, Subreddit)> GetAccessPoints()
        {
            try
            {
                Log.Information("Retrieving access points...");

                Reddit = new Reddit(_agent);
                Subreddit = await Reddit.GetSubredditAsync(Configuration.Subreddit);

                Log.Information("Access points retrieved! Agent: {usr}, Subreddit: {subr}", Configuration.Username, Subreddit.DisplayName);

                return (Reddit, Subreddit);
            }
            catch
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
