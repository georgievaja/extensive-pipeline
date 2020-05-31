using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Extensive.Pipeline.CacheControl.Pure.Functors;
using Microsoft.AspNetCore.Http;

namespace Extensive.Pipeline.CacheControl.Validators
{
    internal interface IValidator
    {
        [return: NotNull]
        IValidator SetNext(
            [DisallowNull] IValidator handler);

        [return: NotNull]
        Maybe<IHeaderDictionary> TryValidate(
            [DisallowNull] IHeaderDictionary headers,
            [DisallowNull] CacheControlResponse response);
    }
}
