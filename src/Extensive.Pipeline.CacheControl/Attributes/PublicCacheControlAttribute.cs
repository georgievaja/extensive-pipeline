﻿using System;
using Extensive.Pipeline.CacheControl.Pure.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Extensive.Pipeline.CacheControl.Attributes
{
    /// <inheritdoc />
    public class PublicCacheControlAttribute : CacheControlAttribute
    {
        /// <summary>
        /// Cache control directive
        /// </summary>
        public RevalidationType CacheabilityType { get; set; }

        /// <summary>
        /// Additional resource vary headers
        /// </summary>
        public string[] AdditionalVaryHeaders { get; set; } = new string[0];

        /// <summary>
        /// Maximum amount of time a resource will be considered fresh.
        /// Relative to the time of the request (in seconds).
        /// </summary>
        public int MaxAge { get; set; }
    }
}
