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