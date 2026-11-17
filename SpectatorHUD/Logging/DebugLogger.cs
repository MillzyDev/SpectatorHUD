using MelonLoader;

namespace SpectatorHUD.Logging;

public class DebugLogger(MelonLogger.Instance backingLogger) : StandardLogger(backingLogger)
{
    public override void Debug(string format, params object[] args)
    {
        this.backingLogger.Msg("[DEBUG] " + format, args);
    }
    
    public override void Debug(string message)
    {
        this.backingLogger.Msg("[DEBUG] " + message);
    }

    public override void Debug(object obj)
    {
        this.backingLogger.Msg("[DEBUG] " + obj);
    }
}