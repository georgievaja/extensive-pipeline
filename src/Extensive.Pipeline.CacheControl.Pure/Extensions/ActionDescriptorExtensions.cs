using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extensive.Pipeline.CacheControl.Pure.Functors;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace Extensive.Pipeline.CacheControl.Pure.Extensions
{
    public static class ActionDescriptorExtensions
    {
        public static Maybe<TAttribute> TryGetAttribute<TAttribute>(
            [DisallowNull] this ActionDescriptor descriptor)
            where TAttribute : Attribute
        {
            if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));

            var item = descriptor.FilterDescriptors
                .Select(x => x.Filter).OfType<TAttribute>().SingleOrDefault();

            return item == null ? Maybe<TAttribute>.None : Maybe<TAttribute>.Some(item);
        }
    }
}
