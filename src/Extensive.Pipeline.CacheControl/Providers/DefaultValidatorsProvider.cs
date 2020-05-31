using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Extensive.Pipeline.CacheControl.Pure.Extensions;

namespace Extensive.Pipeline.CacheControl.Providers
{
    public class DefaultValidatorsProvider : IValidatorsProvider
    {
        public CacheContentValidators GenerateCacheControlResponse(string content)
        {
            var etag = content.Sha256();
            var modified = DateTimeOffset.UtcNow;

            return new CacheContentValidators(etag, modified);
        }
    }
}
