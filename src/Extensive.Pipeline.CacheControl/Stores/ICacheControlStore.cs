using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Pure.Functors;
using System.Diagnostics.CodeAnalysis;

namespace Extensive.Pipeline.CacheControl.Stores
{
    public interface ICacheControlStore
    {
        Task<CacheContentValidators?> TryGetCacheControlResponseAsync([DisallowNull] CacheControlKey key);
        Task SetCacheControlResponseAsync([DisallowNull] CacheControlKey key, [DisallowNull] CacheContentValidators response);
    }
}
