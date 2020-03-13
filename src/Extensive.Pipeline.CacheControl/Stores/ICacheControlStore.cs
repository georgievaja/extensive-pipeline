using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Pure.Functors;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl.Stores
{
    public interface ICacheControlStore
    {
        Task<Maybe<CacheControlResponse>> TryGetCacheControlResponseAsync([NotNull] CacheControlKey key);
        Task SetCacheControlResponseAsync([NotNull] CacheControlKey key, [NotNull] CacheControlResponse response);
    }
}
