using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Axios.Engine;

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
        ALL = 32
    }
    public class AxiosLog : Singleton<AxiosLog>
    {
        private List<string> _log;

        public AxiosLog()
        {
            _log = new List<string>();
        }

        public void AddLine(string line, LoggingFlag flag)
        {
            
            if (flag <= Settings.Loglevel)
                _log.Add("[" + DateTime.Now.ToString("M/d/yyyy H:mm:ss") + " - " + flag.ToString() + "]" + line);
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
    }
}
