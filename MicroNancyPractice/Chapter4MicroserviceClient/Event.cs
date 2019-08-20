using System;

namespace Chapter4MicroserviceClient
{
    public class Event
    {
        public long SequenceNumber { get; set; }
        public DateTimeOffset OccuredAt { get; set; }
        public string Name { get; set;  }
        public LoyaltyProgramUser Content { get; set;  }
    }
}