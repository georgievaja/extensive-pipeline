using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace Extensive.Pipeline.CacheControl.Providers
{
    public interface ICacheControlKeyProvider
    {
        [return: NotNull]
        CacheControlKey GetCacheControlKey();

        [return: NotNull]
        CacheControlKey GetCacheControlKey([DisallowNull] params string[] varyHeaders);
    }
}
