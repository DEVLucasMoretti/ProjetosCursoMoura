using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace api_notebook.Configurations
{
    public class Config
    {
        public Config() { }

        public static string GetConnection(string name)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static string GetConnection()
        {
            return GetConnection("notebook");
        }

        public static string GetLogPath(string key)
        {
             string logPath = System.Configuration.ConfigurationManager.AppSettings[key].ToString();
             logPath = Path.Combine(logPath, $"log({DateTime.Now.ToString("yyyy-MM-dd")}).txt");
            return logPath;
        }

        public static  string GetLogPath()
        {
            return GetLogPath("logPath");
        }

        public static int GetCacheExpiration(string key)
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[key].ToString());
        }
    }
}