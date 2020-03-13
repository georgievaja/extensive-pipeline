using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Extensive.Pipeline.CacheControl.Sample.Resources
{
    public class TeamRequestV1
    {
        /// <summary>
        /// Team name
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
