using System;
using System.Collections.Generic;
using System.Linq;

namespace Chapter4
{
    public class EventStore : IEventStore
    {
        private readonly IList<Event> _events = new List<Event>();
        private long _nextSequenceNumber = 1;

        public IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            return _events.Where(x =>
                x.SequenceNumber >= firstEventSequenceNumber && x.SequenceNumber <= lastEventSequenceNumber).ToList();
        }

        public void PutEvent(string eventName, object content)
        {
            var newEvent = new Event(_nextSequenceNumber++, DateTimeOffset.Now, eventName, content);
            _events.Add(newEvent);
        }
    }
}