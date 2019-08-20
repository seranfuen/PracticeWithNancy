using System.Collections.Generic;

namespace Chapter4
{
    public interface IEventStore
    {
        IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber);
        void PutEvent(string eventName, object content);
    }
}