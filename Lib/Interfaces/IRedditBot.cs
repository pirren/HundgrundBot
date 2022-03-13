using RedditSharp;
using RedditSharp.Things;

namespace HundgrundBot.Lib.Interfaces
{
    public interface IRedditBot
    {
        public Reddit Reddit { get; set; }
        public Subreddit Subreddit { get; set; }
        public Task<List<Comment>> GetCommentsAsync(int amount);
        public Task ReplyAsync(Comment comment);
        public Task WorkAsync();
    }
}
