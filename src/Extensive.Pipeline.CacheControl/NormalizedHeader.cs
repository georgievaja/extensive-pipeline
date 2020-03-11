using System;
using System.Collections.Generic;
using System.Text;

namespace Extensive.Pipeline.CacheControl
{
    public struct NormalizedHeader
    {
        public NormalizedHeader(string header)
        {
            Header = header.ToUpperInvariant();
        }

        public string Header { get; }
    }

    public static class NormalizedHeaderExtensions
    {
        public static NormalizedHeader Normalize(this string header)
        {
            return new NormalizedHeader(header);
        }
    } 
}
