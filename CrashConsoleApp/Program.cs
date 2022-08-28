// <copyright file="Program.cs" company="OpenTelemetry Authors">
using System.Diagnostics;
using OpenTelemetry.Trace;

namespace Examples.Console
{
    /// <summary>
    /// Main samples entry point.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine($"DS ver: {typeof(ActivityEvent).Assembly.GetName().Version}");

            var @event = new ActivityEvent(
                    "some_activity_event",
                    DateTimeOffset.UtcNow,
                    new ActivityTagsCollection(new KeyValuePair<string, object?>[]
                    {
                        new ("some_activity_event_tag_0", "some_activity_event_tag_value_0"),
                        new ("some_activity_event_tag_1", "some_activity_event_tag_value_1"),
                        new ("some_activity_event_tag_2", "some_activity_event_tag_value_2")
                    }));

            EventEnumerationState state = default;

            @event.EnumerateTags(ref state);
        }

        private struct EventEnumerationState : IActivityEnumerator<KeyValuePair<string, object>>
        {
            public bool ForEach(KeyValuePair<string, object> tag)
            {
                System.Console.WriteLine($"{{Key: {tag.Key}, Value: {tag.Value}}}");
                return true;
            }
        }
    }
}
