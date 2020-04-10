using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Extensive.Pipeline.CacheControl.Handlers
{
    public class PrivateCacheControlHandler : ControlHandler
    {
        public override Task<IDictionary<string, StringValues>> ControlCache(
            [NotNull] CacheControlAttribute directive)
        {
            if (directive == null) throw new ArgumentNullException(nameof(directive));

            if (directive is PrivateCacheControlAttribute privateDirective)
            {

            }

            return base.ControlCache(directive);
        }
    }
}
