using System;
using System.Linq;
using Extensive.Pipeline.CacheControl.Pure.Functors;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Extensive.Pipeline.CacheControl.Validators
{
    internal sealed class IfNoneMatchValidator : Validator
    {
        public Maybe<IHeaderDictionary> TryValidate(IHeaderDictionary headers, CacheControlResponse response)
        {
            if (headers == null) throw new ArgumentNullException(nameof(headers));
            if (response == null) throw new ArgumentNullException(nameof(response));

            if (headers.ContainsKey(HeaderNames.IfNoneMatch))
            {
                var eTags = headers[HeaderNames.IfNoneMatch].ToArray();
                var someEqual = eTags.Any(m => 
                    m.Equals(response.ETag, StringComparison.InvariantCultureIgnoreCase));

                return someEqual ?
                    Maybe<IHeaderDictionary>.Some(response.GetHeaders()) :
                    Maybe<IHeaderDictionary>.None;
            }

            return base.TryValidate(headers, response);
        }
    }
}
