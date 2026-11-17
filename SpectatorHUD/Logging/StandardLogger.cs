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

namespace SpectatorHUD.Logging;

public class StandardLogger(MelonLogger.Instance backingLogger) : Logger(backingLogger)
{
    public override void Trace(string format, params object[] args) { }
    public override void Trace(string message) { }
    public override void Trace(object obj) { }

    public override void Debug(string format, params object[] args) { }
    public override void Debug(string message) { }
    public override void Debug(object obj) {}

    public override void Info(string format, params object[] args)
    {
        this.backingLogger.Msg(format, args);
    }

    public override void Info(string message)
    {
        this.backingLogger.Msg(message);
    }

    public override void Info(object obj)
    {
        this.backingLogger.Msg(obj);
    }

    public override void Warn(string format, params object[] args)
    {
        this.backingLogger.Warning(format, args);
    }

    public override void Warn(string message)
    {
        this.backingLogger.Warning(message);
    }

    public override void Warn(object obj)
    {
        this.backingLogger.Warning(obj);
    }

    public override void Error(string format, params object[] args)
    {
        this.backingLogger.Error(format, args);
    }

    public override void Error(string message)
    {
        this.backingLogger.Error(message);
    }

    public override void Error(object obj)
    {
        this.backingLogger.Error(obj);
    }
}