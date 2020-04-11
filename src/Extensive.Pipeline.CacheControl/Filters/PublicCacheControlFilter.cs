using System;
using Extensive.Pipeline.CacheControl.Attributes;
using Extensive.Pipeline.CacheControl.Pure.Enums;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl.Filters
{
    /// <inheritdoc />
    public class PublicCacheControlFilter : CacheControlAttribute
    {
        private readonly CacheControl cacheControl;

        public PublicCacheControlFilter(
            [NotNull] CacheControl cacheControl)
        {
            this.cacheControl = cacheControl ?? throw new ArgumentNullException(nameof(cacheControl));
        }
    }
}
