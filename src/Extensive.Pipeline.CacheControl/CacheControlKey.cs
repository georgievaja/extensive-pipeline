using System.Diagnostics.CodeAnalysis;
using System;

namespace Extensive.Pipeline.CacheControl
{
    public class CacheControlKey
    {
        public CacheControlKey([DisallowNull] string key)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
        }

        public string Key { get; }
    }
}
