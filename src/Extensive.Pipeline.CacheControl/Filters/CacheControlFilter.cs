using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Providers;
using Extensive.Pipeline.CacheControl.Pure.Extensions;
using Extensive.Pipeline.CacheControl.Pure.Functors;
using Extensive.Pipeline.CacheControl.Stores;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Extensive.Pipeline.CacheControl.Filters
{
    internal abstract class CacheControlFilter: ActionFilterAttribute
    {
        private readonly CacheControl cacheControl;
        private readonly ICacheControlKeyProvider keyProvider;
        private readonly ICacheControlStore cacheControlStore;

        protected CacheControlFilter(
            [DisallowNull] CacheControl cacheControl,
            [DisallowNull] ICacheControlKeyProvider keyProvider,
            [DisallowNull] ICacheControlStore cacheControlStore)
        {
            this.cacheControl = cacheControl ?? throw new ArgumentNullException(nameof(cacheControl));
            this.keyProvider = keyProvider ?? throw new ArgumentNullException(nameof(keyProvider));
            this.cacheControlStore = cacheControlStore ?? throw new ArgumentNullException(nameof(cacheControlStore));
        }

        protected Task<Maybe<CacheControlResponse>> TryGetCacheControlValidators(string[] additionalVaryHeaders)
        {
            var vary = GetVaryHeaders(additionalVaryHeaders);
            var key = keyProvider.GetCacheControlKey(vary);

            return cacheControlStore.TryGetCacheControlResponseAsync(key);
        }

        protected string[] GetVaryHeaders(string[] additionalVaryHeaders)
        {
            return cacheControl.DefaultVaryHeaders.DistinctConcat(additionalVaryHeaders).ToArray();
        }
    }
}
