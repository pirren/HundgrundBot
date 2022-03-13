using HundgrundBot.Lib.Interfaces;
using RedditSharp;
using RedditSharp.Things;
using Serilog;

namespace HundgrundBot
{
    public class HundgrundBot : IRedditBot
    {
        public Reddit Reddit { get; set; }
        public Subreddit Subreddit { get; set; }
        public IBotConfiguration Config { get; }
        public IFileHandler FileHandler { get; }

        private TimeSpan latestPost;

        private const string message = "Hej! Hundgrund är en ö i södra Finland, mer info kan hittas på Wikipedia.\r\n\r\nhttps://sv.wikipedia.org/wiki/Hundgrund\r\n\r\nBeep boop, detta är en bot.";
        private const string searchstring = "!hundgrund";

        public HundgrundBot(Reddit reddit, Subreddit subreddit, IBotConfiguration config, IFileHandler fileHandler)
        {
            Reddit = reddit;
            Subreddit = subreddit;
            Config = config;
            FileHandler = fileHandler;
        }

        public async Task<List<Comment>> GetCommentsAsync(int amount)
            => await Subreddit.GetComments(amount).ToListAsync();

        public async Task ReplyAsync(Comment comment)
        {
            await comment.ReplyAsync(message);
            await FileHandler.AddEntry(comment.Id);
        }

        public async Task WorkAsync()
        {
            var repliedto = FileHandler.ReadLines();

            var latestpost = (DateTime.Now.TimeOfDay - latestPost).TotalMinutes;
            if (latestpost < 10)
            {
                Log.Verbose("Latest Reply was {latest} minutes ago, now waiting until 10 minute mark.", latestpost);
            }
            else
            {
                foreach (var comment in (await GetCommentsAsync(10)).Where(x => x.Body.ToLower().Contains(searchstring) && !repliedto.Contains(x.Id)))
                {
                    Log.Verbose("Replying to Comment = {id}, Posted by AuthorName: {author}", comment.Id, comment.AuthorName);
                    await ReplyAsync(comment);
                    latestPost = DateTime.Now.TimeOfDay;
                    return;
                }
            }
        }
    }
}
