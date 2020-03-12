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
        private string host;
        private string pathBase;
        private string path;
        private string[] varyHeaders;
        private string[] varyQueries;

        /// <summary>
        /// Adds method key
        /// </summary>
        /// <param name="m">HTTP method</param>
        public CacheControlKeyBuilder WithMethod(
            [NotNull] string m)
        {
            if (m is null) throw new ArgumentNullException(nameof(m));

            var methodKey = m.ToUpperInvariant();
            this.key = $"{this.key}.M{methodKey}";

            return this;
        }

        /// <summary>
        /// Adds scheme key
        /// </summary>
        /// <param name="m">HTTP scheme</param>
        public CacheControlKeyBuilder WithScheme(
            [NotNull] string s)
        {
            if (s is null) throw new ArgumentNullException(nameof(s));

            var schemeKey = s.ToUpperInvariant();
            this.key = $"{this.key}.S{schemeKey}";

            return this;
        }

        public CacheControlKey Build()
        {
            return new CacheControlKey(this.key);
        }
    }
}
