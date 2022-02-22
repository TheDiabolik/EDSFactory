using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDSFactory
{
    class FileOperation
    {
        private static readonly object m_lockFind = new object();
        //resimler klasöründeki resimleri liste atan metot
        public static List<string> Find(string imagesPath)
        {
            lock (m_lockFind)
            {
                try
                {
                    List<string> imagesName = new List<string>();

                    if (Directory.Exists(imagesPath))
                    {
                        DirectoryInfo di = new DirectoryInfo(imagesPath);
                        FileInfo[] fi = di.GetFiles();

                        foreach (FileInfo fileinfo in fi)
                            imagesName.Add(fileinfo.Name);
                    }

                    return imagesName;
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.FilePathExceptionMessage, ex);
                }
            }
        }



        public static List<string> FindNotValidImages(string imagesPath, bool hasPrefix)
        {
            try
            {
                List<string> imagesName = new List<string>();

                if (Directory.Exists(imagesPath))
                {
                    DirectoryInfo di = new DirectoryInfo(imagesPath);
                    FileInfo[] fi = di.GetFiles();

                    foreach (FileInfo fileinfo in fi)
                    {
                        if (!VerificateImageName.Name(fileinfo.Name, hasPrefix))
                            imagesName.Add(fileinfo.Name);
                    }
                }

                return imagesName;
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.FilePathExceptionMessage, ex);
            }
        }


        public static ConcurrentQueue<string> FindNotValidImages1(string imagesPath, bool hasPrefix)
        {
            lock (imagesPath)
            {
                try
                {
                    ConcurrentQueue<string> imagesName = new ConcurrentQueue<string>();

                    if (Directory.Exists(imagesPath))
                    {
                        DirectoryInfo di = new DirectoryInfo(imagesPath);
                        FileInfo[] fi = di.GetFiles();

                        foreach (FileInfo fileinfo in fi)
                        {
                            if (!VerificateImageName.Name(fileinfo.Name, hasPrefix))
                                imagesName.Enqueue(fileinfo.Name);
                        }
                    }

                    return imagesName;
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.FilePathExceptionMessage, ex);
                }
            }
        }



        //resimler klasöründeki resimleri liste atan metot
        public static List<string> Find(string imagesPath, bool hasPrefix)
        {
            lock (imagesPath)
            {
                try
                {
                    List<string> imagesName = new List<string>();

                    if (Directory.Exists(imagesPath))
                    {
                        DirectoryInfo di = new DirectoryInfo(imagesPath);
                        FileInfo[] fi = di.GetFiles();

                        foreach (FileInfo fileinfo in fi)
                        {
                            if (VerificateImageName.Name(fileinfo.Name, hasPrefix))
                                imagesName.Add(fileinfo.Name);
                        }
                    }

                    return imagesName;
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.FilePathExceptionMessage, ex);
                }
            }
        }



        //resimler klasöründeki resimleri liste atan metot
        public static MyList<string> FindByDate(string imagesPath, bool hasPrefix)
        {
            lock (imagesPath)
            {
                try
                {
                    MyList<string> imagesName = new MyList<string>();

                    if (Directory.Exists(imagesPath))
                    {
                        DirectoryInfo di = new DirectoryInfo(imagesPath);
                        FileInfo[] fi = di.GetFiles();

                        foreach (FileInfo fileinfo in fi)
                        {
                            //if (VerificateImageName.Name(fileinfo.Name, hasPrefix))
                                imagesName.Add(fileinfo.Name);
                        }
                    }

                    return imagesName;
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.FilePathExceptionMessage, ex);
                }
            }
        }

        //resimler klasöründeki resimleri liste atan metot
        public static ConcurrentQueue<string> TakeFile(string imagesPath, bool hasPrefix)
        {
            lock (imagesPath)
            {
                try
                {
                    ConcurrentQueue<string> imagesName = new ConcurrentQueue<string>();

                    if (Directory.Exists(imagesPath))
                    {
                        DirectoryInfo di = new DirectoryInfo(imagesPath);
                        FileInfo[] fi = di.GetFiles();

                        foreach (FileInfo fileinfo in fi)
                        {
                            //if (VerificateImageName.Name(fileinfo.Name, hasPrefix))
                            imagesName.Enqueue(fileinfo.Name);
                        }
                    }

                    return imagesName;
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.FilePathExceptionMessage, ex);
                }
            }
        }

        //resimler klasöründeki resimleri liste atan metot
        public static BlockingCollection<string> TakeFile1(string imagesPath, bool hasPrefix)
        {
            lock (imagesPath)
            {
                try
                {
                    BlockingCollection<string> imagesName = new BlockingCollection<string>();

                    if (Directory.Exists(imagesPath))
                    {
                        DirectoryInfo di = new DirectoryInfo(imagesPath);
                        FileInfo[] fi = di.GetFiles().OrderBy(p => p.LastWriteTime).ToArray();

                        foreach (FileInfo fileinfo in fi)
                        {
                            //if (VerificateImageName.Name(fileinfo.Name, hasPrefix))
                            imagesName.Add(fileinfo.Name);
                        }
                    }

                    return imagesName;
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.FilePathExceptionMessage, ex);
                }
            }
        }


        //resimler klasöründeki resimleri liste atan metot
        public static ConcurrentDictionary<string,DateTime> TakeFileAndReadTime(string imagesPath, bool hasPrefix)
        {
            lock (imagesPath)
            {
                try
                {
                    ConcurrentDictionary<string, DateTime> imagesName = new ConcurrentDictionary<string, DateTime>();

                    if (Directory.Exists(imagesPath))
                    {
                        DirectoryInfo di = new DirectoryInfo(imagesPath);
                        FileInfo[] fi = di.GetFiles();

                        foreach (FileInfo fileinfo in fi)
                        {
                            //if (VerificateImageName.Name(fileinfo.Name, hasPrefix))
                            imagesName.TryAdd(fileinfo.Name, DateTime.Now);
                        }
                    }

                    return imagesName;
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.FilePathExceptionMessage, ex);
                }
            }
        }

        //resimler klasöründeki resimleri liste atan metot
        public static BlockingCollection<KeyValuePair<string, DateTime>> TakeFileAndReadTime1(string imagesPath, bool hasPrefix)
        {
            lock (imagesPath)
            {
                try
                {
                    BlockingCollection<KeyValuePair<string, DateTime>> imagesName = new BlockingCollection<KeyValuePair<string, DateTime>>();

                    if (Directory.Exists(imagesPath))
                    {
                        DirectoryInfo di = new DirectoryInfo(imagesPath);
                        FileInfo[] fi = di.GetFiles();

                        foreach (FileInfo fileinfo in fi)
                        {
                            //if (VerificateImageName.Name(fileinfo.Name, hasPrefix))
                            imagesName.Add(new KeyValuePair<string, DateTime>(fileinfo.Name, DateTime.Now));
                        }
                    }

                    return imagesName;
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.FilePathExceptionMessage, ex);
                }
            }
        }


        //public static void MoveAsync(string imagePath, List<string> imagesNames, string violationImagePath, List<string> violationImagesNames)
        //{
        //    try
        //    {
                
        //               for (int i = 0; i < violationImagesNames.Count; i++)
        //                   MoveFile(imagePath, imagesNames[i], violationImagePath, violationImagesNames[i]);
                   
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ExceptionMessages.FileMoveExceptionMessage, ex);
        //    }
        //}

        public static void Copy(string imagePath, List<string> imagesNames, string violationImagePath, List<string> violationImagesNames)
        {
            try
            {
                for (int i = 0; i < violationImagesNames.Count; i++)
                    Copy(imagePath, imagesNames[i], violationImagePath, violationImagesNames[i]);

            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.FileMoveExceptionMessage, ex);
            }
        }


        public static void Move(string imagePath, List<string> imagesNames, string violationImagePath, List<string> violationImagesNames)
        {
            try
            {
                for (int i = 0; i < violationImagesNames.Count; i++)
                    Move(imagePath, imagesNames[i], violationImagePath, violationImagesNames[i]);

            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.FileMoveExceptionMessage, ex);
            }
        }

        //public static async void MoveFile(string imagePath, string imageName, string violationImagePath, string violationImageName)
        //{
        //    try
        //    {

        //        using (FileStream sourceStream = File.Open(imagePath + "\\" + imageName, FileMode.Open))
        //        {
        //            using (FileStream destinationStream = File.Create(violationImagePath + "\\" + violationImageName))
        //            {
        //                await sourceStream.CopyToAsync(destinationStream); 
        //            }
        //        } 
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ExceptionMessages.FileMoveExceptionMessage, ex);
        //    }
        //}



        private static readonly object m_lockMove = new object();
        public static void Move(string imagePath, string imageName, string violationImagePath, string violationImageName)
        {
            try
            {
                lock (m_lockMove)
                {

                    if (File.Exists(imagePath + "\\" + imageName))
                    {
                        bool isInUse = false;

                        do
                        {
                            isInUse = IsFileLocked(imagePath + "\\" + imageName);

                            if (isInUse)
                                Thread.Sleep(400);

                        } while (isInUse);


                        if (!Directory.Exists(violationImagePath))
                        {
                            Directory.CreateDirectory(violationImagePath);
                        }


                        if (!File.Exists(violationImagePath + "\\" + violationImageName))
                            File.Move(imagePath + "\\" + imageName, violationImagePath + "\\" + violationImageName);
                        else
                            Logging.WriteLog(DateTime.Now.ToString(), "dosya taşınmak istenen kaynakta var!", imagePath + "\\" + imageName, violationImagePath + "\\" + violationImageName, "move metot");
                    }
                } 
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.FileMoveExceptionMessage, ex);
            }
        }

        private static readonly object m_lockCopy = new object();
        public static void Copy(string imagePath, string imageName, string violationImagePath, string violationImageName)
        {
            try
            {
                lock (m_lockCopy)
                { 

                      if (!File.Exists(violationImagePath + "\\" + violationImageName))
                      {
                          bool isInUse = false;

                          do
                          {
                              isInUse = IsFileLocked(imagePath + "\\" + imageName);

                              if (isInUse)
                                  Thread.Sleep(400);

                          } while (isInUse); 

                          if (!isInUse) 
                              File.Copy(imagePath + "\\" + imageName, violationImagePath + "\\" + violationImageName); 
                      }
                 
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.FileMoveExceptionMessage, ex);
            }
        }


        public static void DeleteThumbNailImage(DataGridView dataGridView, int imagePathIndex, int entryNarrowImageNameIndex, int entryWideImageNameIndex, 
            int exitNarrowImageNameIndex, int exitWideImageNameIndex)
        {
            try
            {
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    string imagePath = dataGridView.Rows[i].Cells[imagePathIndex].Value.ToString();
                    string entryNarrowImageName = dataGridView.Rows[i].Cells[entryNarrowImageNameIndex].Value.ToString();
                    string entryWideImageName = dataGridView.Rows[i].Cells[entryWideImageNameIndex].Value.ToString();
                    string exitNarrowImageName = dataGridView.Rows[i].Cells[exitNarrowImageNameIndex].Value.ToString();
                    string exitWideImageName = dataGridView.Rows[i].Cells[exitWideImageNameIndex].Value.ToString();

                    DeleteFile(imagePath, new List<string>(new string[] { entryNarrowImageName, entryWideImageName, exitNarrowImageName, exitWideImageName }));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.DeleteImageExceptionMessage, ex);
            }
        }

        public static void DeleteThumbNailImage(DataGridView dataGridView, int imagePathIndex, int entryNarrowImageNameIndex, int entryWideImageNameIndex)
        {
            try
            {
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    string imagePath = dataGridView.Rows[i].Cells[imagePathIndex].Value.ToString();
                    string entryNarrowImageName = dataGridView.Rows[i].Cells[entryNarrowImageNameIndex].Value.ToString();
                    string entryWideImageName = dataGridView.Rows[i].Cells[entryWideImageNameIndex].Value.ToString();

                    DeleteFile(imagePath, new List<string>(new string[] { entryNarrowImageName, entryWideImageName }));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.DeleteImageExceptionMessage, ex);
            }
        }

    
        public static void DeleteFile(string imagePath, List<string> imagesNames)
        {

            try
            {
                //lock (m_lockDeleteFile)
                {
                    foreach (string imageName in imagesNames)
                    {
                        DeleteFile(imagePath, imageName);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "DeleteFile toplu metot"); 
                //throw new Exception(ExceptionMessages.DeleteImageExceptionMessage, ex);
            }

        }

        private static readonly object m_lockDeleteFile = new object();
        public static void DeleteFile(string imagePath, string imageName)
        {
          
            try
            {
                lock (m_lockDeleteFile)
                {

                    bool isInUse = false;

                    do
                    {
                        isInUse = IsFileLocked(imagePath + "\\" + imageName);

                        if (isInUse)
                            Thread.Sleep(400);

                    } while (isInUse);
 
                        if (File.Exists(imagePath + "\\" + imageName))
                        {
                            File.Delete(imagePath + "\\" + imageName);

                        }
                     
                }
            }
            catch (Exception ex)
            {
                Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "DeleteFile toplu metot3"); 
                throw new Exception(ExceptionMessages.DeleteImageExceptionMessage, ex);
            }
            
        }

        private static readonly object m_lockFileReturn = new object();

        public static bool DeleteFileReturnValue(string imagePath, string imageName)
        {  
           Monitor.Enter(m_lockFileReturn);
           
           bool value = false;

           try
           { 
               bool isInUse = false;

               do
               {
                   isInUse = IsFileLocked(imagePath + "\\" + imageName);

                   if (isInUse)
                       Thread.Sleep(400);

               } while (isInUse);

               if (File.Exists(imagePath + "\\" + imageName))
               {
                   File.Delete(imagePath + "\\" + imageName);
                   value = true;
               }

               return value;
           }
           catch (Exception ex)
           {
               Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "DeleteFile toplu metot4");
              return value;
               throw new Exception(ExceptionMessages.DeleteImageExceptionMessage, ex);  
           }
           finally
           {
               Monitor.Exit(m_lockFileReturn);
           }
           
        }
        


        public static Task FileDeleteAsync(string imagePath, string imageName)
        {
            return Task.Factory.StartNew(() =>
                     {
                         try
                         {
                             //if (File.Exists(imagePath + "\\" + imageName))
                             {
                                 bool isInUse = false;

                                 do
                                 {
                                     isInUse = IsFileLocked(imagePath + "\\" + imageName);

                                     if (isInUse)
                                         Thread.Sleep(400);

                                 } while (isInUse);

                                 File.Delete(imagePath + "\\" + imageName);
                             }
                         }
                         catch (Exception ex)
                         {
                             Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "DeleteFile asnkron");
                             throw new Exception(ExceptionMessages.DeleteImageExceptionMessage, ex);
                         }
                     });
        }



        private readonly static object m_lockIsFileLocked = new object();
        public static bool IsFileLocked(string path)
        {
            Monitor.Enter(m_lockIsFileLocked);

            FileStream stream = null;

            try
            {
                stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);

                if (stream != null)
                    return false;
                else
                    return true;
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();

                Monitor.Exit(m_lockIsFileLocked);
            }

        
        }
    }
 
}
