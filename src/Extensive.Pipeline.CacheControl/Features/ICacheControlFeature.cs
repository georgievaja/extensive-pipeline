using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extensive.Pipeline.CacheControl.Features
{
    public interface ICacheControlFeature
    {
        CacheControlKey ResourceKey { get; }
        bool ValidationSupported { get; }
        bool CacheControlSupported { get; }
        CacheContentValidators? Validators { get; }
        IDictionary<string, StringValues> CacheControlResponseHeaders { get; }
    }
}
