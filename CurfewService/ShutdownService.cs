using System.Diagnostics;

// TODO: Retrieve curfew settings from appsettings file
// TODO: Allow to indicate active/inactive days as well as curfew times

public class ShutdownService 
{
    public void EvaluateShutdown() 
    {
        if (IsRestrictedCurfewTime())
        {
            Process.Start(new ProcessStartInfo("shutdown", "/s /t 0") 
            {
                CreateNoWindow = true,
                UseShellExecute = false
            });
        }
    }

    private bool IsRestrictedCurfewTime() {
        var now = DateTime.Now;
        
        if (now.DayOfWeek == DayOfWeek.Thursday || now.DayOfWeek == DayOfWeek.Friday) {
            return false;
        }

        bool IsAfterCurfew = (now.Hour >= 21 && now.Minute >= 30) || now.Hour >= 22;
        bool IsBeforeWake = now.Hour < 5;

        return IsAfterCurfew || IsBeforeWake;
    }
}