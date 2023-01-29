﻿using NNDIP.Api.Dtos.Event;
using NNDIP.Api.Entities;

namespace NNDIP.Api.Dtos.Plan
{
    public class SimplePlanDto
    {
        public long Id { get; set; }
        public ulong Enabled { get; set; }
        public string Name { get; set; } = null!;
        public long EventId { get; set; }
        public int? Priority { get; set; }
        public string PlanType { get; set; } = null!;

        public virtual EventDto Event { get; set; } = null!;
    }
}
