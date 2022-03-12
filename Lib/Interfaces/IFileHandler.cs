namespace HundgrundBot.Lib.Interfaces
{
    public interface IFileHandler
    {
        void EnsureExists();
        string ReadText();
        IEnumerable<string> ReadLines();
    }
}
