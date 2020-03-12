using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl.Providers
{
    public sealed class DefaultETagValidationProvider : IETagValidationProvider
    {
        public bool IsValid(
            NormalizedHeader[] eTags,
            CacheControlResponse key)
        {
            if (eTags == null) throw new ArgumentNullException(nameof(eTags));
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (eTags.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(eTags));

            //TODO: validate
            return true;
        }
    }
}
