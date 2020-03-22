using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extensive.Pipeline.CacheControl
{
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public abstract class CacheControlAttribute : Attribute
    {
    }
}
