using HundgrundBot.Lib.Bases;
using HundgrundBot.Lib.Interfaces;

namespace HundgrundBot.Lib.Handlers
{
    public class FileHandler : BaseHandler, IFileHandler
    {
        public FileHandler(IBotConfiguration config) : base(config)
        {
        }

        public void EnsureExists()
        {
            if (!File.Exists(Configuration.RepliedToPath))
                File.Create(Configuration.RepliedToPath).Close();
        }

        public IEnumerable<string> ReadLines()
            => File.ReadAllLines(Configuration.RepliedToPath);

        public string ReadText()
            => File.ReadAllText(Configuration.RepliedToPath);
    }
}
