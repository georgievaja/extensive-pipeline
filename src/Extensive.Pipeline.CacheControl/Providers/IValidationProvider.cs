using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl.Providers
{
    public interface IValidationProvider
    {
        [Pure]
        bool IsValid(
            [NotNull] NormalizedHeader[] eTags, 
            [NotNull] CacheControlResponse key);
    }
}
