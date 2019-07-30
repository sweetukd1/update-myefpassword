using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UpdateMyEFPassword.Logger
{
    public class ApplicationLogger
    {
        public static String helpInfo = "helpInfo";
        public static String methodName = "methodName";
        /// <summary>
        /// Logging to WorkerLog File
        /// </summary>
        /// <param name="loginfo"></param>
        public static void LogActionTrackerInfo(string loginfo)

        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            ELKLogger.Log(loginfo, obj);
        }

        /// <summary>
        /// Log the Errors to ELK as Error. The error message, source and innerexception will be logged.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="attributes"></param>
        public static void LogException(Exception ex, Dictionary<string, object> attributes = null)
        {
            if (attributes != null)
            {
                attributes.Add("stackTrace", ex.StackTrace);
                attributes.Add("innerException", ex.InnerException);
            }
            ELKLogger.LogException(ex.Message, attributes);
        }

        /// <summary>
        /// Gets the method name
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetMyMethodName()
        {
            var st = new StackTrace(new StackFrame(1));
            return st.GetFrame(0).GetMethod().Name;
        }


        /// <summary>
        /// Gets Environment Variable
        /// </summary>
        /// <returns></returns>
        public static string GetEnvironmentVariable()
        {
            string environmentVariable = System.Environment.GetEnvironmentVariable("ENVIRONMENT", EnvironmentVariableTarget.Machine);
            if (string.IsNullOrEmpty(environmentVariable))
            {
                environmentVariable = "localhost";
            }
            return environmentVariable;
        }
    }
}
