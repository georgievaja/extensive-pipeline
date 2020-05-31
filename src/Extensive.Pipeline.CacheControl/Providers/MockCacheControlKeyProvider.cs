using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Extensive.Pipeline.CacheControl.Providers
{
    public sealed class MockCacheControlKeyProvider : ICacheControlKeyProvider
    {
        public CacheControlKey GetCacheControlKey()
        {
            return new CacheControlKey("teams.resource");
        }

        public CacheControlKey GetCacheControlKey(string[] varyHeaders)
        {
            return new CacheControlKey("teams.resource");
        }
    }
}
