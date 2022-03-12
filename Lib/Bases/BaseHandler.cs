using HundgrundBot.Lib.Interfaces;

namespace HundgrundBot.Lib.Bases
{
    public abstract class BaseHandler
    {
        public BaseHandler(IBotConfiguration settings)
        {
            Configuration = settings;
        }

        public IBotConfiguration Configuration { get; }
    }
}
