using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Extensive.Pipeline.CacheControl.Providers
{
    public class MockCacheControlKeyProvider : ICacheControlKeyProvider
    {
        public CacheControlKey GetCacheControlKey()
        {
            return new CacheControlKey("teams.resource");
        }
    }
}
