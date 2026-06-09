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
        public static MelonLogger.Instance? Instance { get; set; }

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