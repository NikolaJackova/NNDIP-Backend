using System;
using System.Collections.Generic;

namespace NNDIP.Api.Entities
{
    public partial class Action
    {
        public long Id { get; set; }
        public string Address { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string OutputType { get; set; } = null!;
        public string Value { get; set; } = null!;
        public ulong IsDefault { get; set; }
    }
}
