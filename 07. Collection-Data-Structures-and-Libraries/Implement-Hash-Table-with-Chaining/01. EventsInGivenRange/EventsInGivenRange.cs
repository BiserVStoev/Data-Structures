namespace _01.EventsInGivenRange
{
    using System;
    using System.Globalization;
    using System.Threading;
    using Wintellect.PowerCollections;

    public static class EventsInGivenDateRange
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var events = new OrderedMultiDictionary<DateTime, string>(true);
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string eventEntry = Console.ReadLine();
                var eventTokens = eventEntry.Split('|');
                string eventName = eventTokens[0].Trim();
                DateTime eventDate = DateTime.Parse(eventTokens[1].Trim());
                events.Add(eventDate, eventName);
            }

            int numberOfRangesToProcess = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfRangesToProcess; i++)
            {
                string[] rangeInfo = Console.ReadLine().Split('|');
                DateTime startDate = DateTime.Parse(rangeInfo[0].Trim());
                DateTime endDate = DateTime.Parse(rangeInfo[1].Trim());

                var eventsInRange = events.Range(startDate, true, endDate, true);

                Console.WriteLine(eventsInRange.KeyValuePairs.Count);

                foreach (var @event in eventsInRange)
                {
                    foreach (var course in @event.Value)
                    {
                        Console.WriteLine("{0} | {1:dd-MMM-yyyy}", course, @event.Key);
                    }
                }
            }
        }
    }
}
