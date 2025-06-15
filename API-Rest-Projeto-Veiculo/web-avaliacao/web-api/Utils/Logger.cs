using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace web_api.Utils
{
    public class Logger
    {
        private readonly string logPath;
        public Logger() { }

        public Logger(string logPath)
        {
            this.logPath = logPath;
        }

        public async Task Log(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Data: ");
            sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.Append("\nErro: ");
            sb.Append(ex.Message);
            sb.Append("\nStackTrace: ");
            sb.Append(ex.StackTrace);
            sb.Append("\n________________________________________________________________________________________________________________________________________________________________________________________\n");

            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
                await sw.WriteLineAsync(sb.ToString());
            }
        }

    }
}