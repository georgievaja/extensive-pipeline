using System;
using System.Collections.Generic;
using System.Text;

namespace Extensive.Pipeline.CacheControl.Enums
{
    /// <summary>
    /// Cache control directives - cacheability types
    /// </summary>
    public enum CacheabilityType
    {
        /// <summary>
        /// The response may be cached by any cache.
        /// </summary>
        Public,

        /// <summary>
        /// The response is for a single user and must not be stored by a shared cache.
        /// A private cache (like the user's browser cache) may store the response.
        /// </summary>
        Private,

        /// <summary>
        /// Caches must check with the origin server for validation before using the cached copy.
        /// </summary>
        NoCache,

        /// <summary>
        /// The cache should not store anything about the client request or server response.
        /// </summary>
        NoStore,
    }
}
