using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl.Providers
{
    public interface ICacheControlKeyProvider
    {
        [NotNull]
        CacheControlKey GetCacheControlKey();

        [NotNull]
        CacheControlKey GetCacheControlKey([ItemNotNull, NotNull] params string[] varyHeaders);
    }
}
