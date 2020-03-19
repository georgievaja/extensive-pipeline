using System;
using Extensive.Pipeline.CacheControl.Enums;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl
{
    /// <inheritdoc />
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CacheControlAttribute : Attribute
    {
        public CacheControlAttribute(
            CacheabilityType type)
        {
            CacheabilityType = type;
        }

        public CacheControlAttribute(
            [NotNull, ItemNotNull] params string[] additionalVaryHeaders)
        {
            AdditionalVaryHeaders = additionalVaryHeaders ?? throw new ArgumentNullException(nameof(additionalVaryHeaders));
        }

        public CacheControlAttribute(
            CacheabilityType type, 
            [NotNull, ItemNotNull] params string[] additionalVaryHeaders)
        {
            AdditionalVaryHeaders = additionalVaryHeaders ??  throw new ArgumentNullException(nameof(additionalVaryHeaders));
            CacheabilityType = type;
        }

        public CacheabilityType CacheabilityType { get; }

        public string[] AdditionalVaryHeaders { get; }
    }
}
