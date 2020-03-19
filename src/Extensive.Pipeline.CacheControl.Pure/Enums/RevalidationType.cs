using System;
using System.Collections.Generic;
using System.Text;

namespace Extensive.Pipeline.CacheControl.Pure.Enums
{
    /// <summary>
    /// Cache control directives - revalidation types
    /// </summary>
    public enum RevalidationType
    {
        /// <summary>
        /// The cache must verify the status of the stale resources before using it 
        /// and expired ones should not be used.
        /// </summary>
        MustRevalidate,

        /// <summary>
        /// The cache must be verified, but only by proxies, not by private caches
        /// </summary>
        ProxyRevalidate
    }
}
