using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Extensive.Pipeline.CacheControl.Handlers
{
    public abstract class ControlHandler : IControlHandler
    {
        private IControlHandler nextValidator;

        public IControlHandler SetNext(
            IControlHandler nextValidator)
        {
            this.nextValidator = nextValidator ?? throw new ArgumentNullException(nameof(nextValidator));

            return nextValidator;
        }

        public virtual Task<IDictionary<string, StringValues>> ControlCache(
            CacheControlAttribute attribute)
        {
            if (attribute == null) throw new ArgumentNullException(nameof(attribute));

            return nextValidator.ControlCache(attribute);
        }
    }
}
