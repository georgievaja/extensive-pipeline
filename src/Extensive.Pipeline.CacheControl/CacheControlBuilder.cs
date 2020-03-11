using JetBrains.Annotations;
using System;
using System.Linq;
using static Extensive.Pipeline.CacheControl.NormalizedHeaderExtensions;
namespace Extensive.Pipeline.CacheControl
{
    public sealed class CacheControlBuilder
    {
        private NormalizedHeader[] varyHeaders;
        private NormalizedHeader[] varyQueryStrings;

        /// <summary>
        /// Adds supported vary headers
        /// </summary>
        /// <param name="headers">Normalized vary headers</param>
        public CacheControlBuilder WithVaryHeaders(
            [NotNull, ItemNotNull] string[] headers)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            this.varyHeaders = headers.Select(NormalizedHeaderExtensions.Normalize).ToArray();
            return this;
        }

        /// <summary>
        /// Adds supported vary by query string headers
        /// </summary>
        /// <param name="qs"></param>
        /// <returns></returns>
        public CacheControlBuilder WithVaryQueryStrings(
            [NotNull, ItemNotNull] string[] qs)
        {
            if (qs is null)
            {
                throw new ArgumentNullException(nameof(qs));
            }

            this.varyQueryStrings = qs.Select(NormalizedHeaderExtensions.Normalize).ToArray();

            return this;
        }

        public CacheControl Build()
        {
            return new CacheControl(this.varyHeaders, this.varyQueryStrings);
        }
    }
}
