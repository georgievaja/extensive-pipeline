using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Extensive.Pipeline.CacheControl.Pure.Extensions;

namespace Extensive.Pipeline.CacheControl.Providers
{
    public class DefaultValidatorsProvider : IValidatorsProvider
    {
        public CacheControlResponse GenerateCacheControlResponse(string content)
        {
            var etag = content.Sha256();
            var modified = DateTimeOffset.UtcNow;

            return new CacheControlResponse(etag, modified);
        }
    }
}
