using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl.Providers
{
    public interface IETagValidationProvider
    {
        [Pure]
        bool IsValid(
            [NotNull] NormalizedHeader[] eTags,
            [NotNull] CacheControlResponse key);
    }
}
