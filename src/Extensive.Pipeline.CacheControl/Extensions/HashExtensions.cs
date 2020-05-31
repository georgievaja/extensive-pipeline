using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace Extensive.Pipeline.CacheControl.Extensions
{
    public static class HashExtensions
    {
        /// <summary>
        /// Creates a SHA256 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash</returns>
        public static string Sha256(
            [DisallowNull] this string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            using var sha = SHA256.Create();

            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}
