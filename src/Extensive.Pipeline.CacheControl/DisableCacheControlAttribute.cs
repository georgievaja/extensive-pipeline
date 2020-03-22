using System;
using Extensive.Pipeline.CacheControl.Pure.Enums;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl
{
    /// <summary>
    /// Sets no-store for resources
    /// </summary>
    public class DisableCacheControlAttribute : CacheControlAttribute
    {
    }
}
