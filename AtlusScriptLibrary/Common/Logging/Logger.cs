﻿using System;
using System.Threading.Tasks;

namespace AtlusScriptLibrary.Common.Logging
{
    public class Logger
    {
        public event LogEventHandler LogEvent;

        public string Name { get; }

        public Logger( string name )
        {
            Name = name;
        }

        public void Log( LogLevel level, string message )
        {
            LogEvent?.Invoke( this, new LogEventArgs( Name, level, message ) );
        }

        public void Debug( string message )
        {
            Log( LogLevel.Debug, message );
        }

        public void Trace( string message )
        {
            Log( LogLevel.Trace, message );
        }

        public void Info( string message )
        {
            Log( LogLevel.Info, message );
        }

        public void Warning( string message )
        {
            Log( LogLevel.Warning, message );
        }

        public void Error( string message )
        {
            Log( LogLevel.Error, message );
        }

        public void Fatal( string message )
        {
            Log( LogLevel.Fatal, message );
        }


        public void ConsoleLog(LogLevel level, string message)
        {
            LogEvent?.Invoke(this, new LogEventArgs(Name, level, message));
            //Console.WriteLine("["+level+"]"+ ": " +message);
        }

        public void ConsoleDebug(string message)
        {
            ConsoleLog(LogLevel.Debug, message);
        }

        public void ConsoleTrace(string message)
        {
            ConsoleLog(LogLevel.Trace, message);
        }

        public void ConsoleInfo(string message)
        {
            ConsoleLog(LogLevel.Info, message);
        }

        public void ConsoleWarning(string message)
        {
            ConsoleLog(LogLevel.Warning, message);
        }

        public void ConsoleError(string message)
        {
            ConsoleLog(LogLevel.Error, message);
        }

        public void ConsoleFatal(string message)
        {
            ConsoleLog(LogLevel.Fatal, message);
        }


    }



    public delegate void LogEventHandler( object sender, LogEventArgs e );

    [Flags]
    public enum LogLevel
    {
        Debug    = 1 << 1,
        Trace    = 1 << 2,
        Info     = 1 << 3,
        Warning  = 1 << 4,
        Error    = 1 << 5,
        Fatal    = 1 << 6,
        All      = Debug | Trace | Info | Warning | Error | Fatal
    }
}
