using System.Diagnostics.CodeAnalysis;

namespace Extensive.Pipeline.CacheControl.Providers
{
    public interface IETagValidationProvider
    {
        bool IsValid(
            [DisallowNull] NormalizedHeader[] eTags,
            [DisallowNull] CacheControlResponse key);
    }
}
