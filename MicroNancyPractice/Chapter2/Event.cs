using System;

namespace Chapter2
{
    public class Event
    {
        public Event(int id, DateTimeOffset dateTimeOffset, string eventName, object data)
        {
            Id = id;
            DateTimeOffset = dateTimeOffset;
            EventName = eventName;
            Data = data;
        }

        public int Id { get; }
        public DateTimeOffset DateTimeOffset { get; }
        public string EventName { get; }
        public object Data { get; }
    }
}