using System;
using System.Diagnostics.CodeAnalysis;

namespace Extensive.Pipeline.CacheControl
{
    public class CacheControlBuilder
    {
        private string[] supportedMethods = {"GET", "HEAD"};

        /// <summary>
        /// Adds supported methods
        /// </summary>
        /// <param name="methods">HTTP methods</param>
        [return: NotNull]
        public CacheControlBuilder WithSupportedMethods(
            [DisallowNull] string[] methods)
        {
            if (methods == null) throw new ArgumentNullException(nameof(methods));
            if (methods.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(methods));

            this.supportedMethods = methods;

            return this;
        }

        [return: NotNull]
        public CacheControl Build()
        {
            return new CacheControl(this.supportedMethods);
        }
    }
}
