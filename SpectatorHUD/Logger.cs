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

namespace SpectatorHUD;

public static class Logger
{
    private static DebugLogObject? _debugLogObject;
    private static DebugLogTxt? _debugLogTxt;
    private static DebugLogFormat? _debugLogFormat;


    public static MelonLogger.Instance? Instance { get; set; }

    public static void EnableDebug(bool debug)
    {
        if (debug)
        {
            _debugLogObject = obj => Instance?.Msg("[DEBUG] " + obj);
            _debugLogTxt = txt => Instance?.Msg("[DEBUG] " + txt);
            _debugLogFormat = (txt, args) => Instance?.Msg("[DEBUG] " + txt, args);
        }
        else
        {
            _debugLogObject = null;
            _debugLogTxt = null;
            _debugLogFormat = null;
        }
    }

    public static void Debug(object obj)
    {
        _debugLogObject?.Invoke(obj);
    }

    public static void Debug(string txt)
    {
        _debugLogTxt?.Invoke(txt);
    }

    public static void Debug(string txt, params object[] args)
    {
        _debugLogFormat?.Invoke(txt, args);
    }

    public static void Msg(object obj)
    {
        Instance?.Msg(obj);
    }

    public static void Msg(string txt)
    {
        Instance?.Msg(txt);
    }

    public static void Msg(string txt, params object[] args)
    {
        Instance?.Msg(txt, args);
    }

    public static void Warning(object obj)
    {
        Instance?.Warning(obj);
    }

    public static void Warning(string txt)
    {
        Instance?.Warning(txt);
    }

    public static void Warning(string txt, params object[] args)
    {
        Instance?.Warning(txt, args);
    }

    public static void Error(object obj)
    {
        Instance?.Error(obj);
    }

    public static void Error(string txt)
    {
        Instance?.Error(txt);
    }

    public static void Error(string txt, params object[] args)
    {
        Instance?.Error(txt, args);
    }

    public static void Error(string txt, Exception ex)
    {
        Instance?.Error(txt, ex);
    }

    private delegate void DebugLogObject(object obj);

    private delegate void DebugLogTxt(string txt);

    private delegate void DebugLogFormat(string txt, params object[] args);
}