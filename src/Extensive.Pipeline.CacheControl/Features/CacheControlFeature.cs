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
        private CacheControlFeature()
        {
            CacheControlSupported = false;
            ValidationSupported = false;
            CacheControlResponseHeaders = new Dictionary<string, StringValues>();
        }

        private CacheControlFeature(
            IDictionary<string, StringValues> headers)
        {
            CacheControlSupported = true;
            ValidationSupported = false;
            CacheControlResponseHeaders = headers;
        }

        private CacheControlFeature(
            CacheContentValidators? validators, 
            IDictionary<string, StringValues> headers)
        {
            ValidationSupported = true;
            CacheControlSupported = true;
            CacheControlResponseHeaders = headers;
            Validators = validators;
        }

        public static ICacheControlFeature CacheUnsupported()
        {
            return new CacheControlFeature();
        }

        public static ICacheControlFeature CacheDisabled(
            [DisallowNull] IDictionary<string, StringValues> headers)
        {
            return new CacheControlFeature(headers);
        }

        public static ICacheControlFeature CacheEnabled(
            CacheContentValidators? validators,
            [DisallowNull] IDictionary<string, StringValues> headers)
        {
            return new CacheControlFeature(validators, headers);
        }

        public bool CacheControlSupported { get; }

        public CacheContentValidators? Validators { get; }

        public bool ValidationSupported { get; }

        public IDictionary<string, StringValues> CacheControlResponseHeaders { get; }
    }
}
