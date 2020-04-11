using System;
using System.Linq;
using System.Net.Http;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Extensive.Pipeline.CacheControl.Attributes
{
    /// <summary>
    /// Sets no-store for resources
    /// </summary>
    public class DisableCacheControlAttribute : ActionFilterAttribute
    {
    }
}
