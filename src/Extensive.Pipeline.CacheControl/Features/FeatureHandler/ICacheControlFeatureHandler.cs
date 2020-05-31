using Extensive.Pipeline.CacheControl.Attributes;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace Extensive.Pipeline.CacheControl.Features.FeatureHandler
{
    public interface ICacheControlFeatureHandler
    {
        [return: NotNull]
        ICacheControlFeatureHandler SetNext(
            [DisallowNull] ICacheControlFeatureHandler handler);

        Task<ICacheControlFeature> GetCacheControlFeature(
            [DisallowNull] CacheControlAttribute descriptor);
    }
}
