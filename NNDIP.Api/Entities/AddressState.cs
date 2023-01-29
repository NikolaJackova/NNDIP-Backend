using System;
using System.Collections.Generic;

namespace NNDIP.Api.Entities
{
    public partial class AddressState
    {
        public string Address { get; set; } = null!;
        public string OutputType { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string ActionName { get; set; } = null!;
        public string PlanName { get; set; } = null!;
    }
}
