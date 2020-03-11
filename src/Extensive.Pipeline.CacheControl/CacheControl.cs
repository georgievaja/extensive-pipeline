using System;
using System.Collections.Generic;
using System.Text;

namespace Extensive.Pipeline.CacheControl
{
    public class CacheControl
    {
        public NormalizedHeader[] VaryHeaders { get; }
        public NormalizedHeader[] VaryQueryStrings { get; }

        public CacheControl(NormalizedHeader[] varyHeaders, NormalizedHeader[] varyQueryStrings)
        {
            this.VaryHeaders = varyHeaders;
            this.VaryQueryStrings = varyQueryStrings;
        }
   }
}
