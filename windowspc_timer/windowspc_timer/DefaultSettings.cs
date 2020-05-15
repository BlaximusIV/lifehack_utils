using NodaTime;

namespace windowspc_timer
{
    public static class DefaultSettings
    {
        public static DayTimeRange WeekdayRangeRuleSet = new DayTimeRange(new LocalTime(4, 0), new LocalTime(20, 0));
        public static DayTimeRange WeekendRangeRuleSet = new DayTimeRange(new LocalTime(4, 0), new LocalTime(22, 0));
    }
}
