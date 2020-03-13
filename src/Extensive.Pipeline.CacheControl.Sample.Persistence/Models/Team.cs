using System;
using System.Collections.Generic;
using System.Text;

namespace Extensive.Pipeline.CacheControl.Sample.Persistence.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Updated { get; set; }
    }
}
