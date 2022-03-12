namespace HundgrundBot.Lib.Interfaces
{
    public interface IBotConfiguration
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
        public string Subreddit { get; set; }
        public string RepliedToPath { get; set; }
    }
}
