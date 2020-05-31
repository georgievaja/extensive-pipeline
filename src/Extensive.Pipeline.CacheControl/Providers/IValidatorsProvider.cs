﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Extensive.Pipeline.CacheControl.Providers
{
    public interface IValidatorsProvider
    {
        CacheControlResponse GenerateCacheControlResponse(string content);
    }
}