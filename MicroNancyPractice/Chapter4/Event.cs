﻿using System;

namespace Chapter4
{
    public class Event
    {
        public Event(long sequenceNumber, DateTimeOffset occuredAt, string name, object content)
        {
            SequenceNumber = sequenceNumber;
            OccuredAt = occuredAt;
            Name = name;
            Content = content;
        }

        public long SequenceNumber { get; }
        public DateTimeOffset OccuredAt { get; }
        public string Name { get; }
        public object Content { get; }
    }
}