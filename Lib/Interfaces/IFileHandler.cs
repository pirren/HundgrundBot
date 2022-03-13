namespace HundgrundBot.Lib.Interfaces
{
    public interface IFileHandler
    {
        void EnsureExists();
        Task AddEntry(string id);
        string ReadText();
        IEnumerable<string> ReadLines();
    }
}
