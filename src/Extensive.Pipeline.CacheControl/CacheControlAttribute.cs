using System;
using System.Collections.Generic;
using System.Text;
using Extensive.Pipeline.CacheControl.Enums;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl
{
    /// <inheritdoc />
    [PublicAPI]
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
