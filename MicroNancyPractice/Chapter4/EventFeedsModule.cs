using Nancy;

namespace Chapter4
{
    public class EventFeedsModule : NancyModule
    {
        public EventFeedsModule(IEventStore eventStore) : base("/events")
        {
            Get("/", _ =>
            {
                if (!long.TryParse(Request.Query.start.Value, out long firstEventSequenceNumber))
                    firstEventSequenceNumber = 0;

                if (!long.TryParse(Request.Query.end.Value, out long lastEventSequenceNumber))
                    firstEventSequenceNumber = 0;

                return eventStore.GetEvents(firstEventSequenceNumber, lastEventSequenceNumber);
            });
        }
    }
}