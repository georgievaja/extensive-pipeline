using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Extensive.Pipeline.CacheControl.Features
{
    public class CacheControlFeature : ICacheControlFeature
    {
        private CacheControlFeature(
            CacheControlKey key)
        {
            ResourceKey = key;
            CacheControlSupported = false;
            ValidationSupported = false;
            CacheControlResponseHeaders = new Dictionary<string, StringValues>();
        }

        private CacheControlFeature(
            IDictionary<string, StringValues> headers,
            CacheControlKey key)
        {
            ResourceKey = key;
            CacheControlSupported = true;
            ValidationSupported = false;
            CacheControlResponseHeaders = headers;
        }

        private CacheControlFeature(
            CacheContentValidators? validators, 
            IDictionary<string, StringValues> headers,
            CacheControlKey key)
        {
            ResourceKey = key;
            ValidationSupported = true;
            CacheControlSupported = true;
            CacheControlResponseHeaders = headers;
            Validators = validators;
        }

        internal static ICacheControlFeature CacheUnsupported(
            [DisallowNull] CacheControlKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return new CacheControlFeature(key);
        }

        internal static ICacheControlFeature CacheDisabled(
            [DisallowNull] CacheControlKey key,
            [DisallowNull] IDictionary<string, StringValues> headers)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (headers is null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            return new CacheControlFeature(headers, key);
        }

        internal static ICacheControlFeature CacheEnabled(
            [DisallowNull] CacheControlKey key,
            [AllowNull] CacheContentValidators? validators,
            [DisallowNull] IDictionary<string, StringValues> headers)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (headers is null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            return new CacheControlFeature(validators, headers, key);
        }

        public CacheControlKey ResourceKey { get; }

        public bool CacheControlSupported { get; }

        public CacheContentValidators? Validators { get; }

        public bool ValidationSupported { get; }

        public IDictionary<string, StringValues> CacheControlResponseHeaders { get; }
    }
}
