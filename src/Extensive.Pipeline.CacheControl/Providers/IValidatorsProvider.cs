using System;
using System.Collections.Generic;
using System.Text;

namespace Extensive.Pipeline.CacheControl.Providers
{
    public interface IValidatorsProvider
    {
        CacheContentValidators GenerateCacheControlResponse(string content);
    }
}
