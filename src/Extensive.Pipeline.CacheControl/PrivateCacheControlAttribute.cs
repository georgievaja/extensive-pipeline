using System;
using Extensive.Pipeline.CacheControl.Pure.Enums;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl
{
    /// <summary>
    /// Sets the response is for a single user and must not be stored by a shared cache.
    /// A private cache (like the user's browser cache) may store the response.
    /// </summary>
    public class PrivateCacheControlAttribute : CacheControlAttribute
    {
        public PrivateCacheControlAttribute(
            PrivateRevalidationType type)
        {
            CacheabilityType = type;
        }

        public PrivateCacheControlAttribute(
            [NotNull, ItemNotNull] params string[] additionalVaryHeaders)
        {
            AdditionalVaryHeaders = additionalVaryHeaders ?? throw new ArgumentNullException(nameof(additionalVaryHeaders));
        }

        public PrivateCacheControlAttribute(
            PrivateRevalidationType type, 
            [NotNull, ItemNotNull] params string[] additionalVaryHeaders)
        {
            AdditionalVaryHeaders = additionalVaryHeaders ??  throw new ArgumentNullException(nameof(additionalVaryHeaders));
            CacheabilityType = type;
        }

        /// <summary>
        /// Cache control directive
        /// </summary>
        public PrivateRevalidationType CacheabilityType { get; }

        /// <summary>
        /// Additional resource vary headers
        /// </summary>
        public string[] AdditionalVaryHeaders { get; }
    }
}
