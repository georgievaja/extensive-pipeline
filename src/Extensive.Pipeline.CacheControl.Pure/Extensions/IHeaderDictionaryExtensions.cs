using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extensive.Pipeline.CacheControl.Pure.Extensions
{
    public static class IHeaderDictionaryExtensions
    {
        public static IHeaderDictionary AddRange(this IHeaderDictionary target, IDictionary<string, StringValues> source)
        {
            foreach (var header in source)
            {
                target.Add(header);
            }

            return target;
        }
    }
}
