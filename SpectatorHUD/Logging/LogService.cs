using System;
using MelonLoader;
using MelonLoader.Logging;

namespace SpectatorHUD.Logging;

public static class LogService
{
    private static readonly Lazy<Logger> LazyLogger = new Lazy<Logger>(GetLogger);
    
    public static Logger Logger => LazyLogger.Value;

    private static Logger GetLogger()
    {
        var backingLogger = new MelonLogger.Instance($"${ModInfo.Author}:{ModInfo.Name}", ColorARGB.BlueViolet);
        
#if DEBUG
        return new TraceLogger(backingLogger);
#else
        if (Config.Instance.DebugLogging)
        {
            return new DebugLogger(backingLogger);
        }
        
        return new StandardLogger(backingLogger);
#endif
    }
}