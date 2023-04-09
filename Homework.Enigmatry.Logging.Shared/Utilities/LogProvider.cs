﻿using Microsoft.Extensions.Logging;

namespace Homework.Enigmatry.Logging.Shared.Utilities
{
    public static class LogProvider
    {
        private static readonly Action<ILogger,string, Exception> _informationLoggerWithMessage = LoggerMessage.Define<string>(
            LogLevel.Information,
            Events.Started,
            "Information details: {Param1}");

        private static readonly Action<ILogger, string, Exception> _errorLoggerWithMessage = LoggerMessage.Define<string>(
            LogLevel.Error,
            Events.Started,
            "Error details: {Param1}");

        private static readonly Action<ILogger, string, Exception> _warningLoggerWithMessage = LoggerMessage.Define<string>(
            LogLevel.Warning,
            Events.Started,
            "Warning details: {Param1}");  
        private static readonly Action<ILogger, string, Exception> _traceLoggerWithMessage = LoggerMessage.Define<string>(
            LogLevel.Trace,
            Events.Started,
            "Trace details: {Param1}"); 
        private static readonly Action<ILogger, string, Exception> _debugLoggerWithMessage = LoggerMessage.Define<string>(
            LogLevel.Debug,
            Events.Started,
            "Debug details: {Param1}");


        
        public static void InformationalMessage(ILogger logger, string message,Exception innerException)
        {
            if(logger.IsEnabled(LogLevel.Information))
                _informationLoggerWithMessage(logger, message, innerException);
        } 

        public static void ErrorMessage(ILogger logger, string message, Exception innerException)
        {
            if (logger.IsEnabled(LogLevel.Error))
                _errorLoggerWithMessage(logger, message, innerException);
        }

        public static void WarningMessage(ILogger logger, string message, Exception innerException)
        {
            if (logger.IsEnabled(LogLevel.Warning))
                _warningLoggerWithMessage(logger, message, innerException);
        }

        public static void TraceMessage(ILogger logger, string message, Exception innerException)
        {
            if (logger.IsEnabled(LogLevel.Trace))
                _traceLoggerWithMessage(logger, message, innerException);
        }
        public static void DebugMessage(ILogger logger, string message, Exception innerException)
        {
            if (logger.IsEnabled(LogLevel.Trace))
                _debugLoggerWithMessage(logger, message, innerException);
        }


        public static void InformationalMessage(ILogger logger, string message)
        {
            if (logger.IsEnabled(LogLevel.Information))
                _informationLoggerWithMessage(logger, message, null);
        }

        public static void ErrorMessage(ILogger logger, string message)
        {
            if (logger.IsEnabled(LogLevel.Error))
                _errorLoggerWithMessage(logger, message, null);
        }

        public static void WarningMessage(ILogger logger, string message)
        {
            if (logger.IsEnabled(LogLevel.Warning))
                _warningLoggerWithMessage(logger, message, null);
        }

        public static void TraceMessage(ILogger logger, string message)
        {
            if (logger.IsEnabled(LogLevel.Trace))
                _traceLoggerWithMessage(logger, message, null);
        }
        public static void DebugMessage(ILogger logger, string message)
        {
            if (logger.IsEnabled(LogLevel.Trace))
                _debugLoggerWithMessage(logger, message, null);
        }

    }
    internal static class Events
    {
        public static readonly EventId Started = new EventId(100, "Started");
    }

}
