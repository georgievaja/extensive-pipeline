using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl
{
    public class CacheControlKeyBuilder
    {
        private string key;

        public CacheControlKeyBuilder()
        {
            key = "CC.KEY:";
        }

        /// <summary>
        /// Adds method key
        /// </summary>
        /// <param name="method">HTTP method</param>
        public CacheControlKeyBuilder WithMethod(
            [NotNull] string method)
        {
            if (method is null) throw new ArgumentNullException(nameof(method));

            var methodKey = method.ToUpperInvariant();
            this.key = $"{this.key}.M:{methodKey}";

            return this;
        }

        /// <summary>
        /// Adds scheme key
        /// </summary>
        /// <param name="m">HTTP scheme</param>
        public CacheControlKeyBuilder WithScheme(
            [NotNull] string scheme)
        {
            if (scheme is null) throw new ArgumentNullException(nameof(scheme));

            var schemeKey = scheme.ToUpperInvariant();
            this.key = $"{this.key}.S:{schemeKey}";

            return this;
        }

        /// <summary>
        /// Adds host key
        /// </summary>
        /// <param name="host">HTTP host</param>
        public CacheControlKeyBuilder WithHost(
            [NotNull] string host)
        {
            if (host is null) throw new ArgumentNullException(nameof(host));

            var hostKey = host.ToUpperInvariant();
            this.key = $"{this.key}.H:{hostKey}";

            return this;
        }

        /// <summary>
        /// Adds base path
        /// </summary>
        /// <param name="pathBase">HTTP base path</param>
        public CacheControlKeyBuilder WithPathBase(
            [NotNull] string pathBase)
        {
            if (pathBase is null) throw new ArgumentNullException(nameof(pathBase));

            var pathBaseKey = pathBase.ToUpperInvariant();
            this.key = $"{this.key}.PB:{pathBaseKey}";

            return this;
        }

        /// <summary>
        /// Adds path
        /// </summary>
        /// <param name="path">HTTP base path</param>
        public CacheControlKeyBuilder WithPath(
            [NotNull] string path)
        {
            if (path is null) throw new ArgumentNullException(nameof(path));

            var pathKey = path.ToUpperInvariant();
            this.key = $"{this.key}.P:{pathKey}";

            return this;
        }

        public CacheControlKey Build()
        {
            return new CacheControlKey(this.key);
        }
    }
}
