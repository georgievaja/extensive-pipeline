using System;
using System.Collections.Generic;
using System.Linq;
using Extensive.Pipeline.CacheControl.Pure.Functors;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Extensive.Pipeline.CacheControl.Validators
{
    internal sealed class IfModifiedSinceValidator : Validator
    {
        public override Maybe<IHeaderDictionary> TryValidate(
            IHeaderDictionary headers,
            CacheContentValidators response)
        {
            if (headers == null) throw new ArgumentNullException(nameof(headers));
            if (response == null) throw new ArgumentNullException(nameof(response));

            if (headers.ContainsKey(HeaderNames.IfModifiedSince))
            {
                var modDate = headers[HeaderNames.IfModifiedSince];
                var equal = response.LastModified.Equals(DateTimeOffset.Parse(modDate));

                return equal ? 
                    Maybe<IHeaderDictionary>.Some(response.GetHeaders()):
                    Maybe<IHeaderDictionary>.None;
            }

            return base.TryValidate(headers, response);
        }
    }
}
