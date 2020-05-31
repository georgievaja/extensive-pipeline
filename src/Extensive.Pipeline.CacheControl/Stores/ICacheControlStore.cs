using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Pure.Functors;
using System.Diagnostics.CodeAnalysis;

namespace Extensive.Pipeline.CacheControl.Stores
{
    public interface ICacheControlStore
    {
        Task<Maybe<CacheControlResponse>> TryGetCacheControlResponseAsync([DisallowNull] CacheControlKey key);
        Task SetCacheControlResponseAsync([DisallowNull] CacheControlKey key, [DisallowNull] CacheControlResponse response);
    }
}
