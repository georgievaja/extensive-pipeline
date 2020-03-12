using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl.Stores
{
    public interface ICacheStore
    {
        Task<CacheControlResponse> GetCacheControlResponseAsync([NotNull] CacheControlKey key);
        Task SetCacheControlResponseAsync([NotNull] CacheControlKey key, [NotNull] CacheControlResponse response);
    }
}
