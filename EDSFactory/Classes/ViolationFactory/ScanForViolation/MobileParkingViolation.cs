using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory
{
    class MobileParkingViolation : IScanForViolation
    {
        private static MobileParkingViolation m_do;
        DataSourceWatcher m_watcher;
        public MobileParkingViolation()
        {
            m_watcher = new DataSourceWatcher();
            m_watcher.AddWatcher(MobileParking.Singleton(MainForm.m_mf));
        }

        public static MobileParkingViolation Singleton()
        {
            if (m_do == null)
                m_do = new MobileParkingViolation();

            return m_do;
        } 
      
        public void Violation()
        {
            Settings.MobileParkingSettings eaevs = Settings.MobileParkingSettings.Singleton();
            DatabaseOperation.MobileParking eaevdo = DatabaseOperation.MobileParking.Singleton();
            Settings.WorkPlanAndMainSettings m_wpams = Settings.WorkPlanAndMainSettings.Singleton();
            DatabaseOperation.NTP ntpDatabase = DatabaseOperation.NTP.Singleton();

            eaevs = eaevs.DeSerialize(eaevs);
            m_wpams = m_wpams.DeSerialize(m_wpams);

            List<string> workPlan = m_wpams.MobileParkingWorkingPlan;

            //resimler klasöründeki resimleri alıyoruz
            //List<string> imageNameList = FileOperation.Find(eaevs.m_imagePath);
            Task<List<string>> taskDatabaseSelect = ntpDatabase.AsycSelect();
            taskDatabaseSelect.Wait();

            List<string> imageNameList = new List<string>(taskDatabaseSelect.Result);
            
            //eds formatında olmayan resimleri siliyoruz
            List<string> confirmedImageNameList = CheckImageNameOrList.CheckImageNameList(imageNameList, Enums.Validate.Valid, true, "MOBİL EDS");
            //resimler istenlen tipte ve aralıkta ise 
            List<string> wantedImageTypeAndRangeList = CheckImageNameOrList.FindTypeAndRangeList(confirmedImageNameList, Enums.TypeAndRange.Wanted, 6200, 6299, "L1-C1", "L1-C2");

            //resimler klasöründeki plakaları alıyoruz
            HashSet<string> plate = new HashSet<string>(wantedImageTypeAndRangeList.ConvertAll(x => ImageName.Plate(x)));

            ////doğru isim formatı, belirlenen resim tipleri ve aralıkta olan resimler için plakalarda geziyoruz
            foreach (string itemPlate in plate)
            {
                //resim listesini kontrol ediyoruz
                List<string> validatedImageNameList = CheckImageNameOrList.ValidateImageNameList(itemPlate, wantedImageTypeAndRangeList, Enums.Validate.Valid, "L1-C1", "L1-C2");

                List<string> checkedWorkPlan = ViolationsDate.CheckProgramWorkPlan(validatedImageNameList, workPlan, Enums.WorkPlan.In);

                List<DateTime> violationDateInDatabase = new List<DateTime>();

                Task<List<DateTime>> checkDatabaseViolation = DatabaseOperation.MobileParking.Singleton().AsycSelect(itemPlate);
                checkDatabaseViolation.Wait();

                for (int i = 0; i < checkDatabaseViolation.Result.Count; i++)
                    violationDateInDatabase.Add(checkDatabaseViolation.Result[i]);

                //ihlal resimleri listesi
                List<string> violationImagesNames = new List<string>();

                if (checkedWorkPlan.Count > 0)
                {
                    List<DateTime> violationDate = ViolationsDate.Date(violationDateInDatabase, Enums.Violation.Violation, eaevs.m_protectViolationTime,
                             checkedWorkPlan, new TimeSpan(eaevs.m_minViolationTimeHour, eaevs.m_minViolationTimeMinute, eaevs.m_minViolationTimeSecond),
                             new TimeSpan(eaevs.m_maxViolationTimeHour, eaevs.m_maxViolationTimeMinute, eaevs.m_maxViolationTimeSecond));

                    if (violationDate.Count > 0)
                        violationImagesNames.AddRange(ImageName.ViolationImagesNameList(violationDate, checkedWorkPlan));
                }

                if (violationImagesNames.Count > 0)//veritabanına kaydediyoruz
                {
                    //ihlal resimlerini sıralıyoruz
                    violationImagesNames.Sort();

                    //ihlal resimlerini veri tabanına kaydediyoruz
                    for (int i = 0; i < violationImagesNames.Count; i += 4)
                    {
                        string violationPlate = ImageName.Plate(violationImagesNames[i]);
                        string violationEntryDay = StringFormatOperation.Date(ImageName.Day(violationImagesNames[i]));
                        string violationEntryHour = StringFormatOperation.Hour(ImageName.Hour(violationImagesNames[i]));
                        string violationExitDay = StringFormatOperation.Date(ImageName.Day(violationImagesNames[i + 3]));
                        string violationExitHour = StringFormatOperation.Hour(ImageName.Hour(violationImagesNames[i + 3]));

                        string entryNarrowImageName = ImageName.Name(violationImagesNames[i]);
                        string entryWideImageName = ImageName.Name(violationImagesNames[i + 1]);
                        string exitNarrowImageName = ImageName.Name(violationImagesNames[i + 2]);
                        string exitWideImageName = ImageName.Name(violationImagesNames[i + 3]);

                        string violationEntryNarrowImageName = ViolationImagesNameFormat.MobileParking(ImageName.Plate(violationImagesNames[i]), ImageName.Day(violationImagesNames[i]), ImageName.Hour(violationImagesNames[i]),
                           eaevs.m_minViolationTimeMinute, ImageName.ImageType(violationImagesNames[i]), ImageName.PlaceNo(violationImagesNames[i]), ImageName.PlaceName(violationImagesNames[i]));
                        string violationEntryWideImageName = ViolationImagesNameFormat.MobileParking(ImageName.Plate(violationImagesNames[i + 1]), ImageName.Day(violationImagesNames[i + 1]), ImageName.Hour(violationImagesNames[i + 1]),
                           eaevs.m_minViolationTimeMinute, ImageName.ImageType(violationImagesNames[i + 1]), ImageName.PlaceNo(violationImagesNames[i + 1]), ImageName.PlaceName(violationImagesNames[i + 1]));
                        string violationExitNarrowImageName = ViolationImagesNameFormat.MobileParking(ImageName.Plate(violationImagesNames[i + 2]), ImageName.Day(violationImagesNames[i + 2]), ImageName.Hour(violationImagesNames[i + 2]),
                            eaevs.m_minViolationTimeMinute, ImageName.ImageType(violationImagesNames[i + 2]), ImageName.PlaceNo(violationImagesNames[i + 2]), ImageName.PlaceName(violationImagesNames[i + 2]));
                        string violationExitWideImageName = ViolationImagesNameFormat.MobileParking(ImageName.Plate(violationImagesNames[i + 3]), ImageName.Day(violationImagesNames[i + 3]), ImageName.Hour(violationImagesNames[i + 3]),
                            eaevs.m_minViolationTimeMinute, ImageName.ImageType(violationImagesNames[i + 3]), ImageName.PlaceNo(violationImagesNames[i + 3]), ImageName.PlaceName(violationImagesNames[i + 3]));

                        List<string> values = new List<string>(new string[] { violationPlate, violationEntryDay, violationEntryHour, violationExitDay, violationExitHour, violationEntryNarrowImageName,
                    violationEntryWideImageName, violationExitNarrowImageName, violationExitWideImageName, eaevs.m_thumbNailImagesPath });

                          Task<int> taskInsertViolation = eaevdo.AsyncInsert(values);
                        taskInsertViolation.Wait();

                        if (taskInsertViolation.Result > 0)
                        {
                            //küçük resimler için resmi boyutlandırıp tekrar kaydediyoruz.
                            ImageSize.ReSizeImageInList(eaevs.m_imagePath, new List<string>(new string[] {entryNarrowImageName,  entryWideImageName,
                                exitNarrowImageName , exitWideImageName}), eaevs.m_thumbNailImagesPath, new List<string>(new string[] {violationEntryNarrowImageName,  violationEntryWideImageName,
                                violationExitNarrowImageName , violationExitWideImageName}), new Size(160, 120));

                            //resimler ihlal resimleri klasörüne
                            FileOperation.Move(eaevs.m_imagePath, new List<string>(new string[] {entryNarrowImageName,  entryWideImageName,
                                exitNarrowImageName , exitWideImageName}), eaevs.m_violationImagesPath, new List<string>(new string[] {violationEntryNarrowImageName,  
                                    violationEntryWideImageName,violationExitNarrowImageName , violationExitWideImageName}));

                            m_watcher.SyncFileCreated(values); 
                        }

                        //sync resim listesini silioz
                        Task task = ntpDatabase.AsyncDelete(violationImagesNames[i]);
                        Task task1 = ntpDatabase.AsyncDelete(violationImagesNames[i + 1]);
                        Task task2 = ntpDatabase.AsyncDelete(violationImagesNames[i + 2]);
                        Task task3 = ntpDatabase.AsyncDelete(violationImagesNames[i + 3]);

                        Task.WaitAll(task, task1, task2, task3);
                    }

                    violationImagesNames.Clear();
                }

                if (eaevs.m_deleteImages)
                {
                    //ihlal olmayan resim listesini eklemek için liste oluşturduk
                    List<string> notViolationImageNames = new List<string>();

                    //resim listesini kontrol ediyoruz
                    //List<string> notValidatedImages = CheckImageNameOrList.ValidateImageNameList(itemPlate, wantedImageTypeAndRangeList, Enums.Validate.InValid, "L1-C1", "L1-C2");

                    List<string> invalid = new List<string>();

                    invalid = CheckImageNameOrList.ValidateImageNameList(itemPlate, wantedImageTypeAndRangeList, Enums.Validate.InValid, "L1-C1", "L1-C2");

                    foreach (string item in invalid)
                    {
                        if (!notViolationImageNames.Contains(item))
                        {
                            string day = ImageName.Day(item);
                            string hour = ImageName.Hour(item);

                            DateTime dr = ViolationsDate.StringDateToDateTime(day, hour);
                            DateTime drlimit = ViolationsDate.FindDate(dr, new TimeSpan(eaevs.m_maxViolationTimeHour, eaevs.m_maxViolationTimeMinute, eaevs.m_maxViolationTimeSecond), Enums.Date.Add);

                            if (DateTime.Now > drlimit)
                                notViolationImageNames.Add(item);
                        }
                    }


                    //if (notValidatedImages.Count > 0)
                    //{
                    //    foreach (string item in notValidatedImages)
                    //        if (!notViolationImageNames.Contains(item))
                    //            notViolationImageNames.Add(item);
                    //}
                    List<DateTime> notViolationDate = ViolationsDate.Date(violationDateInDatabase, Enums.Violation.NotViolation, eaevs.m_protectViolationTime,
                            checkedWorkPlan, new TimeSpan(eaevs.m_minViolationTimeHour, eaevs.m_minViolationTimeMinute, eaevs.m_minViolationTimeSecond),
                            new TimeSpan(eaevs.m_maxViolationTimeHour, eaevs.m_maxViolationTimeMinute, eaevs.m_maxViolationTimeSecond));

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
                        notViolationImageNames.Sort();


                        for (int i = 0; i < notViolationImageNames.Count; i++)
                        {
                            Task taskDatabaseDelete = ntpDatabase.AsyncDelete(notViolationImageNames[i]);
                            Task taskFileDelete = FileOperation.FileDeleteAsync(eaevs.m_imagePath, notViolationImageNames[i]);
                            Task.WaitAll(taskDatabaseDelete, taskFileDelete);
                        } 
                         
                        notViolationImageNames.Clear();
                    }
                }
            }

            if (eaevs.m_deleteImages)//plaka haricinde uygun olmayanları siliyoruz
            {
                //ihlal olmayan resim listesini eklemek için liste oluşturduk
                List<string> notViolationImageNames = new List<string>();

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

                //istenmeyen tipleri buluyoruz
                List<string> notWantedTypeAndRange = CheckImageNameOrList.FindTypeAndRangeList(imageNameList, Enums.TypeAndRange.NotWanted, 6200, 6299, "L1-C1", "L1-C2");

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
                        Task taskDatabaseDelete = ntpDatabase.AsyncDelete(notViolationImageNames[i]);
                        Task taskFileDelete = FileOperation.FileDeleteAsync(eaevs.m_imagePath, notViolationImageNames[i]);
                        Task.WaitAll(taskDatabaseDelete, taskFileDelete);
                    }  
                    
                    notViolationImageNames.Clear();
                }
            }
        }
    }
}