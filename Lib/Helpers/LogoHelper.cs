namespace HundgrundBot.Lib.Helpers
{
    public static class LogoHelper
    {
        public static void Print()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"
                               _      
     /_/   _   _/_  _   _   _//_)_ _/_
    / //_// //_//_///_// //_//_)/_//  
                _/                    
        ");

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
