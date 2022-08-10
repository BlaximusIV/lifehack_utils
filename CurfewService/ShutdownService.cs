using System.Diagnostics;

public class ShutdownService 
{
    public void EvaluateShutdown() 
    {
        var now = DateTime.Now;

        // Should be configured in the appsettings. I'm just working this PoC for now.
        bool IsAfterCurfew = (now.Hour >= 21 && now.Minute >= 30) || now.Hour >= 22;
        bool IsBeforeWake = now.Hour < 5;

        if (IsAfterCurfew || IsBeforeWake)
        {
            Process.Start(new ProcessStartInfo("shutdown", "/s /t 0") 
            {
                CreateNoWindow = true,
                UseShellExecute = false
            });
        }
    }
}