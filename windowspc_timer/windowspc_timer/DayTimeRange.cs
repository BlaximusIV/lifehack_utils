using NodaTime;
using System;

namespace windowspc_timer
{
    public class DayTimeRange
    {
        public LocalTime StartTime { get; }
        public LocalTime EndTime { get; }

        public DayTimeRange(LocalTime startTime, LocalTime endTime)
        {
            if (endTime <= startTime)
                throw new ArgumentException("Ending time cannot equal or be before starting time.");

            StartTime = startTime;
            EndTime = endTime;
        }

        public bool IsInRange(LocalTime time) =>
            time < EndTime && time > StartTime;
    }
}
