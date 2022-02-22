using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    class FixedParkingViolation : IScanForViolation
    {
        private static FixedParkingViolation m_do;
        DataSourceWatcher m_watcher;

        public FixedParkingViolation()
        {
            m_watcher = new DataSourceWatcher();
            m_watcher.AddWatcher(FixedParking.Singleton(MainForm.m_mf));
        }

        public static FixedParkingViolation Singleton()
        {
            if (m_do == null)
                m_do = new FixedParkingViolation();

            return m_do;
        } 

        public void Violation()
        {
            Settings.FixedParkingSettings settings = Settings.FixedParkingSettings.Singleton();
            DatabaseOperation.FixedParking database = DatabaseOperation.FixedParking.Singleton();
            Settings.WorkPlanAndMainSettings m_wpams = Settings.WorkPlanAndMainSettings.Singleton();
            DatabaseOperation.NTP ntpDatabase = DatabaseOperation.NTP.Singleton();

            settings = settings.DeSerialize(settings);
            m_wpams = m_wpams.DeSerialize(m_wpams);

            List<string> workPlan = m_wpams.FixedParkingWorkingPlan;

            ////resimler klasöründeki resimleri alıyoruz
            //List<string> imageNameList = FileOperation.Find(settings.m_imagePath);   
            //List<string> imageNameList = new List<string>(DatabaseOperation.NTP.Singleton().Select()); 
            Task<List<string>> taskDatabaseSelect = ntpDatabase.AsycSelect();
            taskDatabaseSelect.Wait();

            List<string> imageNameList = new List<string>(taskDatabaseSelect.Result);   

            //List<string> imageNameList = new List<string>(MainForm.m_syncImages);

            //eds formatında olmayan resimleri siliyoruz
            List<string> confirmedImageNameList = CheckImageNameOrList.CheckImageNameList(imageNameList, Enums.Validate.Valid, false, "");

            string[] wantedTypes; 

            if (settings.m_videoMode)
            { 
                wantedTypes = new string[] { "L1-C0", "L1-C1", "L1-C2"  };
            }
            else
            { 
                wantedTypes = new string[] { "L1-C1", "L1-C2"  };
            }


            //resimler istenlen tipte ve aralıkta ise 
            List<string> wantedImageTypeAndRangeList = CheckImageNameOrList.FindTypeAndRangeList(confirmedImageNameList, Enums.TypeAndRange.Wanted, 6000, 6999, wantedTypes);

            //resimler klasöründeki plakaları alıyoruz
            HashSet<string> plate = new HashSet<string>(wantedImageTypeAndRangeList.ConvertAll(x => ImageName.Plate(x)));

            ////doğru isim formatı, belirlenen resim tipleri ve aralıkta olan resimler için plakalarda geziyoruz
            foreach (string itemPlate in plate)
            { 
                //resim listesini kontrol ediyoruz
                List<string> validatedImageNameList = CheckImageNameOrList.ValidateImageNameList(itemPlate, wantedImageTypeAndRangeList, Enums.Validate.Valid, wantedTypes);

                List<string> checkedWorkPlan = ViolationsDate.CheckProgramWorkPlan(validatedImageNameList, workPlan, Enums.WorkPlan.In);

                List<DateTime> violationDateInDatabase = new List<DateTime>();

                Task<List<DateTime>> checkDatabaseViolation = DatabaseOperation.FixedParking.Singleton().AsycSelect(itemPlate);
                checkDatabaseViolation.Wait();

                for (int i = 0; i < checkDatabaseViolation.Result.Count; i++)
                    violationDateInDatabase.Add(checkDatabaseViolation.Result[i]); 
        
                //ihlal resimleri listesi
                List<string> violationImagesNames = new List<string>();

                if (checkedWorkPlan.Count > 0)
                {
                    List<DateTime> violationDate = ViolationsDate.Date(violationDateInDatabase, Enums.Violation.Violation, settings.m_protectViolationTime,
                             checkedWorkPlan, new TimeSpan(settings.m_minViolationTimeHour, settings.m_minViolationTimeMinute, settings.m_minViolationTimeSecond),
                             new TimeSpan(settings.m_maxViolationTimeHour, settings.m_maxViolationTimeMinute, settings.m_maxViolationTimeSecond));

                    if (violationDate.Count > 0)
                        violationImagesNames.AddRange(ImageName.ViolationImagesNameList(violationDate, checkedWorkPlan));
                }

                if (violationImagesNames.Count > 0)//veritabanına kaydediyoruz
                {
                    //ihlal resimlerini sıralıyoruz
                    violationImagesNames.Sort();

                    if (settings.m_videoMode)
                    {

                        //ihlal resimlerini veri tabanına kaydediyoruz
                        for (int i = 0; i < violationImagesNames.Count; i += 6)
                        {
                            string violationPlate = ImageName.Plate(violationImagesNames[i + 1]);
                            string violationEntryDay = StringFormatOperation.Date(ImageName.Day(violationImagesNames[i + 1]));
                            string violationEntryHour = StringFormatOperation.Hour(ImageName.Hour(violationImagesNames[i + 1]));
                            string violationExitDay = StringFormatOperation.Date(ImageName.Day(violationImagesNames[i + 4]));
                            string violationExitHour = StringFormatOperation.Hour(ImageName.Hour(violationImagesNames[i + 4]));

                            string entryVideoName = ImageName.Name(violationImagesNames[i]);
                            string entryNarrowImageName = ImageName.Name(violationImagesNames[i + 1]);
                            string entryWideImageName = ImageName.Name(violationImagesNames[i + 2]);
                            string exitVideoName = ImageName.Name(violationImagesNames[i + 3]);
                            string exitNarrowImageName = ImageName.Name(violationImagesNames[i + 4]);
                            string exitWideImageName = ImageName.Name(violationImagesNames[i + 5]);

                            string violationEntryNarrowImageName = ViolationImagesNameFormat.FixedParking(ImageName.Plate(violationImagesNames[i + 1]), ImageName.Day(violationImagesNames[i + 1]), ImageName.Hour(violationImagesNames[i + 1]),
                               settings.m_minViolationTimeMinute, ImageName.ImageType(violationImagesNames[i + 1]), ImageName.PlaceNo(violationImagesNames[i + 1]), ImageName.PlaceName(violationImagesNames[i + 1]));
                            string violationEntryWideImageName = ViolationImagesNameFormat.FixedParking(ImageName.Plate(violationImagesNames[i + 2]), ImageName.Day(violationImagesNames[i + 2]), ImageName.Hour(violationImagesNames[i + 2]),
                               settings.m_minViolationTimeMinute, ImageName.ImageType(violationImagesNames[i + 2]), ImageName.PlaceNo(violationImagesNames[i + 2]), ImageName.PlaceName(violationImagesNames[i + 2]));
                            string violationExitNarrowImageName = ViolationImagesNameFormat.FixedParking(ImageName.Plate(violationImagesNames[i + 4]), ImageName.Day(violationImagesNames[i + 4]), ImageName.Hour(violationImagesNames[i + 4]),
                                settings.m_minViolationTimeMinute, ImageName.ImageType(violationImagesNames[i + 4]), ImageName.PlaceNo(violationImagesNames[i + 4]), ImageName.PlaceName(violationImagesNames[i + 4]));
                            string violationExitWideImageName = ViolationImagesNameFormat.FixedParking(ImageName.Plate(violationImagesNames[i + 5]), ImageName.Day(violationImagesNames[i + 5]), ImageName.Hour(violationImagesNames[i + 5]),
                                settings.m_minViolationTimeMinute, ImageName.ImageType(violationImagesNames[i + 5]), ImageName.PlaceNo(violationImagesNames[i + 5]), ImageName.PlaceName(violationImagesNames[i + 5]));

                            List<string> values = new List<string>(new string[] { violationPlate, violationEntryDay, violationEntryHour, violationExitDay, violationExitHour, violationEntryNarrowImageName,
                    violationEntryWideImageName, violationExitNarrowImageName, violationExitWideImageName, settings.m_thumbNailImagesPath });

                            Task<int> taskInsertViolation = database.AsyncInsert(values);
                            taskInsertViolation.Wait();

                            if (taskInsertViolation.Result > 0)
                            {
                                //küçük resimler için resmi boyutlandırıp tekrar kaydediyoruz.
                                ImageSize.ReSizeImageInList(settings.m_imagePath, new List<string>(new string[] {entryNarrowImageName,  entryWideImageName,
                                exitNarrowImageName , exitWideImageName}), settings.m_thumbNailImagesPath, new List<string>(new string[] {violationEntryNarrowImageName,  violationEntryWideImageName,
                                violationExitNarrowImageName , violationExitWideImageName}), new Size(160, 120));

                                //resimler ihlal resimleri klasörüne
                                FileOperation.Move(settings.m_imagePath, new List<string>(new string[] {entryNarrowImageName,  entryWideImageName,
                                exitNarrowImageName , exitWideImageName}), settings.m_violationImagesPath, new List<string>(new string[] {violationEntryNarrowImageName,  
                                    violationEntryWideImageName,violationExitNarrowImageName , violationExitWideImageName}));

                                //resimler ihlal resimleri klasörüne
                                FileOperation.Move(settings.m_imagePath, new List<string>(new string[] { entryVideoName, exitVideoName }),
                                    settings.m_violationImagesPath, new List<string>(new string[] { entryVideoName, exitVideoName }));

                                m_watcher.SyncFileCreated(values);

                            }

                            //sync resim listesini silioz
                            Task taskvi = ntpDatabase.AsyncDelete(violationImagesNames[i]);
                            Task taskvi1 = ntpDatabase.AsyncDelete(violationImagesNames[i + 3]);


                            Task task = ntpDatabase.AsyncDelete(violationImagesNames[i + 1]);
                            Task task1 = ntpDatabase.AsyncDelete(violationImagesNames[i + 2]);
                            Task task2 = ntpDatabase.AsyncDelete(violationImagesNames[i + 4]);
                            Task task3 = ntpDatabase.AsyncDelete(violationImagesNames[i + 5]);

                            Task.WaitAll(task, task1, task2, task3, taskvi, taskvi1);
                        }
                    }
                    else
                    {
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

                            string violationEntryNarrowImageName = ViolationImagesNameFormat.FixedParking(ImageName.Plate(violationImagesNames[i]), ImageName.Day(violationImagesNames[i]), ImageName.Hour(violationImagesNames[i]),
                               settings.m_minViolationTimeMinute, ImageName.ImageType(violationImagesNames[i]), ImageName.PlaceNo(violationImagesNames[i]), ImageName.PlaceName(violationImagesNames[i]));
                            string violationEntryWideImageName = ViolationImagesNameFormat.FixedParking(ImageName.Plate(violationImagesNames[i + 1]), ImageName.Day(violationImagesNames[i + 1]), ImageName.Hour(violationImagesNames[i + 1]),
                               settings.m_minViolationTimeMinute, ImageName.ImageType(violationImagesNames[i + 1]), ImageName.PlaceNo(violationImagesNames[i + 1]), ImageName.PlaceName(violationImagesNames[i + 1]));
                            string violationExitNarrowImageName = ViolationImagesNameFormat.FixedParking(ImageName.Plate(violationImagesNames[i + 2]), ImageName.Day(violationImagesNames[i + 2]), ImageName.Hour(violationImagesNames[i + 2]),
                                settings.m_minViolationTimeMinute, ImageName.ImageType(violationImagesNames[i + 2]), ImageName.PlaceNo(violationImagesNames[i + 2]), ImageName.PlaceName(violationImagesNames[i + 2]));
                            string violationExitWideImageName = ViolationImagesNameFormat.FixedParking(ImageName.Plate(violationImagesNames[i + 3]), ImageName.Day(violationImagesNames[i + 3]), ImageName.Hour(violationImagesNames[i + 3]),
                                settings.m_minViolationTimeMinute, ImageName.ImageType(violationImagesNames[i + 3]), ImageName.PlaceNo(violationImagesNames[i + 3]), ImageName.PlaceName(violationImagesNames[i + 3]));

                            List<string> values = new List<string>(new string[] { violationPlate, violationEntryDay, violationEntryHour, violationExitDay, violationExitHour, violationEntryNarrowImageName,
                    violationEntryWideImageName, violationExitNarrowImageName, violationExitWideImageName, settings.m_thumbNailImagesPath });

                            Task<int> taskInsertViolation = database.AsyncInsert(values);
                            taskInsertViolation.Wait();

                            if (taskInsertViolation.Result > 0)
                            {
                                //küçük resimler için resmi boyutlandırıp tekrar kaydediyoruz.
                                ImageSize.ReSizeImageInList(settings.m_imagePath, new List<string>(new string[] {entryNarrowImageName,  entryWideImageName,
                                exitNarrowImageName , exitWideImageName}), settings.m_thumbNailImagesPath, new List<string>(new string[] {violationEntryNarrowImageName,  violationEntryWideImageName,
                                violationExitNarrowImageName , violationExitWideImageName}), new Size(160, 120));

                                //resimler ihlal resimleri klasörüne
                                FileOperation.Move(settings.m_imagePath, new List<string>(new string[] {entryNarrowImageName,  entryWideImageName,
                                exitNarrowImageName , exitWideImageName}), settings.m_violationImagesPath, new List<string>(new string[] {violationEntryNarrowImageName,  
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
                    }   
                     

                    violationImagesNames.Clear();
                }

                if (settings.m_deleteImages)
                {
                    //ihlal olmayan resim listesini eklemek için liste oluşturduk
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
                            DateTime drlimit = ViolationsDate.FindDate(dr, new TimeSpan(settings.m_maxViolationTimeHour, settings.m_maxViolationTimeMinute, settings.m_maxViolationTimeSecond), Enums.Date.Add);

                            if (DateTime.Now > drlimit)
                                notViolationImageNames.Add(item); 
                        }
                    }


                    //resim listesini kontrol ediyoruz
                    //List<string> notValidatedImages = CheckImageNameOrList.ValidateImageNameList(itemPlate, wantedImageTypeAndRangeList, Enums.Validate.InValid, "L1-C1", "L1-C2");

                    //if (notValidatedImages.Count > 0)
                    //{
                    //    foreach (string item in notValidatedImages)
                    //        if (!notViolationImageNames.Contains(item))
                    //            notViolationImageNames.Add(item);
                    //}
                    List<DateTime> notViolationDate = ViolationsDate.Date(violationDateInDatabase, Enums.Violation.NotViolation, settings.m_protectViolationTime,
                            checkedWorkPlan, new TimeSpan(settings.m_minViolationTimeHour, settings.m_minViolationTimeMinute, settings.m_minViolationTimeSecond),
                            new TimeSpan(settings.m_maxViolationTimeHour, settings.m_maxViolationTimeMinute, settings.m_maxViolationTimeSecond));

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
                            Task taskFileDelete = FileOperation.FileDeleteAsync(settings.m_imagePath, notViolationImageNames[i]);
                            Task.WaitAll(taskDatabaseDelete, taskFileDelete);
                        }

                        notViolationImageNames.Clear();
                    }
                }

       
            }

            if (settings.m_deleteImages)//plaka haricinde uygun olmayanları siliyoruz
            {
                //ihlal olmayan resim listesini eklemek için liste oluşturduk
                List<string> notViolationImageNames = new List<string>();

                List<string> notConfirmedImageNameList = CheckImageNameOrList.CheckImageNameList(imageNameList, Enums.Validate.InValid, false, "");

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
                List<string> notWantedTypeAndRange = CheckImageNameOrList.FindTypeAndRangeList(imageNameList, Enums.TypeAndRange.NotWanted, 6000, 6999, wantedTypes);

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
                        Task taskFileDelete = FileOperation.FileDeleteAsync(settings.m_imagePath, notViolationImageNames[i]);
                        Task.WaitAll(taskDatabaseDelete, taskFileDelete);
                    } 

                    notViolationImageNames.Clear();
                }
            }

        }
    }
}
