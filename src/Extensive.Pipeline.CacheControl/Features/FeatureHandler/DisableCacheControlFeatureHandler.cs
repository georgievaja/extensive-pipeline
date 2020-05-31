using Extensive.Pipeline.CacheControl.Attributes;
using Extensive.Pipeline.CacheControl.Providers;
using Extensive.Pipeline.CacheControl.Stores;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Extensive.Pipeline.CacheControl.Features.FeatureHandler
{
    public class DisableCacheControlFeatureHandler : CacheControlFeatureHandler
    {

        public DisableCacheControlFeatureHandler(
           [DisallowNull] ICacheControlKeyProvider keyProvider,
           [DisallowNull] CacheControl cacheControl,
           [DisallowNull] ICacheControlStore cacheControlStore)
           : base(cacheControl, keyProvider, cacheControlStore)
        {
        }

        public override Task<ICacheControlFeature> GetCacheControlFeature(
            CacheControlAttribute directive)
        {
            if (directive == null) throw new ArgumentNullException(nameof(directive));

            if (directive is DisableCacheControlAttribute _)
            {
                var key = base.GetCacheControlKey();

                var headers = new Dictionary<string, StringValues>() 
                { 
                    { HeaderNames.CacheControl, new[] { "no-store" } 
                    } 
                };

                var feature = CacheControlFeature.CacheDisabled(key, headers);

                return Task.FromResult(feature);
            }

            return base.GetCacheControlFeature(directive);
        }
    }
}
