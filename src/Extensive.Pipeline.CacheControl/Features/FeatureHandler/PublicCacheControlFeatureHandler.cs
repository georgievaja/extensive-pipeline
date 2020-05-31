using Extensive.Pipeline.CacheControl.Attributes;
using Extensive.Pipeline.CacheControl.Providers;
using Extensive.Pipeline.CacheControl.Stores;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace Extensive.Pipeline.CacheControl.Features.FeatureHandler
{
    public class PublicCacheControlFeatureHandler : CacheControlFeatureHandler
    {
        private readonly ICacheControlKeyProvider keyProvider;
        private readonly ICacheControlStore cacheControlStore;

        public PublicCacheControlFeatureHandler(
           [DisallowNull] ICacheControlKeyProvider keyProvider,
           [DisallowNull] CacheControl cacheControl,
           [DisallowNull] ICacheControlStore cacheControlStore)
           : base(cacheControl, keyProvider, cacheControlStore)
        {
            this.cacheControlStore = cacheControlStore ?? throw new ArgumentNullException(nameof(cacheControlStore));
            this.keyProvider = keyProvider ?? throw new ArgumentNullException(nameof(keyProvider));
        }

        public override async Task<ICacheControlFeature> GetCacheControlFeature(
            CacheControlAttribute directive)
        {
            if (directive == null) throw new ArgumentNullException(nameof(directive));

            if (directive is PublicCacheControlAttribute attr)
            {
                var validators = await base.TryGetCacheControlValidators(attr.AdditionalVaryHeaders);

                var headers = new Dictionary<string, StringValues>()
                {
                        { HeaderNames.CacheControl, new[]
                               {"public", $"max-age={attr.MaxAge}"} },
                    { HeaderNames.Vary, base.GetVaryHeaders(attr.AdditionalVaryHeaders) }
                };

                return CacheControlFeature.CacheEnabled(validators, headers);
            }

            return await base.GetCacheControlFeature(directive);
        }
    }
}
