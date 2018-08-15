using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Updater.Common
{
    public static class ShortCache
    {
        public static ConcurrentDictionary<string,string> Urls = new ConcurrentDictionary<string, string>();
    }
}
