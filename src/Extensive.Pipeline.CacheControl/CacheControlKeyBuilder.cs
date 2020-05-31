using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Extensive.Pipeline.CacheControl
{
    internal sealed class CacheControlKeyBuilder
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
        [return: NotNull]
        public CacheControlKeyBuilder WithMethod(
            [DisallowNull] string method)
        {
            if (method is null) throw new ArgumentNullException(nameof(method));

            var methodKey = method.ToUpperInvariant();
            this.key = $"{this.key}.M:{methodKey}";

            return this;
        }

        /// <summary>
        /// Adds scheme key
        /// </summary>
        /// <param name="scheme">HTTP scheme</param>
        /// <param name="m">HTTP scheme</param>
        [return: NotNull]
        public CacheControlKeyBuilder WithScheme(
            [DisallowNull] string scheme)
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
        [return: NotNull]
        public CacheControlKeyBuilder WithHost(
            [DisallowNull] string host)
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
        [return: NotNull]
        public CacheControlKeyBuilder WithPathBase(
            [DisallowNull] string pathBase)
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
        [return: NotNull]
        public CacheControlKeyBuilder WithPath(
            [DisallowNull] string path)
        {
            if (path is null) throw new ArgumentNullException(nameof(path));

            var pathKey = path.ToUpperInvariant();
            this.key = $"{this.key}.P:{pathKey}";

            return this;
        }

        /// <summary>
        /// Adds vary headers
        /// </summary>
        /// <param name="headers">VARY headers</param>
        public CacheControlKeyBuilder WithVaryHeaders(
            [NotNull, ItemNotNull] string[] headers)
        {
            if (headers == null) throw new ArgumentNullException(nameof(headers));
            if (headers.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(headers));

            var vhKey = string.Concat(headers, "|");
            this.key = $"{this.key}.VH:{vhKey}";

            return this;
        }

        /// <summary>
        /// Adds query string values
        /// </summary>
        /// <param name="queryCollection">HTTP query string collection</param>
        public CacheControlKeyBuilder WithQueryStrings(
            [NotNull] IQueryCollection queryCollection)
        {
            if (queryCollection == null) throw new ArgumentNullException(nameof(queryCollection));

            var qCKey = queryCollection
                .Select(m => $"{m.Key}:{m.Value}");

            this.key = $"{this.key}.QC:{string.Concat(qCKey, "|")}";

            return this;
        }

        public CacheControlKey Build()
        {
            return new CacheControlKey(this.key);
        }
    }
}
