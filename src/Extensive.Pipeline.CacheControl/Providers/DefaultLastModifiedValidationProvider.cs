using System;
using System.Collections.Generic;
using System.Text;

namespace Extensive.Pipeline.CacheControl.Providers
{
    public class DefaultLastModifiedValidationProvider : ILastModifiedValidationProvider
    {
        public bool IsValid(
            DateTimeOffset lastModifiedDate, 
            CacheControlResponse key)
        {
            if (lastModifiedDate == null) throw new ArgumentNullException(nameof(lastModifiedDate));
            if (key == null) throw new ArgumentNullException(nameof(key));
            
            return key.LastModified.Equals(lastModifiedDate);
        }
    }
}
