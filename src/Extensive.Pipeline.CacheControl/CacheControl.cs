using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using Extensive.Pipeline.CacheControl.Pure.Enums;

namespace Extensive.Pipeline.CacheControl
{
    public sealed class CacheControl
    {
        public CacheControl(
            [DisallowNull] HttpMethod[] supportedMethods,
            [DisallowNull] string[] defaultVaryHeaders)
        {
            if (supportedMethods == null) throw new ArgumentNullException(nameof(supportedMethods));
            if (defaultVaryHeaders == null) throw new ArgumentNullException(nameof(defaultVaryHeaders));

            if (supportedMethods.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(supportedMethods));
            if (defaultVaryHeaders.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(defaultVaryHeaders));

            SupportedMethods = supportedMethods;
            DefaultVaryHeaders = defaultVaryHeaders;
        }

        public HttpMethod[] SupportedMethods { get; }
        public string[] DefaultVaryHeaders { get; }
    }
}
