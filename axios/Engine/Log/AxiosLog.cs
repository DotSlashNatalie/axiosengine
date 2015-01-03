using System;
using System.Collections.Generic;
using Axios.Engine.Data;
using Axios.Engine.File;


namespace Axios.Engine.Log
{
    [Flags]
    public enum LoggingFlag
    {
        NONE = 0,
        DEBUG = 1,
        INFO = 2,
        WARN = 4,
        ERROR = 8,
        FATAL = 16,
        ALL = ~0
    }
    public class AxiosLog : Singleton<AxiosLog>
    {
        private List<string> _log;
        // Logs everything regardless of log level
        // Used for debugging purposes
        private List<string> _extendedlog;

        public AxiosLog()
        {
            _log = new List<string>();
            _extendedlog = new List<string>();
        }

        public void AddLine(string line, LoggingFlag flag)
        {

            if (Settings.Loglevel.HasFlag(flag))
            {
                AxiosCommandConsole c = (AxiosCommandConsole)Cache.Instance.get("commandconsole");
                if (c != null)
                    c.AddToLog(line);
                _log.Add("[" + DateTime.Now.ToString("M/d/yyyy H:mm:ss") + " - " + flag.ToString() + "]" + line);
            }

            _extendedlog.Add("[" + DateTime.Now.ToString("M/d/yyyy H:mm:ss") + " - " + flag.ToString() + "]" + line);
        }

        public List<string> GetLogList()
        {
            return _log;
        }

        public string GetLog(string seperator)
        {
            return String.Join(seperator, _log.ToArray()) + seperator;
        }

        public string GetLog()
        {
            return GetLog("\r\n");
        }

        public void writeLog()
        {
            using (AxiosRegularFile file = new AxiosRegularFile(System.IO.Directory.GetCurrentDirectory() + "/axioslog.log"))
            {
                file.WriteData(GetLog(), System.IO.FileMode.Create);
            }
        }

        public void writeExtendedLog()
        {
            using (AxiosRegularFile file = new AxiosRegularFile(System.IO.Directory.GetCurrentDirectory() + "/axioslog.log"))
            {
                file.WriteData(String.Join("\r\n", _extendedlog.ToArray()), System.IO.FileMode.Create);
            }
        }
    }
}
