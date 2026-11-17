/*
 *      SpectatorHUD
 *      Copyright (C) 2026  Millzy
 *
 *      This software is licensed under the terms of the Functional Source License,
 *      Version 1.1, ALv2 Future License.  You are permitted to use, copy, modify,
 *      create derivative works, publicly display, and redistribute it under the the
 *      terms of the license.
 *
 *      THE SOFTWARE IS PROVIDED "AS IS" AND WITHOUT WARRANTIES OF ANY KIND, EXPRESS OR
 *      IMPLIED, INCLUDING WITHOUT LIMITATION WARRANTIES OF FITNESS FOR A PARTICULAR
 *      PURPOSE, MERCHANTABILITY, TITLE OR NON-INFRINGEMENT.
 *
 *      You should have received a copy of the Functional Source License, Version 1.1,
 *      ALv2 Future License along with this program.  If not, see https://fsl.software/.
 */

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