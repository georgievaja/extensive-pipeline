using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Extensive.Pipeline.CacheControl.Pure.Enums;
using Microsoft.Net.Http.Headers;

namespace Extensive.Pipeline.CacheControl
{
    public sealed class CacheControlBuilder
    {
        private HttpMethod[] supportedMethods = {
            HttpMethod.Get, 
            HttpMethod.Head
        };

        private string[] defaultVaryHeaders = { 
            HeaderNames.ContentEncoding, 
            HeaderNames.ContentLanguage, 
            HeaderNames.ContentType };


        /// <summary>
        /// Adds supported methods
        /// </summary>
        /// <param name="methods">HTTP methods</param>
        [return: NotNull]
        public CacheControlBuilder WithSupportedMethods(
            [DisallowNull] HttpMethod[] methods)
        {
            if (methods == null) throw new ArgumentNullException(nameof(methods));
            if (methods.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(methods));

            this.supportedMethods = methods;

            return this;
        }

        /// <summary>
        /// Adds default vary headers
        /// </summary>
        /// <param name="headers">HTTP headers</param>
        public CacheControlBuilder WithDefaultVaryHeaders(
            [NotNull, ItemNotNull] string[] headers)
        {
            if (headers == null) throw new ArgumentNullException(nameof(headers));
            if (headers.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(headers));

            this.defaultVaryHeaders = headers;

            return this;
        }

        [return: NotNull]
        public CacheControl Build()
        {
            return new CacheControl(
                this.supportedMethods, 
                this.defaultVaryHeaders);
        }
    }
}
