using System;
using Extensive.Pipeline.CacheControl.Pure.Enums;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl.Attributes
{
    /// <summary>
    /// Sets the response is for a single user and must not be stored by a shared cache.
    /// A private cache (like the user's browser cache) may store the response.
    /// </summary>
    public class PrivateCacheControlAttribute : CacheControlAttribute
    {
        /// <summary>
        /// Cache control directive
        /// </summary>
        public PrivateRevalidationType CacheabilityType { get; set;  }

        /// <summary>
        /// Additional resource vary headers
        /// </summary>
        public string[] AdditionalVaryHeaders { get; set;  } = new string[0];

        /// <summary>
        /// Maximum amount of time a resource will be considered fresh.
        /// Relative to the time of the request (in seconds).
        /// </summary>
        public int MaxAge { get; set; }
    }
}
