using System;
using System.Collections.Generic;
using System.Text;
using Extensive.Pipeline.CacheControl.Pure.Functors;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Extensive.Pipeline.CacheControl.Validators
{
    public class SuccessorValidator : Validator
    {
        public override Maybe<IHeaderDictionary> TryValidate(IHeaderDictionary headers, CacheControlResponse response)
        {
            try
            {
                return base.TryValidate(headers, response);
            }
            catch
            {
                return Maybe<IHeaderDictionary>.None;
            }
        }
    }
}
