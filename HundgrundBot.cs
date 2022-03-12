using HundgrundBot.Lib.Interfaces;
using RedditSharp;
using RedditSharp.Things;

namespace HundgrundBot
{
    public class HundgrundBot : IRedditBot
    {
        public Reddit Reddit { get; set; }
        public Subreddit Subreddit { get; set; }
        public IBotConfiguration Config { get; }

        public HundgrundBot(Reddit reddit, Subreddit subreddit, IBotConfiguration config)
        {
            Reddit = reddit;
            Subreddit = subreddit;
            Config = config;
        }

        public async Task<List<Comment>> GetComments(int amount = 25)
            => await Subreddit.GetComments(amount).ToListAsync();

        //public async Task<Comment[]> GetComments(int amount)
        //    => await Subreddit.GetComments(amount).ToArrayAsync();
    }
}
