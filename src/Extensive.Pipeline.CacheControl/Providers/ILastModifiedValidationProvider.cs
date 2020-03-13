using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl.Providers
{
    public interface ILastModifiedValidationProvider
    {
        [Pure]
        bool IsValid(
            [NotNull] DateTimeOffset lastModifiedDate,
            [NotNull] CacheControlResponse key);
    }
}
