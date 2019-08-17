using System;
using System.Collections.Generic;
using System.Linq;

namespace Chapter2
{
    public interface IEventStore
    {
        void Raise(string eventName, object data);
        IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber);
    }

    public class EventStore : IEventStore
    {
        private readonly IList<Event> _events = new List<Event>();
        private int _nextId = 1;

        public void Raise(string eventName, object data)
        {
            _events.Add(new Event(GetNextId(), DateTimeOffset.UtcNow, eventName, data));
        }

        public IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            return _events.Where(x => x.Id >= firstEventSequenceNumber && x.Id <= lastEventSequenceNumber).OrderBy(x => x.Id).ToList();
        }

        private int GetNextId()
        {
            return _nextId++;
        }
    }
}