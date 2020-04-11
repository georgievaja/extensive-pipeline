using System;
using Extensive.Pipeline.CacheControl.Attributes;
using Extensive.Pipeline.CacheControl.Pure.Enums;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl.Filters
{
    /// <summary>
    /// Sets the response is for a single user and must not be stored by a shared cache.
    /// A private cache (like the user's browser cache) may store the response.
    /// </summary>
    public class PrivateCacheControlFilter : CacheControlAttribute
    {
        private readonly CacheControl cacheControl;

        public PrivateCacheControlFilter(
            [NotNull] CacheControl cacheControl)
        {
            this.cacheControl = cacheControl ?? throw new ArgumentNullException(nameof(cacheControl));
        }
    }
}
