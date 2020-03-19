using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Extensive.Pipeline.CacheControl.Enums;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl
{
    public class CacheControl
    {
        public CacheControl(
            [NotNull, ItemNotNull] HttpMethod[] supportedMethods,
            [NotNull, ItemNotNull] string[] defaultVaryHeaders,
            CacheabilityType cacheabilityType)
        {
            if (supportedMethods == null) throw new ArgumentNullException(nameof(supportedMethods));
            if (defaultVaryHeaders == null) throw new ArgumentNullException(nameof(defaultVaryHeaders));

            if (supportedMethods.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(supportedMethods));
            if (defaultVaryHeaders.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(defaultVaryHeaders));

            SupportedMethods = supportedMethods;
            DefaultVaryHeaders = defaultVaryHeaders;
            DefaultCacheabilityType = cacheabilityType;
        }

        public HttpMethod[] SupportedMethods { get; }
        public string[] DefaultVaryHeaders { get; }
        public CacheabilityType DefaultCacheabilityType { get; }
    }
}
