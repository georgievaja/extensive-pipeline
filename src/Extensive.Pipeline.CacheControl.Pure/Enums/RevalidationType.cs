namespace Extensive.Pipeline.CacheControl.Pure.Enums
{
    /// <summary>
    /// Cache control directives - cacheability revalidation types
    /// </summary>
    public enum RevalidationType
    {
        /// <summary>
        /// Caches must check with the origin server for validation before using the cached copy.
        /// </summary>
        NoCache,

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

    /// <summary>
    /// Cache control directives for private cache - cacheability revalidation types
    /// </summary>
    public enum PrivateRevalidationType
    {
        /// <summary>
        /// Caches must check with the origin server for validation before using the cached copy.
        /// </summary>
        NoCache,

        /// <summary>
        /// The cache must verify the status of the stale resources before using it 
        /// and expired ones should not be used.
        /// </summary>
        MustRevalidate
    }
}
