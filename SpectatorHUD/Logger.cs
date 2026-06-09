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
        private delegate void MessageEvent(object obj);
        private delegate void MessageEventStr(string txt);
        private delegate void MessageEventFormat(string txt, params object[] args);
        private delegate void MessageEventException(string txt, Exception ex);
        
        private static MelonLogger.Instance? _instance;
        private static readonly Queue<Tuple<LogType, Either<string, object>, Either<object[]?, Exception>>> PreQueue = new();
        private static MessageEvent _messageEvent = QueueMessage;
        private static MessageEventStr _messageEventStr = QueueMessage;
        private static MessageEventFormat _messageEventFormat = QueueMessage;
        private static MessageEvent _warningEvent = QueueWarning;
        private static MessageEventStr _warningEventStr = QueueWarning;
        private static MessageEventFormat _warningEventFormat = QueueWarning;
        private static MessageEvent _errorEvent = QueueError;
        private static MessageEventStr _errorEventStr = QueueError;
        private static MessageEventFormat _errorEventFormat = QueueError;
        private static MessageEventException _errorEventException = QueueError;

        public static void SetInstance(MelonLogger.Instance? instance)
        {
            _instance = instance;
            
            if (instance != null)
            {
                _messageEvent = LogMessage;
                _messageEventStr = LogMessage;
                _messageEventFormat = LogMessage;
                _warningEvent = LogWarning;
                _warningEventStr = LogWarning;
                _warningEventFormat = LogWarning;
                _errorEvent = LogError;
                _errorEventStr = LogError;
                _errorEventFormat = LogError;
                _errorEventException = LogError;
                
                FlushQueue();
            }
            else
            {
                _messageEvent = QueueMessage;
                _messageEventStr = QueueMessage;
                _messageEventFormat = QueueMessage;
                _warningEvent = QueueWarning;
                _warningEventStr = QueueWarning;
                _warningEventFormat = QueueWarning;
                _errorEvent = QueueError;
                _errorEventStr = QueueError;
                _errorEventFormat = QueueError;
                _errorEventException = QueueError;
            }
        }

        public static void Msg(object obj) => _messageEvent(obj);
        public static void Msg(string txt) => _messageEventStr(txt);
        public static void Msg(string txt, params object[] args) => _messageEventFormat(txt, args);
        public static void Warning(object obj) => _warningEvent(obj);
        public static void Warning(string txt) => _warningEventStr(txt);
        public static void Warning(string txt, params object[] args) => _warningEventFormat(txt, args);
        public static void Error(object obj) => _errorEvent(obj);
        public static void Error(string txt) => _errorEventStr(txt);
        public static void Error(string txt, params object[] args) => _errorEventFormat(txt, args);
        public static void Error(string txt, Exception ex) => _errorEventException(txt, ex);

        private static void FlushQueue()
        {
            while (PreQueue.Count > 0)
            {
                Tuple<LogType, Either<string, object>, Either<object[]?, Exception>> item = PreQueue.Dequeue();
                switch (item.Item1)
                {
                    case LogType.Msg:
                        switch (item.Item2)
                        {
                            case Either<string, object>.Left(var txt):
                                switch (item.Item3)
                                {
                                    case Either<object[]?, Exception>.Left(var args):
                                        if (args == null)
                                        {
                                            LogMessage(txt);
                                            break;
                                        }
                                        LogMessage(txt, args);
                                        break;
                                    default: throw new InvalidOperationException();
                                }
                                break;
                            case Either<string, object>.Right(var obj):
                                LogMessage(obj);
                                break;
                            default:
                                throw new InvalidOperationException();
                        }
                        break;
                    case LogType.Warning:
                        switch (item.Item2)
                        {
                            case Either<string, object>.Left(var txt):
                                switch (item.Item3)
                                {
                                    case Either<object[]?, Exception>.Left(var args):
                                        if (args == null)
                                        {
                                            LogWarning(txt);
                                            break;
                                        }
                                        LogWarning(txt, args);
                                        break;
                                    default: throw new InvalidOperationException();
                                }
                                break;
                            case Either<string, object>.Right(var obj):
                                LogWarning(obj);
                                break;
                            default:
                                throw new InvalidOperationException();
                        }
                        break;
                    case LogType.Error:
                        switch (item.Item2)
                        {
                            case Either<string, object>.Left(var txt):
                                switch (item.Item3)
                                {
                                    case Either<object[]?, Exception>.Left(var args):
                                        if (args == null)
                                        {
                                            LogError(txt);
                                            break;
                                        }
                                        LogError(txt, args);
                                        break;
                                    case Either<object[]?, Exception>.Right(var ex):
                                        LogError(txt, ex);
                                        break;
                                    default:
                                        throw new InvalidOperationException();
                                }
                                break;
                            case Either<string, object>.Right(var obj):
                                LogWarning(obj);
                                break;
                            default:
                                throw new InvalidOperationException();
                        }
                        break;
                    default: throw new InvalidOperationException();
                }
            }
        }

        private static void QueueMessage(object obj)
        {
            Tuple<LogType, Either<string, object>, Either<object[]?, Exception>> value = new(
                LogType.Msg,
                new Either<string, object>.Right(obj),
                new Either<object[]?, Exception>.Left(null)
            );
            PreQueue.Enqueue(value);
        }
        
        private static void QueueMessage(string txt)
        {
            Tuple<LogType, Either<string, object>, Either<object[]?, Exception>> value = new(
                LogType.Msg,
                new Either<string, object>.Left(txt),
                new Either<object[]?, Exception>.Left(null)
            );
            PreQueue.Enqueue(value);
        }
        
        private static void QueueMessage(string txt, params object[] args)
        {
            Tuple<LogType, Either<string, object>, Either<object[]?, Exception>> value = new(
                LogType.Msg,
                new Either<string, object>.Left(txt),
                new Either<object[]?, Exception>.Left(args)
            );
            PreQueue.Enqueue(value);
        }
        
        private static void QueueWarning(object obj)
        {
            Tuple<LogType, Either<string, object>, Either<object[]?, Exception>> value = new(
                LogType.Warning,
                new Either<string, object>.Right(obj),
                new Either<object[]?, Exception>.Left(null)
            );
            PreQueue.Enqueue(value);
        }
        
        private static void QueueWarning(string txt)
        {
            Tuple<LogType, Either<string, object>, Either<object[]?, Exception>> value = new(
                LogType.Warning,
                new Either<string, object>.Left(txt),
                new Either<object[]?, Exception>.Left(null)
            );
            PreQueue.Enqueue(value);
        }
        
        private static void QueueWarning(string txt, params object[] args)
        {
            Tuple<LogType, Either<string, object>, Either<object[]?, Exception>> value = new(
                LogType.Warning,
                new Either<string, object>.Left(txt),
                new Either<object[]?, Exception>.Left(args)
            );
            PreQueue.Enqueue(value);
        }
        
        private static void QueueError(object obj)
        {
            Tuple<LogType, Either<string, object>, Either<object[]?, Exception>> value = new(
                LogType.Error,
                new Either<string, object>.Right(obj),
                new Either<object[]?, Exception>.Left(null)
            );
            PreQueue.Enqueue(value);
        }
        
        private static void QueueError(string txt)
        {
            Tuple<LogType, Either<string, object>, Either<object[]?, Exception>> value = new(
                LogType.Error,
                new Either<string, object>.Left(txt),
                new Either<object[]?, Exception>.Left(null)
            );
            PreQueue.Enqueue(value);
        }
        
        private static void QueueError(string txt, params object[] args)
        {
            Tuple<LogType, Either<string, object>, Either<object[]?, Exception>> value = new(
                LogType.Error,
                new Either<string, object>.Left(txt),
                new Either<object[]?, Exception>.Left(args)
            );
            PreQueue.Enqueue(value);
        }

        private static void QueueError(string txt, Exception ex)
        {
            Tuple<LogType, Either<string, object>, Either<object[]?, Exception>> value = new(
                LogType.Error,
                new Either<string, object>.Left(txt),
                new Either<object[]?, Exception>.Right(ex)
            );
            PreQueue.Enqueue(value);
        }

        private static void LogMessage(object obj) => _instance?.Msg(obj);
        private static void LogMessage(string txt) => _instance?.Msg(txt);
        private static void LogMessage(string txt, params object[] args) => _instance?.Msg(txt, args);
        private static void LogWarning(object obj) => _instance?.Warning(obj);
        private static void LogWarning(string txt) => _instance?.Warning(txt);
        private static void LogWarning(string txt, params object[] args) => _instance?.Warning(txt, args);
        private static void LogError(object obj) => _instance?.Error(obj);
        private static void LogError(string txt) => _instance?.Error(txt);
        private static void LogError(string txt, params object[] args) => _instance?.Error(txt, args);
        private static void LogError(string txt, Exception ex) => _instance?.Error(txt, ex);

        private enum LogType
        {
            Msg,
            Warning,
            Error
        }
    }
}