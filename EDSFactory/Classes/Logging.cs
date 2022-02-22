using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDSFactory
{
    class Logging
    {
        public static void WriteLog(string time, string message, string stackTrace, string targetSite, string comment)
        {
            try
            {
                if (!Directory.Exists("Logs"))
                    Directory.CreateDirectory("Logs");

                if (!Directory.Exists("Logs\\"  + DateTime.Now.Year.ToString()))
                    Directory.CreateDirectory("Logs\\" + DateTime.Now.Year.ToString());

                if (!Directory.Exists("Logs\\"  + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString()))
                    Directory.CreateDirectory("Logs\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString());

                if (!Directory.Exists("Logs\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString()))
                    Directory.CreateDirectory("Logs\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString());

                string path = "Logs\\" +"\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" +
                    DateTime.Now.Date.ToShortDateString() + ".txt";

                StreamWriter sw;

                if (!File.Exists(path))
                    sw = new StreamWriter(path);
                else
                    sw = File.AppendText(path);

                sw.WriteLine("-------------------------------------");
                sw.WriteLine("Hata Zamanı : ");
                sw.WriteLine(time);
                sw.WriteLine();
                sw.WriteLine("Hata Mesajı :");
                sw.WriteLine(message);
                sw.WriteLine();
                sw.WriteLine("Hata Oluşan Kod Parçacığı :");
                sw.WriteLine(stackTrace);
                sw.WriteLine();
                sw.WriteLine("Hata Oluşan Metot :");
                sw.WriteLine(targetSite);
                sw.WriteLine();
                sw.WriteLine("Yorum :");
                sw.WriteLine(comment);
                sw.WriteLine();
                sw.WriteLine("-------------------------------------");

                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.LoggingExceptionMessage, ex);
            }
        }

        public static void WriteLog(string EDSTypeDirectoryName, string logTime, string logMessage, string logComment)
        {
            try
            {
                if (!Directory.Exists("Logs"))
                    Directory.CreateDirectory("Logs");

                if (!Directory.Exists("Logs\\" + EDSTypeDirectoryName + "\\" + DateTime.Now.Year.ToString()))
                    Directory.CreateDirectory("Logs\\" + EDSTypeDirectoryName + "\\" + DateTime.Now.Year.ToString());

                if (!Directory.Exists("Logs\\" + EDSTypeDirectoryName + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString()))
                    Directory.CreateDirectory("Logs\\" + EDSTypeDirectoryName + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString());

                if (!Directory.Exists("Logs\\" + EDSTypeDirectoryName + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString()))
                    Directory.CreateDirectory("Logs\\" + EDSTypeDirectoryName + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString());

                string path = "Logs\\" + EDSTypeDirectoryName + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" +
                    DateTime.Now.Date.ToShortDateString() + ".txt";

                StreamWriter sw;

                if (!File.Exists(path))
                    sw = new StreamWriter(path);
                else
                    sw = File.AppendText(path);

                sw.WriteLine("-------------------------------------");
                sw.WriteLine(logTime);
                sw.WriteLine();
                //sw.WriteLine(logType);
                //sw.WriteLine();
                sw.WriteLine(logMessage);
                sw.WriteLine();
                sw.WriteLine(logComment);
                sw.WriteLine("-------------------------------------");

                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.LoggingExceptionMessage, ex);
            }
        }

        public static void DeleteLog(bool deleteLog, int deleteLogPeriod)
        {
            //try
            //{
            if (deleteLog)
            {
                switch (deleteLogPeriod)
                {
                    case 0:
                        {
                            if (Directory.Exists("Logs\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString()))
                            {
                                string[] directories = Directory.GetFiles("Logs\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString());

                                for (int i = 0; i < directories.Length; i++)
                                {
                                    string logFileName = Path.GetFileName(directories[i]);

                                    DateTime logDay = DateTime.Parse(Path.GetFileNameWithoutExtension(logFileName));
                                    DateTime currentDay = DateTime.Now.Date;

                                    if (logDay < currentDay)
                                        File.Delete(directories[i]);
                                }
                            }
                            break;
                        }
                    case 1:
                        {
                            if (Directory.Exists("Logs\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString()))
                            {
                                string[] directories = Directory.GetFiles("Logs\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString());

                                for (int i = 0; i < directories.Length; i++)
                                {
                                    if (DayOfWeek.Monday == DateTime.Now.DayOfWeek)
                                    {
                                        string logFileName = Path.GetFileName(directories[i]);

                                        DateTime logDay = DateTime.Parse(Path.GetFileNameWithoutExtension(logFileName));
                                        DateTime currentDay = DateTime.Now.Date;

                                        if (logDay < currentDay)
                                            File.Delete(directories[i]);
                                    }
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            if (Directory.Exists("Logs\\" + DateTime.Now.Year.ToString()))
                            {
                                string[] directories = Directory.GetDirectories(("Logs\\" + DateTime.Now.Year.ToString()));

                                for (int i = 0; i < directories.Length; i++)
                                {
                                    int logMonth = int.Parse(Path.GetFileName(directories[i]));
                                    int currentMonth = DateTime.Now.Month;

                                    if (logMonth < currentMonth)
                                        Directory.Delete(directories[i], true);
                                }
                            }

                            break;
                        }
                    case 3:
                        {
                            if (Directory.Exists("Logs\\"))
                            {
                                string[] directories = Directory.GetDirectories(("Logs\\"));

                                for (int i = 0; i < directories.Length; i++)
                                {
                                    int logYear = int.Parse(Path.GetFileName(directories[i]));
                                    int currentYear = DateTime.Now.Year;

                                    if (logYear < currentYear)
                                        Directory.Delete(directories[i], true);
                                }
                            }
                            break;
                        }
                }

            }




                //if (!Directory.Exists("Logs"))
                //    Directory.CreateDirectory("Logs");

                //if (!Directory.Exists("Logs\\" + EDSTypeDirectoryName + "\\" + DateTime.Now.Year.ToString()))
                //    Directory.CreateDirectory("Logs\\" + EDSTypeDirectoryName + "\\" + DateTime.Now.Year.ToString());

                //if (!Directory.Exists("Logs\\" + EDSTypeDirectoryName + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString()))
                //    Directory.CreateDirectory("Logs\\" + EDSTypeDirectoryName + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString());

                //if (!Directory.Exists("Logs\\" + EDSTypeDirectoryName + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString()))
                //    Directory.CreateDirectory("Logs\\" + EDSTypeDirectoryName + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString());

                //string path = "Logs\\" + EDSTypeDirectoryName + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" +
                //    DateTime.Now.Date.ToShortDateString() + ".txt";

                //StreamWriter sw;

                //if (!File.Exists(path))
                //    sw = new StreamWriter(path);
                //else
                //    sw = File.AppendText(path);

                //sw.WriteLine("-------------------------------------");
                //sw.WriteLine(logTime);
                //sw.WriteLine();
                ////sw.WriteLine(logType);
                ////sw.WriteLine();
                //sw.WriteLine(logMessage);
                //sw.WriteLine();
                //sw.WriteLine(logComment);
                //sw.WriteLine("-------------------------------------");

                //sw.Flush();
                //sw.Close();
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ExceptionMessages.LoggingExceptionMessage, ex);
            //}
        }


    }
}
