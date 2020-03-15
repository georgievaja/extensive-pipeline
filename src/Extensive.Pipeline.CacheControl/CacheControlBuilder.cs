using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl
{
    public class CacheControlBuilder
    {
        private string[] supportedMethods = {"GET", "HEAD"};

        /// <summary>
        /// Adds supported methods
        /// </summary>
        /// <param name="methods">HTTP methods</param>
        public CacheControlBuilder WithSupportedMethods(
            [NotNull, ItemNotNull] string[] methods)
        {
            if (methods == null) throw new ArgumentNullException(nameof(methods));
            if (methods.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(methods));

            this.supportedMethods = methods;

            return this;
        }

        public CacheControl Build()
        {
            return new CacheControl(this.supportedMethods);
        }
    }
}
