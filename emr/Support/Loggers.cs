namespace emr.Support
{

    public class Loggers : IDisposable
    {
        private IConfiguration _iconfiguration;
        public Loggers(IConfiguration configuration)
        {
            _iconfiguration = configuration;
        }


        public static void SendLogToText(LoggerModel log)
        {
            var line = Environment.NewLine + Environment.NewLine;

            try
            {
                string filepath = log.FolderPath;

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {

                    File.Create(filepath).Dispose();

                }

                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error = "Error Message :" + " " + log.ErrorMessage;
                    sw.WriteLine("-----------Log Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                e.ToString();

            }

        }
        public void WriteLog(string message)
        {
            LoggerModel logger = new LoggerModel();
            if (logger != null)
            {
                logger.Date = DateTime.Now.ToString("HH:mm:ss tt");
                logger.ErrorMessage = message;
                logger.FolderPath = _iconfiguration["ErrorLog"];
                SendLogToText(logger);
            }
        }
        public void Dispose()
        {

        }

    }
}