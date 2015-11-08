using System;
using System.Diagnostics.Eventing.Reader;
using MongoDBPrototype.Properties;

namespace MongoDBPrototype
{
    public class EventViewerReader
    {
        public void ReadEvents(string query = "*[System/EventID=903]")
        {
            var eventsQuery = new EventLogQuery("Application", PathType.LogName, query);
            try
            {
                var logReader = new EventLogReader(eventsQuery);
                // This is a crazy fucking for loop.  Took me a while to realize what was going on.
                for (EventRecord eventdetail = logReader.ReadEvent();
                    eventdetail != null;
                    eventdetail = logReader.ReadEvent())
                {
                    // Read Event details
                    eventdetail.ToXml();
                }
            }
            catch (EventLogNotFoundException e)
            {
                Console.WriteLine(Resources.EventViewerReader_Thing_Error_while_reading_the_event_logs);
            }
        }
    }
}