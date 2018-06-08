using Autofac;
using Cynthia.Chat.Common;
using Cynthia.Chat.Common.Attributes;

namespace Cynthia.Chat.Server.Services
{
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