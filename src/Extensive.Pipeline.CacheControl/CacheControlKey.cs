using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extensive.Pipeline.CacheControl
{
    public class CacheControlKey
    {
        public CacheControlKey([NotNull] string key)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
        }

        public string Key { get; }
    }
}
