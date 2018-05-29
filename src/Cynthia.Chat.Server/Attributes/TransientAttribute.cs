using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cynthia.Chat.Server.Attributes;

namespace Cynthia.Test.Chat.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TransientAttribute : ServiceAttribute
    {
    }
}
