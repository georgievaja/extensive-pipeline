using System;
using Extensive.Pipeline.CacheControl.Pure.Enums;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl
{
    /// <inheritdoc />
    public class PublicCacheControlAttribute : CacheControlAttribute
    {
        public PublicCacheControlAttribute(
            RevalidationType type)
        {
            CacheabilityType = type;
        }

        public PublicCacheControlAttribute(
            [NotNull, ItemNotNull] params string[] additionalVaryHeaders)
        {
            AdditionalVaryHeaders = additionalVaryHeaders ?? throw new ArgumentNullException(nameof(additionalVaryHeaders));
        }

        public PublicCacheControlAttribute(
            RevalidationType type, 
            [NotNull, ItemNotNull] params string[] additionalVaryHeaders)
        {
            AdditionalVaryHeaders = additionalVaryHeaders ??  throw new ArgumentNullException(nameof(additionalVaryHeaders));
            CacheabilityType = type;
        }

        /// <summary>
        /// Cache control directive
        /// </summary>
        public RevalidationType CacheabilityType { get; }

        /// <summary>
        /// Additional resource vary headers
        /// </summary>
        public string[] AdditionalVaryHeaders { get; }

        /// <summary>
        /// Maximum amount of time a resource will be considered fresh.
        /// Relative to the time of the request (in seconds).
        /// </summary>
        public int MaxAge { get; set; }
    }
}
