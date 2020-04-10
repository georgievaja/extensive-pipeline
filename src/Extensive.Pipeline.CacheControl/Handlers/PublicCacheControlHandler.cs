using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Providers;
using Extensive.Pipeline.CacheControl.Stores;
using Extensive.Pipeline.CacheControl.Validators;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace Extensive.Pipeline.CacheControl.Handlers
{
    public class PublicCacheControlHandler : ControlHandler
    {
        private readonly ICacheControlStore cacheStore;
        private readonly ICacheControlKeyProvider cacheControlKeyProvider;
        private readonly IValidator validator;

        public PublicCacheControlHandler(
            [NotNull] ICacheControlStore cacheStore,
            [NotNull] ICacheControlKeyProvider cacheControlKeyProvider,
            [NotNull] IValidator validator
            )
        {
            this.cacheStore = cacheStore ?? throw new ArgumentNullException(nameof(cacheStore));
            this.cacheControlKeyProvider = cacheControlKeyProvider ?? throw new ArgumentNullException(nameof(cacheControlKeyProvider));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public override async Task<IDictionary<string, StringValues>> ControlCache(
            [NotNull] CacheControlAttribute directive)
        {
            if (directive == null) throw new ArgumentNullException(nameof(directive));

            if (directive is PublicCacheControlAttribute publicDirective)
            {

            }
            return await base.ControlCache(directive);
        }
    }
}
