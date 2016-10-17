namespace EventsInGivenDataRange
{
    using System;
    using System.Globalization;
    using System.Threading;

    using Wintellect.PowerCollections;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            FindEventsInGivenRange();
        }

        private static void FindEventsInGivenRange()
        {
            var events = RegisterEvents();
            int eventRangesCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < eventRangesCount; i++)
            {
                string[] dateRange = Console.ReadLine().Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                DateTime startDate = DateTime.Parse(dateRange[0]);
                DateTime endDate = DateTime.Parse(dateRange[1]);
                var eventsInRange = GetEventsInRange(events, startDate, endDate);
                PrintEvents(eventsInRange);
            }  
        }

        private static void PrintEvents(OrderedMultiDictionary<DateTime, string>.View eventsInRange)
        {
            Console.WriteLine(eventsInRange.KeyValuePairs.Count);
            foreach (var eventList in eventsInRange)
            {
                foreach (var singleEvent in eventList.Value)
                {
                    Console.WriteLine($"{singleEvent} | + {eventList.Key:dd-MMM-yyyy hh:mm}");
                }               
            }
        }

        private static OrderedMultiDictionary<DateTime, string>.View GetEventsInRange(OrderedMultiDictionary<DateTime, string> events, DateTime startDate, DateTime endDate)
        {
            var eventsInRange = events.Range(startDate, true, endDate, true);

            return eventsInRange;
        }

        private static OrderedMultiDictionary<DateTime, string> RegisterEvents()
        {
            var events = new OrderedMultiDictionary<DateTime, string>(true);
            var eventsCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < eventsCount; i++)
            {
                string[] input = Console.ReadLine().Split(new [] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                string courseEvent = input[0];
                DateTime eventDate = DateTime.Parse(input[1]);
                events.Add(eventDate, courseEvent);
            }

            return events;
        }


    }
}
