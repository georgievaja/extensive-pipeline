using System;

namespace Extensive.Pipeline.CacheControl
{
    public class CacheControlResponse
    {
        public CacheControlResponse() { }
        public CacheControlResponse(
            string eTag,
            DateTimeOffset lastModified,
            int maxAge)
        {
            ETag = eTag;
            LastModified = lastModified;
            MaxAge = maxAge;
        }
        
        /// <summary>
        /// Strong resource validator - Entity Tag
        /// </summary>
        public string ETag { get; set;  }
        
        /// <summary>
        /// Weak resource validator - Last Modified
        /// </summary>
        public DateTimeOffset LastModified { get; set; }

        /// <summary>
        /// Maximum amount of time a resource will be considered fresh.
        /// Relative to the time of the request (in seconds).
        /// </summary>
        public int MaxAge { get; set; }
    }
}
