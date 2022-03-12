using RedditSharp;
using RedditSharp.Things;

namespace HundgrundBot.Lib.Interfaces
{
    public interface IAuthHandler
    {
        public Reddit Reddit { get; set; }
        public Subreddit Subreddit { get; set; }
        Task<bool> Auth();
        Task<(Reddit, Subreddit)> GetAccessPoints();
        //Task<Comment[]> GetComments(int amount);
    }
}
