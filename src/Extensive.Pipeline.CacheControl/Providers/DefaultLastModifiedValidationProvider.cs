using System;
using System.Collections.Generic;
using System.Text;

namespace Extensive.Pipeline.CacheControl.Providers
{
    public class DefaultLastModifiedValidationProvider : ILastModifiedValidationProvider
    {
        public bool IsValid(
            DateTimeOffset[] lastModifiedDates, 
            CacheControlResponse key)
        {
            if (lastModifiedDates == null) throw new ArgumentNullException(nameof(lastModifiedDates));
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (lastModifiedDates.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(lastModifiedDates));
            
            //TODO:validate
            return true;
        }
    }
}
