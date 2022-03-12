# HundgrundBot

Reddit bot written in C# and using RedditSharp, free of use etc. All info the bot needs to be fed should be placed in `botsettings.json` with following format:
```
{
  "BotConfiguration": {
    "Username": "BotAccountName",
    "Password": "BotAccountPassword",
    "ClientId": "ClientId",
    "ClientSecret": "ClientSecret",
    "RedirectUri": "Uri for redirect",
    "Subreddit": "SubredditName",
    "RepliedToPath": "FilePath"
  }
}
```
More info on this subject is found [here](https://github.com/reddit-archive/reddit/wiki/OAuth2)
