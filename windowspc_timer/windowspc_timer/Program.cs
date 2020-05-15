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

        /// <summary>
        /// Whether I can stay up a little later tonight.
        /// </summary>
        /// <returns></returns>
        private static bool TodayIsWeekend()
        {
            var today = DateTime.Now.DayOfWeek;

            return today == DayOfWeek.Friday || today == DayOfWeek.Saturday;
        }

        private static void RunShutDownSequence()
        {
            // Ensure shutdown if no input or incorrect input is given for 5 minutes
            var tokenSource = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;

            var task = Task.Run(() =>
            {
                // Elapses in 5 minutes
                var timer = new System.Timers.Timer(300_000) { Enabled = true };
                timer.Elapsed += TimedEventShutDown;

            }, tokenSource.Token);

            // Give the user an opportunity to abort shut down
            var input = GetInput();
            while (input != ConsoleKey.N && input != ConsoleKey.Y)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid input.");
                input = GetInput();
            }

            if (input == ConsoleKey.N)
                ShutDown();

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

        private static ConsoleKey GetInput()
        {
            Console.WriteLine("It's past curfew, do you need a time extension? (y/n)");
            var input = Console.ReadKey();
            Console.WriteLine();

            return input.Key;
        }
    }
}
