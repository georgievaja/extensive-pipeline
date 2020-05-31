using System;
using Extensive.Pipeline.CacheControl.Enums;

namespace Extensive.Pipeline.CacheControl
{
    /// <inheritdoc />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CacheControlAttribute : Attribute
    {
        public CacheControlAttribute()
        {
            CacheabilityType = CacheabilityType.Public;
        }

        public CacheabilityType CacheabilityType { get; set; }
    }
}
