using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Extensive.Pipeline.CacheControl.Handlers
{
    public interface IControlHandler
    {
        [NotNull]
        IControlHandler SetNext(
            [NotNull] IControlHandler handler);

        [Pure]
        Task<IDictionary<string, StringValues>> ControlCache(
            [NotNull] CacheControlAttribute directive);
    }
}
