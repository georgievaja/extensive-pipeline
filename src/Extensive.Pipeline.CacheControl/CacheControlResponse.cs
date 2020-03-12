using System;
using System.Collections.Generic;
using System.Text;

namespace Extensive.Pipeline.CacheControl
{
    public class CacheControlResponse
    {
        public CacheControlResponse(
            DateTimeOffset created,
            NormalizedHeader eTag,
            DateTimeOffset lastModified)
        {
            Created = created;
            ETag = eTag;
            LastModified = lastModified;
        }

        public DateTimeOffset Created { get; }
        public NormalizedHeader ETag { get; }
        public DateTimeOffset LastModified { get; }
    }
}
