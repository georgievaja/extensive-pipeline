using System.Collections.Generic;
using Extensive.Pipeline.CacheControl.Pure.Functors;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace Extensive.Pipeline.CacheControl.Validators
{
    internal interface IValidator
    {
        [NotNull]
        IValidator SetNext(
            [NotNull] IValidator handler);

        [Pure]
        Maybe<IHeaderDictionary> TryValidate(
            [NotNull] IHeaderDictionary headers,
            [NotNull] CacheControlResponse response);
    }
}
