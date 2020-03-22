using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace Extensive.Pipeline.CacheControl
{
    public class CacheControlResponse
    {
        public CacheControlResponse() { }
        public CacheControlResponse(
            string eTag,
            DateTimeOffset lastModified)
        {
            ETag = eTag;
            LastModified = lastModified;
        }
        
        /// <summary>
        /// Strong resource validator - Entity Tag
        /// </summary>
        public string ETag { get; set; }
        
        /// <summary>
        /// Weak resource validator - Last Modified
        /// </summary>
        public DateTimeOffset LastModified { get; set; }


        /// <summary>
        /// Prepare headers dictionary
        /// </summary>
        public IHeaderDictionary GetHeaders()
        {
            var dict = new Dictionary<string, StringValues>
            {
                {HeaderNames.ETag, new StringValues(this.ETag)},
                {HeaderNames.LastModified, new StringValues(this.LastModified.ToString())}
            };

            return new HeaderDictionary(dict);
        }

    }
}
