using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace Extensive.Pipeline.CacheControl
{
    public class CacheControl
    {
        public CacheControl([DisallowNull] string[] supportedMethods)
        {
            if (supportedMethods == null) throw new ArgumentNullException(nameof(supportedMethods));
            if (supportedMethods.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(supportedMethods));

            SupportedMethods = supportedMethods;
        }

        public string[] SupportedMethods { get; }
    }
}
