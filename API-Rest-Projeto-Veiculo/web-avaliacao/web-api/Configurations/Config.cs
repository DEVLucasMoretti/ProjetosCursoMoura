using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace web_api.Configurations
{
    public class Config
    {
        public Config() { }

        public static string GetConnection()
        {
            return GetConnection("Loja");
        }

        public static string GetConnection(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static string GetLogPath()
        {
            return GetLogPath("LogPath");
        }

        public static string GetLogPath(string chave)
        {
            string logPath = ConfigurationManager.AppSettings[chave];
            logPath = Path.Combine(logPath, $"{DateTime.Now.ToString("yyyy-MM-dd")}.txt");
            return logPath;

        }
    }
}