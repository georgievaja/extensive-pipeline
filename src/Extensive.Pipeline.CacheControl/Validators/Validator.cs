using System;
using System.Collections.Generic;
using Extensive.Pipeline.CacheControl.Pure.Functors;
using Microsoft.AspNetCore.Http;

namespace Extensive.Pipeline.CacheControl.Validators
{
    internal abstract class Validator : IValidator
    {
        private IValidator nextValidator;

        public IValidator SetNext(
            IValidator nextValidator)
        {
            this.nextValidator = nextValidator ?? throw new ArgumentNullException(nameof(nextValidator));

            return nextValidator;
        }

        public virtual Maybe<IHeaderDictionary> TryValidate(
            IHeaderDictionary headers,
            CacheControlResponse response)
        {
            if (headers == null) throw new ArgumentNullException(nameof(headers));
            if (response == null) throw new ArgumentNullException(nameof(response));

            return nextValidator.TryValidate(headers, response);
        }
    }
}
