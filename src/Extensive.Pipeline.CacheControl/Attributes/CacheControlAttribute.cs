using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Extensive.Pipeline.CacheControl.Attributes
{
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public abstract class CacheControlAttribute : Attribute, IFilterMetadata
    {
    }
}
