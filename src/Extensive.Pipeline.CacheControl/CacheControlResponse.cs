using System;

namespace Extensive.Pipeline.CacheControl
{
    public class CacheControlResponse
    {
        public CacheControlResponse(
            DateTimeOffset created,
            string eTag,
            DateTimeOffset lastModified)
        {
            Created = created;
            ETag = eTag;
            LastModified = lastModified;
        }

        public DateTimeOffset Created { get; set; }
        public string ETag { get; set;  }
        public DateTimeOffset LastModified { get; set; }
    }
}
