using System;
using Extensive.Pipeline.CacheControl.Pure.Enums;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl.Attributes
{
    /// <inheritdoc />
    public class PublicCacheControlAttribute : CacheControlAttribute
    {
        /// <summary>
        /// Cache control directive
        /// </summary>
        public RevalidationType CacheabilityType { get; set; }

        /// <summary>
        /// Additional resource vary headers
        /// </summary>
        public string[] AdditionalVaryHeaders { get; set; }

        /// <summary>
        /// Maximum amount of time a resource will be considered fresh.
        /// Relative to the time of the request (in seconds).
        /// </summary>
        public int MaxAge { get; set; }
    }
}
