using Extensive.Pipeline.CacheControl.Attributes;
using Extensive.Pipeline.CacheControl.Providers;
using Extensive.Pipeline.CacheControl.Pure.Extensions;
using Extensive.Pipeline.CacheControl.Stores;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensive.Pipeline.CacheControl.Features.FeatureHandler
{
    public abstract class CacheControlFeatureHandler : ICacheControlFeatureHandler
    {
        private ICacheControlFeatureHandler? nextValidator;
        private readonly CacheControl cacheControl;
        private readonly ICacheControlKeyProvider keyProvider;
        private readonly ICacheControlStore cacheControlStore;

        protected CacheControlFeatureHandler(
            [DisallowNull] CacheControl cacheControl,
            [DisallowNull] ICacheControlKeyProvider keyProvider,
            [DisallowNull] ICacheControlStore cacheControlStore)
        {
            this.cacheControl = cacheControl ?? throw new ArgumentNullException(nameof(cacheControl));
            this.keyProvider = keyProvider ?? throw new ArgumentNullException(nameof(keyProvider));
            this.cacheControlStore = cacheControlStore ?? throw new ArgumentNullException(nameof(cacheControlStore));
        }


        public ICacheControlFeatureHandler SetNext(
            ICacheControlFeatureHandler nextValidator)
        {
            this.nextValidator = nextValidator ?? throw new ArgumentNullException(nameof(nextValidator));

            return nextValidator;
        }

        public virtual async Task<ICacheControlFeature> GetCacheControlFeature(
            CacheControlAttribute descriptor)
        {
            if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));

            return nextValidator == null ? CacheControlFeature.CacheUnsupported() : await nextValidator.GetCacheControlFeature(descriptor);
        }

        protected Task<CacheContentValidators?> TryGetCacheControlValidators(string[] additionalVaryHeaders)
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
