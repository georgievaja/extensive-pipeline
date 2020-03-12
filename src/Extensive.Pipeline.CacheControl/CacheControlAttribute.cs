using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl
{
    /// <inheritdoc />
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CacheControlAttribute : Attribute
    {
        public bool IsPrivate { get; set; }
        public int MaxAge { get; set; }
    }
}
