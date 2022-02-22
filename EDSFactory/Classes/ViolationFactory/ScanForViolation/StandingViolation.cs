﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    class StandingViolation : IScanForViolation
    {
        private static StandingViolation m_do;
          DataSourceWatcher m_watcher;
          public StandingViolation()
        {
            m_watcher = new DataSourceWatcher();
            m_watcher.AddWatcher(Standing.Singleton(MainForm.m_mf));
        }

        public static StandingViolation Singleton()
        {
            if (m_do == null)
                m_do = new StandingViolation();

            return m_do;
        } 
        public void Violation()
        {
            Settings.StandingSettings m_ses = Settings.StandingSettings.Singleton();
            DatabaseOperation.Standing se = DatabaseOperation.Standing.Singleton();
            Settings.WorkPlanAndMainSettings m_wpams = Settings.WorkPlanAndMainSettings.Singleton();
            DatabaseOperation.NTP ntpDatabase = DatabaseOperation.NTP.Singleton();

            m_ses = m_ses.DeSerialize(m_ses);
            m_wpams = m_wpams.DeSerialize(m_wpams);

            List<string> workPlan = m_wpams.StandingWorkingPlan; 
          

            //resimler klasöründeki resimleri alıyoruz
            List<string> imageNameList = FileOperation.Find(m_ses.m_imagePath + "\\" + "sync");
            //Logging.WriteLog(DateTime.Now.ToString(), "", "", "", "Violation 1");
            //Task<List<string>> taskDatabaseSelect = ntpDatabase.AsycSelect();
            //Logging.WriteLog(DateTime.Now.ToString(), "", "", "", "Violation 2");
            //taskDatabaseSelect.Wait();
            //Logging.WriteLog(DateTime.Now.ToString(), "", "", "", "Violation 3");

            //List<string> imageNameList = new List<string>(taskDatabaseSelect.Result); 

            //eds formatında olmayan resimleri siliyoruz
            List<string> confirmedImageNameList = CheckImageNameOrList.CheckImageNameList(imageNameList, Enums.Validate.Valid, true, "MOBİL EDS");


            string[] entryWantedTypes, exitWantedTypes;
            string[] wantedTypes;


            if (m_ses.m_videoMode)
            { 
                wantedTypes = new string[] { "L1-C0", "L1-C1", "L1-C2" };
            }
            else
            {
                wantedTypes = new string[] { "L1-C1", "L1-C2" };
            }



            //resimler istenlen tipte ve aralıkta ise 
            List<string> wantedImageTypeAndRangeList = CheckImageNameOrList.FindTypeAndRangeList(confirmedImageNameList, Enums.TypeAndRange.Wanted, 6300, 6399, wantedTypes);//son üç hane 000
            //Logging.WriteLog(DateTime.Now.ToString(), "", "", "", "Violation 4");
            //resimler klasöründeki plakaları alıyoruz
            HashSet<string> plate = new HashSet<string>(wantedImageTypeAndRangeList.ConvertAll(x => ImageName.Plate(x)));

            ////doğru isim formatı, belirlenen resim tipleri ve aralıkta olan resimler için plakalarda geziyoruz
            foreach (string itemPlate in plate)
            {
                //resim listesini kontrol ediyoruz
                List<string> validatedImageNameList = CheckImageNameOrList.ValidateImageNameList(itemPlate, wantedImageTypeAndRangeList,
               Enums.Validate.Valid, wantedTypes);

                List<string> checkedWorkPlan = ViolationsDate.CheckProgramWorkPlan(validatedImageNameList, workPlan, Enums.WorkPlan.In);

                List<DateTime> violationDateInDatabase = new List<DateTime>();
                //Logging.WriteLog(DateTime.Now.ToString(), "", "", "", "Violation 5");

                Task<List<DateTime>> checkDatabaseViolation = DatabaseOperation.Standing.Singleton().AsycSelect(itemPlate);
                checkDatabaseViolation.Wait();

                for (int i = 0; i < checkDatabaseViolation.Result.Count; i++)
                    violationDateInDatabase.Add(checkDatabaseViolation.Result[i]); 

                //ihlal resimleri listesi
                List<string> violationImagesNames = new List<string>();

                if (checkedWorkPlan.Count > 0)
                {
                    List<DateTime> violationTime = ViolationsDate.Date(violationDateInDatabase, Enums.Violation.Violation, m_ses.m_protectViolationTime, checkedWorkPlan);

                    if (violationTime.Count > 0)   //ihlal oluşturan resimlerin isimlerini alıyoruz
                        violationImagesNames.AddRange(ImageName.ViolationImagesNameList(violationTime, checkedWorkPlan));
                }

                if (violationImagesNames.Count > 0)
                {
                    //ihlal resimlerini sıralıyoruz
                    violationImagesNames.Sort();



                    if (m_ses.m_videoMode)//video mod aktifse
                    {
                        //ihlal resimlerini veri tabanına kaydediyoruz
                        for (int i = 0; i < violationImagesNames.Count; i += 3)
                        {
                            string violationPlate = ImageName.Plate(violationImagesNames[i]);
                            string violationDay = StringFormatOperation.Date(ImageName.Day(violationImagesNames[i]));
                            string violationHour = StringFormatOperation.Hour(ImageName.Hour(violationImagesNames[i]));
                            string firstImageName = ViolationImagesNameFormat.MobileHighwayShoulder(ImageName.Plate(violationImagesNames[i + 1]), ImageName.Day(violationImagesNames[i + 1]), ImageName.Hour(violationImagesNames[i + 1]),
                              ImageName.ImageType(violationImagesNames[i + 1]), ImageName.PlaceNo(violationImagesNames[i + 1]), ImageName.PlaceName(violationImagesNames[i + 1]));
                            string secondImageName = ViolationImagesNameFormat.MobileHighwayShoulder(ImageName.Plate(violationImagesNames[i + 2]), ImageName.Day(violationImagesNames[i + 2]), ImageName.Hour(violationImagesNames[i + 2]),
                                ImageName.ImageType(violationImagesNames[i + 2]), ImageName.PlaceNo(violationImagesNames[i + 2]), ImageName.PlaceName(violationImagesNames[i + 2]));


                            List<string> values = new List<string>(new string[] { violationPlate, violationDay, violationHour, firstImageName, secondImageName, m_ses.m_thumbNailImagesPath });

                            Task<int> taskInsertViolation = se.AsyncInsert(values);
                            taskInsertViolation.Wait();


                            if (taskInsertViolation.Result > 0)
                            {
                                //küçük resimler için resmi boyutlandırıp tekrar kaydediyoruz.
                                ImageSize.ReSizeImageInList(m_ses.m_imagePath, new List<string>(new string[] { firstImageName, secondImageName }), m_ses.m_thumbNailImagesPath, new List<string>(new string[] { firstImageName, secondImageName })
                                , new Size(160, 120));
                                //resimler ihlal resimleri klasörüne
                                FileOperation.Move(m_ses.m_imagePath, new List<string>(new string[] { firstImageName, secondImageName }),
                                    m_ses.m_violationImagesPath, new List<string>(new string[] { firstImageName, secondImageName }));

                                m_watcher.SyncFileCreated(values);
                                //MainForm.m_digitalSignature.SignFile(m_ses.m_violationImagesPath + "\\" + firstImageName);
                                //MainForm.m_digitalSignature.SignFile(m_ses.m_violationImagesPath + "\\" + secondImageName); 
                            }

                            //sync resim listesini silioz
                            //Task task = ntpDatabase.AsyncDelete(violationImagesNames[i]);
                            //Task task1 = ntpDatabase.AsyncDelete(violationImagesNames[i + 1]);

                            //Task.WaitAll(task, task1);
                        }
                    }
                    
                    else
                    {   
                    //ihlal resimlerini veri tabanına kaydediyoruz
                    for (int i = 0; i < violationImagesNames.Count; i += 2)
                    {
                        string violationPlate = ImageName.Plate(violationImagesNames[i]);
                        string violationDay = StringFormatOperation.Date(ImageName.Day(violationImagesNames[i]));
                        string violationHour = StringFormatOperation.Hour(ImageName.Hour(violationImagesNames[i]));
                        string firstImageName = ViolationImagesNameFormat.MobileHighwayShoulder(ImageName.Plate(violationImagesNames[i]), ImageName.Day(violationImagesNames[i]), ImageName.Hour(violationImagesNames[i]),
                          ImageName.ImageType(violationImagesNames[i]), ImageName.PlaceNo(violationImagesNames[i]), ImageName.PlaceName(violationImagesNames[i]));
                        string secondImageName = ViolationImagesNameFormat.MobileHighwayShoulder(ImageName.Plate(violationImagesNames[i + 1]), ImageName.Day(violationImagesNames[i + 1]), ImageName.Hour(violationImagesNames[i + 1]),
                            ImageName.ImageType(violationImagesNames[i + 1]), ImageName.PlaceNo(violationImagesNames[i + 1]), ImageName.PlaceName(violationImagesNames[i + 1]));


                        List<string> values = new List<string>(new string[] { violationPlate, violationDay, violationHour, firstImageName, secondImageName, m_ses.m_thumbNailImagesPath });


                        //Logging.WriteLog(DateTime.Now.ToString(), violationPlate, violationDay, violationHour, "Violation 6");
                        //Logging.WriteLog(DateTime.Now.ToString(), firstImageName, secondImageName, m_ses.m_thumbNailImagesPath, "Violation 6");
                        Task<int> taskInsertViolation = se.AsyncInsert(values);
                        taskInsertViolation.Wait();


                        if (taskInsertViolation.Result > 0)
                        {
                            //küçük resimler için resmi boyutlandırıp tekrar kaydediyoruz.
                            ImageSize.ReSizeImageInList(m_ses.m_imagePath + "\\" + "sync", new List<string>(new string[] { firstImageName, secondImageName }), m_ses.m_thumbNailImagesPath, new List<string>(new string[] { firstImageName, secondImageName })
                            , new Size(160, 120));
                            //resimler ihlal resimleri klasörüne
                            FileOperation.Move(m_ses.m_imagePath + "\\" + "sync", new List<string>(new string[] { firstImageName, secondImageName }),
                                m_ses.m_violationImagesPath, new List<string>(new string[] { firstImageName, secondImageName }));

                            m_watcher.SyncFileCreated(values); 
                            //MainForm.m_digitalSignature.SignFile(m_ses.m_violationImagesPath + "\\" + firstImageName);
                            //MainForm.m_digitalSignature.SignFile(m_ses.m_violationImagesPath + "\\" + secondImageName); 
                        }

                        //sync resim listesini silioz
                        //Task task = ntpDatabase.AsyncDelete(violationImagesNames[i]);
                        //Task task1 = ntpDatabase.AsyncDelete(violationImagesNames[i + 1]);

                        //Task.WaitAll(task, task1); 
                    }

                    }
                    
                    
                 
                    violationImagesNames.Clear();
                }

                if (m_ses.m_deleteImages)
                {
                    //ihlal olmayan resimleri atıyoruz
                    List<string> notViolationImageNames = new List<string>();

                    List<string> invalid = new List<string>();
                    invalid = CheckImageNameOrList.ValidateImageNameList(itemPlate, wantedImageTypeAndRangeList, Enums.Validate.InValid, wantedTypes);

                    foreach (string item in invalid)
                    {
                        if (!notViolationImageNames.Contains(item))
                        {
                            string day = ImageName.Day(item);
                            string hour = ImageName.Hour(item);

                            DateTime dr = ViolationsDate.StringDateToDateTime(day, hour);
                            DateTime drlimit = ViolationsDate.FindDate(dr, new TimeSpan(0, 1, 0), Enums.Date.Add);

                            if (DateTime.Now > drlimit)
                                notViolationImageNames.Add(item);
                        }
                    }
                     

                    //ihlal oluşturmayan resimleri silmek için
                    List<DateTime> notViolationDate = ViolationsDate.Date(violationDateInDatabase, Enums.Violation.NotViolation, m_ses.m_protectViolationTime, checkedWorkPlan);

                    if (notViolationDate.Count > 0)
                    {
                        List<string> notViolationDateImagesNames = ImageName.ViolationImagesNameList(notViolationDate, validatedImageNameList);

                        if (notViolationDateImagesNames.Count > 0)
                        {
                            foreach (string item in notViolationDateImagesNames)
                                if (!notViolationImageNames.Contains(item))
                                    notViolationImageNames.Add(item);
                        }
                    }

                    List<string> outWorkPlan = ViolationsDate.CheckProgramWorkPlan(validatedImageNameList, workPlan, Enums.WorkPlan.Out);

                    if (outWorkPlan.Count > 0)
                    {
                        foreach (string item in outWorkPlan)
                            if (!notViolationImageNames.Contains(item))
                                notViolationImageNames.Add(item);
                    }

                    if (notViolationImageNames.Count > 0)
                    {
                        for (int i = 0; i < notViolationImageNames.Count; i++)
                        {
                            //Task taskDatabaseDelete = ntpDatabase.AsyncDelete(notViolationImageNames[i]);
                            Task taskFileDelete = FileOperation.FileDeleteAsync(m_ses.m_imagePath + "\\" + "sync", notViolationImageNames[i]);
                            //Task.WaitAll(taskDatabaseDelete, taskFileDelete);
                            taskFileDelete.Wait();
                        }
                    
                        notViolationImageNames.Clear();
                    }
                }
            }

            if (m_ses.m_deleteImages)//plaka haricinde uygun olmayan formatta resim varsa
            {
                //ihlal olmayan resimleri atıyoruz
                List<string> notViolationImageNames = new List<string>();

                if (imageNameList.Count > 0)
                {
                    ////uygun formatta olmayan resimleri siliyor
                    List<string> notConfirmedImageNameList = CheckImageNameOrList.CheckImageNameList(imageNameList, Enums.Validate.InValid, true, "MOBİL EDS");

                    if (notConfirmedImageNameList.Count > 0)
                    {
                        foreach (string item in notConfirmedImageNameList)
                            if (!notViolationImageNames.Contains(item))
                            {
                                notViolationImageNames.Add(item);
                                imageNameList.Remove(item);
                            }
                    }
                    //istediğimiz aralıkta olmayan ve tipte olmayan resimleri silmek için
                    List<string> notWantedTypeAndRange = CheckImageNameOrList.FindTypeAndRangeList(imageNameList, Enums.TypeAndRange.NotWanted, 6300, 6399, wantedTypes);//son üç hane 000

                    if (notWantedTypeAndRange.Count > 0)
                    {
                        foreach (string item in notWantedTypeAndRange)
                            if (!notViolationImageNames.Contains(item))
                            {
                                notViolationImageNames.Add(item);
                                imageNameList.Remove(item);
                            }
                    }

                    if (notViolationImageNames.Count > 0)
                    {
                        
                        for (int i = 0; i < notViolationImageNames.Count; i++)
                        {
                            //Task taskDatabaseDelete = ntpDatabase.AsyncDelete(notViolationImageNames[i]);
                            Task taskFileDelete = FileOperation.FileDeleteAsync(m_ses.m_imagePath + "\\" + "sync", notViolationImageNames[i]);
                            //Task.WaitAll(taskDatabaseDelete, taskFileDelete);
                            taskFileDelete.Wait();
                        }
 
                        notViolationImageNames.Clear();
                    }
                }
            }
        }
    }
}
