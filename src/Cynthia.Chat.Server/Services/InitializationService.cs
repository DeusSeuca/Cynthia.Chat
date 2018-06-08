using Autofac;
using Cynthia.Chat.Server.Attributes;
using Cynthia.DataBase.Common;

namespace Cynthia.Chat.Server.Services
{
    [Service]
    [Singleton]
    public class InitializationService
    {
        public ChatMessageCacheService cache { get; set; }
        public void Start()
        {
            cache.InitData();
            cache.AutoSaveData();
        }
    }
}