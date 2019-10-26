using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 1.引入log4net.dll
 * 2.添加log4net.config配置文件
 * 3.在 Properties.AssemblyInfo 中添加：[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
 */

namespace D.Extend
{
    public class Log
    {
        public static void Info(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("InfoLog");
            if (log.IsInfoEnabled) { log.Info(msg); }
            log = null;
        }

        public static void Error(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("Error");
            if (log.IsInfoEnabled) { log.Info(msg); }
            log = null;
        }
    }
}
