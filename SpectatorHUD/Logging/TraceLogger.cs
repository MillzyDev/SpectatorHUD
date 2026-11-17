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

using System;
using System.Diagnostics;
using System.Reflection;
using MelonLoader;
using SpectatorHUD.Annotations;

namespace SpectatorHUD.Logging;

public sealed class TraceLogger(MelonLogger.Instance backingLogger) : DebugLogger(backingLogger)
{
    [StringFormatMethod("format")]
    public override void Trace(string format, params object[] args)
    {
        StackFrame? callerFrame = new StackTrace().GetFrame(1);
        MethodBase? callerMethod = callerFrame?.GetMethod();
        Type? declaringType = callerMethod?.DeclaringType;
        
        string context = "";
        if (declaringType != null)
        {
            context += declaringType.Name + "::";
        }
        context += callerMethod?.Name;
        
        this.backingLogger.Msg("[TRACE] " + format, args);
        this.backingLogger.Msg("[TRACE] \tin: " + context);
        this.backingLogger.Msg("[TRACE] \tat: " + callerFrame?.GetFileName() + ":" + callerFrame?.GetFileLineNumber());
    }

    public override void Trace(string message)
    {
        StackFrame? callerFrame = new StackTrace().GetFrame(1);
        MethodBase? callerMethod = callerFrame?.GetMethod();
        Type? declaringType = callerMethod?.DeclaringType;
        
        string context = "";
        if (declaringType != null)
        {
            context += declaringType.Name + "::";
        }
        context += callerMethod?.Name;
        
        this.backingLogger.Msg("[TRACE] " + message);
        this.backingLogger.Msg("[TRACE] \tin: " + context);
        this.backingLogger.Msg("[TRACE] \tat: " + callerFrame?.GetFileName() + ":" + callerFrame?.GetFileLineNumber());
    }

    public override void Trace(object obj)
    {
        StackFrame? callerFrame = new StackTrace().GetFrame(1);
        MethodBase? callerMethod = callerFrame?.GetMethod();
        Type? declaringType = callerMethod?.DeclaringType;
        
        string context = "";
        if (declaringType != null)
        {
            context += declaringType.Name + "::";
        }
        context += callerMethod?.Name;
        
        this.backingLogger.Msg("[TRACE] " + obj);
        this.backingLogger.Msg("[TRACE] \tin: " + context);
        this.backingLogger.Msg("[TRACE] \tat: " + callerFrame?.GetFileName() + ":" + callerFrame?.GetFileLineNumber());
    }
}