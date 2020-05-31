using System;
using System.Diagnostics.CodeAnalysis;

namespace Extensive.Pipeline.CacheControl.Providers
{
    public interface ILastModifiedValidationProvider
    {
        bool IsValid(
            [DisallowNull] DateTimeOffset lastModifiedDate,
            [DisallowNull] CacheControlResponse key);
    }
}
