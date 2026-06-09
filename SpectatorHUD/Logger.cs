/*
 *      SpectatorHUD
 *      Copyright (C) 2026  Millzy
 *
 *      This program is free software: you can redistribute it and/or modify
 *      it under the terms of the GNU Lesser General Public License as published by
 *      the Free Software Foundation, either version 3 of the License, or
 *      (at your option) any later version.
 *
 *      This program is distributed in the hope that it will be useful,
 *      but WITHOUT ANY WARRANTY; without even the implied warranty of
 *      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *      GNU General Public License for more details.
 *
 *      You should have received a copy of the GNU Lesser General Public License
 *      along with this program.  If not, see https://www.gnu.org/licenses/.
 */

using MelonLoader;

namespace SpectatorHUD
{
    public static class Logger
    {
        private delegate void DebugLogObject(object obj);
        private delegate void DebugLogTxt(string txt);
        private delegate void DebugLogFormat(string txt, params object[] args);
        
        
        public static MelonLogger.Instance? Instance { get; set; }

        private static DebugLogObject? _debugLogObject;
        private static DebugLogTxt? _debugLogTxt;
        private static DebugLogFormat? _debugLogFormat;

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

        public static void Debug(object obj) => _debugLogObject?.Invoke(obj);
        public static void Debug(string txt) => _debugLogTxt?.Invoke(txt);
        public static void Debug(string txt, params object[] args) => _debugLogFormat?.Invoke(txt, args); 
        public static void Msg(object obj) => Instance?.Msg(obj);
        public static void Msg(string txt) => Instance?.Msg(txt);
        public static void Msg(string txt, params object[] args) => Instance?.Msg(txt, args);
        public static void Warning(object obj) => Instance?.Warning(obj);
        public static void Warning(string txt) => Instance?.Warning(txt);
        public static void Warning(string txt, params object[] args) => Instance?.Warning(txt, args);
        public static void Error(object obj) => Instance?.Error(obj);
        public static void Error(string txt) => Instance?.Error(txt);
        public static void Error(string txt, params object[] args) => Instance?.Error(txt, args);
        public static void Error(string txt, Exception ex) => Instance?.Error(txt, ex);
    }
}