using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl
{
    public class CacheControl
    {
        public CacheControl([NotNull] string[] supportedMethods)
        {
            if (supportedMethods == null) throw new ArgumentNullException(nameof(supportedMethods));
            if (supportedMethods.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(supportedMethods));

            SupportedMethods = supportedMethods;
        }

        public string[] SupportedMethods { get; }
    }
}
