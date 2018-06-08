using System;
using System.Collections.Generic;

namespace Cynthia.Chat.Common
{
    public interface IDatabaseService : IEnumerable<IDatabase>
    {
        IDatabase this[string name] { get; }
    }
}
