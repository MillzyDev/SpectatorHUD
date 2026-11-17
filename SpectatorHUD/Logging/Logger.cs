using MelonLoader;
using SpectatorHUD.Annotations;

namespace SpectatorHUD.Logging;

public abstract class Logger(MelonLogger.Instance backingLogger)
{
    protected readonly MelonLogger.Instance backingLogger = backingLogger;

    [StringFormatMethod("format")]
    public abstract void Trace(string format, params object[] args);
    public abstract void Trace(string message);
    public abstract void Trace(object obj);
    
    [StringFormatMethod("format")]
    public abstract void Debug(string format, params object[] args);
    public abstract void Debug(string message);
    public abstract void Debug(object obj);
    
    [StringFormatMethod("format")]
    public abstract void Info(string format, params object[] args);
    public abstract void Info(string message);
    public abstract void Info(object obj);
    
    [StringFormatMethod("format")]
    public abstract void Warn(string format, params object[] args);
    public abstract void Warn(string message);
    public abstract void Warn(object obj);
    
    [StringFormatMethod("format")]
    public abstract void Error(string format, params object[] args);
    public abstract void Error(string message);
    public abstract void Error(object obj);
}