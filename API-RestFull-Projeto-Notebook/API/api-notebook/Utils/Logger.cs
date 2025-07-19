using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Logger
    {
        private readonly string LogPath;
        public Logger(string logPath)
        {
            this.LogPath = logPath;
        }
              
        public  async Task Log(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Data: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.\n");
            sb.Append($"Erro: {ex.Message}.\n");
            sb.Append($"Stacktrace: {ex.StackTrace}\n");
            sb.Append("________________________________________________________________________________________________________________________________________________________________________________________________________");

            using(StreamWriter sw = new StreamWriter(LogPath, true))
            {
                await sw.WriteAsync(sb.ToString());
            }
        }
    }
}
