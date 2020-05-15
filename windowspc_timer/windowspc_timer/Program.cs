using NodaTime;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace windowspc_timer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
                throw new ArgumentException("Custom parameters not currently supported");

            var rangeRuleSet = TodayIsWeekend() ? DefaultSettings.WeekendRangeRuleSet : DefaultSettings.WeekdayRangeRuleSet;
            var now = new LocalTime(DateTime.Now.Hour, DateTime.Now.Minute);

            var isValidTime = rangeRuleSet.IsInRange(now);

            if (!isValidTime)
                RunShutDownSequence();
        } 

        private static bool TodayIsWeekend()
        {
            var today = DateTime.Now.DayOfWeek;

            return today == DayOfWeek.Friday || today == DayOfWeek.Saturday;
        }

        private static void RunShutDownSequence()
        {
            // TODO: Research Toast Notifications
            var tokenSource = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;

            var task = Task.Run(() =>
            {
                // Elapses in 5 minutes
                var timer = new System.Timers.Timer(300_000) { Enabled = true };
                timer.Elapsed += TimedEventShutDown;

            }, tokenSource.Token);

            void prompt() =>
                Console.WriteLine($"It's past curfew, do you need a time extension? (y/n)");

            prompt();

            var input = Console.ReadKey();
            if (input.Key == ConsoleKey.N)
                ShutDown();
            else if (input.Key != ConsoleKey.Y)
            {
                Console.WriteLine("Invalid input.");
                prompt();
            }

            tokenSource.Cancel();
            task.Dispose();
        }

        private static void TimedEventShutDown(Object source, ElapsedEventArgs e) => ShutDown();

        private static void ShutDown()
        {
            var processInfo = new ProcessStartInfo("shutdown", "/s /t 0")
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };

            Process.Start(processInfo);
        }
    }
}
