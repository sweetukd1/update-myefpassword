using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateMyEFPassword.Logger
{
    public class ELKLogger
    {
        public enum LogType
        {
            Trace, Debug, Info, Warn, Error, Fatal, Off
        }


        public static void Init(string enviroment, string configPath, string loggername)
        {
            EF.Language.Logging.Log.Init(enviroment, configPath, loggername);
        }
        public static void RegisterDependencies()
        {
            EF.Language.Logging.Log.RegisterDependencies();

        }


        /// <summary>
        /// Log the Errors to ELK as Error. The error message, source and innerexception will be logged.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="attributes"></param>
        public static void Log(Exception ex, String message, Dictionary<string, object> attributes = null)
        {
            LogEventInfo logEvent = new LogEventInfo(NLog.LogLevel.Info, EF.Language.Logging.Log.Logger.Name, message);
            Log(attributes, logEvent);
        }




        /// <summary>
        /// Log the message to ELK as Info. 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="attributes"></param>
        public static void Log(string message, Dictionary<string, object> attributes = null)
        {
            LogEventInfo logEvent = new LogEventInfo(NLog.LogLevel.Info, EF.Language.Logging.Log.Logger.Name, message);
            Log(attributes, logEvent);
        }

        /// <summary>
        /// Log the message to ELK as Info. 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="attributes"></param>
        public static void LogException(string message, Dictionary<string, object> attributes = null)
        {
            LogEventInfo logEvent = new LogEventInfo(NLog.LogLevel.Error, EF.Language.Logging.Log.Logger.Name, message);
            Log(attributes, logEvent);
        }

        /// <summary>
        /// Log the message to ELK as the type being passed
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        /// <param name="attributes"></param>
        public static void Log(LogType logType, string message, Dictionary<string, object> attributes = null)
        {
            LogLevel logLevel;
            switch (logType)
            {
                case LogType.Trace:
                    logLevel = LogLevel.Trace;
                    break;
                case LogType.Debug:
                    logLevel = LogLevel.Debug;
                    break;
                case LogType.Warn:
                    logLevel = LogLevel.Warn;
                    break;
                case LogType.Fatal:
                    logLevel = LogLevel.Fatal;
                    break;
                case LogType.Error:
                    logLevel = LogLevel.Error;
                    break;
                case LogType.Off:
                    logLevel = LogLevel.Off;
                    break;
                default:
                    logLevel = LogLevel.Info;
                    break;
            }
            LogEventInfo logEvent = new LogEventInfo(logLevel, EF.Language.Logging.Log.Logger.Name, string.Format("{0}:{1}", logLevel, message));
            Log(attributes, logEvent);
        }
        private static void Log(Dictionary<string, object> attributes, LogEventInfo logEvent)
        {
            if (attributes != null && attributes.Any())
            {
                foreach (var item in attributes)
                {
                    logEvent.Properties[item.Key] = item.Value;
                }
            }
            EF.Language.Logging.Log.Logger.Log(logEvent);
        }

    }
}
