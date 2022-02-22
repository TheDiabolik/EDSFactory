using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    class FixedHighwayShoulderViolation : IScanForViolation
    {
        private static FixedHighwayShoulderViolation m_do;
        DataSourceWatcher m_watcher;

        public FixedHighwayShoulderViolation()
        {
            m_watcher = new DataSourceWatcher();
            m_watcher.AddWatcher(FixedHighwayShoulder.Singleton(MainForm.m_mf));
        }
         

        public static FixedHighwayShoulderViolation Singleton()
        {
            if (m_do == null)
                m_do = new FixedHighwayShoulderViolation();

            return m_do;
        } 


        public void Violation()
        {
            Settings.FixedHighwayShoulderSettings settings = Settings.FixedHighwayShoulderSettings.Singleton();
            DatabaseOperation.FixedHighwayShoulder eaevdo = DatabaseOperation.FixedHighwayShoulder.Singleton();
            Settings.WorkPlanAndMainSettings m_wpams = Settings.WorkPlanAndMainSettings.Singleton();
            DatabaseOperation.NTP ntpDatabase = DatabaseOperation.NTP.Singleton();

            settings = settings.DeSerialize(settings);
            m_wpams = m_wpams.DeSerialize(m_wpams);

            List<string> workPlan = m_wpams.FixedHighwayShoulderWorkingPlan;

            //resimler klasöründeki resimleri alıyoruz
            //List<string> imageNameList = FileOperation.Find(eaevs.m_imagePath);   

            Task<List<string>> taskDatabaseSelect = ntpDatabase.AsycSelect();
            taskDatabaseSelect.Wait();

            List<string> imageNameList = new List<string>(taskDatabaseSelect.Result);   

            //eds formatında olmayan resimleri siliyoruz
            List<string> confirmedImageNameList = CheckImageNameOrList.CheckImageNameList(imageNameList, Enums.Validate.Valid, false, ""); 

            string[] entryWantedTypes, exitWantedTypes; 
            string[] wantedTypes;


            if (settings.m_videoMode)
            {
                entryWantedTypes = new string[] { "L1-C0", "L1-C1", "L1-C2" }; 
                exitWantedTypes = new string[] { "L2-C0", "L2-C1", "L2-C2" };
                wantedTypes = new string[] { "L1-C0", "L1-C1", "L1-C2", "L2-C0", "L2-C1", "L2-C2" }; 
            } 
            else
            {
                entryWantedTypes = new string[] {  "L1-C1", "L1-C2" };
                exitWantedTypes = new string[] {  "L2-C1", "L2-C2" };
                wantedTypes = new string[] {   "L1-C1", "L1-C2",   "L2-C1", "L2-C2" }; 
            }

            //resimler istenlen tipte ve aralıkta ise 
            List<string> wantedImageTypeAndRangeList = CheckImageNameOrList.FindTypeAndRangeList(confirmedImageNameList, Enums.TypeAndRange.Wanted, 0050, 0299, wantedTypes);

            //resimler klasöründeki plakaları alıyoruz
            HashSet<string> plate = new HashSet<string>(wantedImageTypeAndRangeList.ConvertAll(x => ImageName.Plate(x)));
            
            ////doğru isim formatı, belirlenen resim tipleri ve aralıkta olan resimler için plakalarda geziyoruz
            foreach (string itemPlate in plate)
            {   
                ////resim listesini kontrol ediyoruz
                List<string> validatedImageNameList = new List<string>();

                validatedImageNameList.AddRange(CheckImageNameOrList.ValidateImageNameList(itemPlate, wantedImageTypeAndRangeList, Enums.Validate.Valid, entryWantedTypes));
                validatedImageNameList.AddRange(CheckImageNameOrList.ValidateImageNameList(itemPlate, wantedImageTypeAndRangeList, Enums.Validate.Valid, exitWantedTypes));

                //List<string> validatedImageNameList = wantedImageTypeAndRangeList;

                List<string> checkedWorkPlan = ViolationsDate.CheckProgramWorkPlan(validatedImageNameList, workPlan, Enums.WorkPlan.In);

                List<string> entryCheckedWorkPlan = checkedWorkPlan.FindAll(x => entryWantedTypes.Contains(ImageName.ImageType(x)));
                List<string> exitCheckedWorkPlan = checkedWorkPlan.FindAll(x => exitWantedTypes.Contains(ImageName.ImageType(x)));

                //video revizyonu
                //List<string> entryCheckedWorkPlan = checkedWorkPlan.FindAll(x => (ImageName.ImageType(x) == "L1-C0") || (ImageName.ImageType(x) == "L1-C1") || (ImageName.ImageType(x) == "L1-C2"));
                //List<string> exitCheckedWorkPlan = checkedWorkPlan.FindAll(x => (ImageName.ImageType(x) == "L2-C0") || (ImageName.ImageType(x) == "L2-C1") || (ImageName.ImageType(x) == "L2-C2"));
  
                List<DateTime> violationDateInDatabase = new List<DateTime>();

                Task <List<DateTime>> checkDatabaseViolation = DatabaseOperation.FixedHighwayShoulder.Singleton().AsycSelect(itemPlate);
                checkDatabaseViolation.Wait(); 
              
                for (int i = 0; i < checkDatabaseViolation.Result.Count; i++)
                    violationDateInDatabase.Add(checkDatabaseViolation.Result[i]);

                entryCheckedWorkPlan.Sort();
                exitCheckedWorkPlan.Sort();

                //ihlal resimleri listesi
                List<string> violationImagesNames = new List<string>();

                if (entryCheckedWorkPlan.Count > 0 && exitCheckedWorkPlan.Count > 0)
                {
                    List<string> violationDate = new List<string>();

                    int speedTolerance = 0;

                    if (settings.m_applyTolerance)
                        speedTolerance = settings.m_tolerancePercentage; 

                    violationDate = ViolationsDate.Date(violationDateInDatabase, Enums.Violation.Violation, settings.m_protectViolationTime, entryCheckedWorkPlan, exitCheckedWorkPlan,
                        settings.m_distance, settings.m_speed, speedTolerance);

                    if (violationDate.Count > 0)
                    {
                        List<DateTime> entryDate = new List<DateTime>();
                        List<DateTime> exitDate = new List<DateTime>();

                        foreach (var item in violationDate)
                        {
                            if (item.Split(',')[1] == "Entry")
                                entryDate.Add(DateTime.Parse(item.Split(',')[0]));
                            else if (item.Split(',')[1] == "Exit")
                                exitDate.Add(DateTime.Parse(item.Split(',')[0]));

                        }

                        violationImagesNames.AddRange(ImageName.ViolationImagesNameList(entryDate, entryCheckedWorkPlan));
                        violationImagesNames.AddRange(ImageName.ViolationImagesNameList(exitDate, exitCheckedWorkPlan));
                    }

                }

                if (violationImagesNames.Count > 0)//veritabanına kaydediyoruz
                {
                    //ihlal resimlerini sıralıyoruz
                    violationImagesNames.Sort();


                    if(settings.m_videoMode)//video mod aktifse
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

                            string violationEntryNarrowImageName = ViolationImagesNameFormat.FixedHighwayShoulder(ImageName.Plate(violationImagesNames[i + 1]), ImageName.Day(violationImagesNames[i + 1]), ImageName.Hour(violationImagesNames[i + 1]),
                                ImageName.ImageType(violationImagesNames[i + 1]), ImageName.PlaceNo(violationImagesNames[i + 1]), ImageName.PlaceName(violationImagesNames[i + 1]));
                            string violationEntryWideImageName = ViolationImagesNameFormat.FixedHighwayShoulder(ImageName.Plate(violationImagesNames[i + 2]), ImageName.Day(violationImagesNames[i + 2]), ImageName.Hour(violationImagesNames[i + 2]),
                              ImageName.ImageType(violationImagesNames[i + 2]), ImageName.PlaceNo(violationImagesNames[i + 2]), ImageName.PlaceName(violationImagesNames[i + 2]));
                            string violationExitNarrowImageName = ViolationImagesNameFormat.FixedHighwayShoulder(ImageName.Plate(violationImagesNames[i + 4]), ImageName.Day(violationImagesNames[i + 4]), ImageName.Hour(violationImagesNames[i + 4]),
                               ImageName.ImageType(violationImagesNames[i + 4]), ImageName.PlaceNo(violationImagesNames[i + 4]), ImageName.PlaceName(violationImagesNames[i + 4]));
                            string violationExitWideImageName = ViolationImagesNameFormat.FixedHighwayShoulder(ImageName.Plate(violationImagesNames[i + 5]), ImageName.Day(violationImagesNames[i + 5]), ImageName.Hour(violationImagesNames[i + 5]),
                                 ImageName.ImageType(violationImagesNames[i + 5]), ImageName.PlaceNo(violationImagesNames[i + 5]), ImageName.PlaceName(violationImagesNames[i + 5]));


                            List<string> values = new List<string>();


                            DateTime entryDate = ViolationsDate.StringDateToDateTime(ImageName.Day(entryNarrowImageName), ImageName.Hour(entryNarrowImageName));
                            DateTime exitDate = ViolationsDate.StringDateToDateTime(ImageName.Day(exitNarrowImageName), ImageName.Hour(exitNarrowImageName));

                            TimeSpan result = exitDate.Subtract(entryDate);
                            double distance = double.Parse(settings.m_distance.ToString()) / 1000;
                            double speed = distance / result.TotalHours;

                            values = new List<string>(new string[] { violationPlate, violationEntryDay, violationEntryHour, violationExitDay, violationExitHour, Convert.ToInt32(speed).ToString(),
                                    violationEntryNarrowImageName,
                    violationEntryWideImageName, violationExitNarrowImageName, violationExitWideImageName, settings.m_thumbNailImagesPath });



                            //int insertResultValue = eaevdo.Insert(values);

                            Task<int> taskInsertViolation = eaevdo.AsyncInsert(values);
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

                                //MainForm.m_digitalSignature.SignFile(eaevs.m_violationImagesPath + "\\" + violationEntryNarrowImageName);
                                //MainForm.m_digitalSignature.SignFile(eaevs.m_violationImagesPath + "\\" + violationEntryWideImageName);
                                //MainForm.m_digitalSignature.SignFile(eaevs.m_violationImagesPath + "\\" + violationExitNarrowImageName);
                                //MainForm.m_digitalSignature.SignFile(eaevs.m_violationImagesPath + "\\" + violationExitWideImageName);
                            }

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

                            string violationEntryNarrowImageName = ViolationImagesNameFormat.FixedHighwayShoulder(ImageName.Plate(violationImagesNames[i]), ImageName.Day(violationImagesNames[i]), ImageName.Hour(violationImagesNames[i]),
                                ImageName.ImageType(violationImagesNames[i]), ImageName.PlaceNo(violationImagesNames[i]), ImageName.PlaceName(violationImagesNames[i]));
                            string violationEntryWideImageName = ViolationImagesNameFormat.FixedHighwayShoulder(ImageName.Plate(violationImagesNames[i + 1]), ImageName.Day(violationImagesNames[i + 1]), ImageName.Hour(violationImagesNames[i + 1]),
                              ImageName.ImageType(violationImagesNames[i + 1]), ImageName.PlaceNo(violationImagesNames[i + 1]), ImageName.PlaceName(violationImagesNames[i + 1]));
                            string violationExitNarrowImageName = ViolationImagesNameFormat.FixedHighwayShoulder(ImageName.Plate(violationImagesNames[i + 2]), ImageName.Day(violationImagesNames[i + 2]), ImageName.Hour(violationImagesNames[i + 2]),
                               ImageName.ImageType(violationImagesNames[i + 2]), ImageName.PlaceNo(violationImagesNames[i + 2]), ImageName.PlaceName(violationImagesNames[i + 2]));
                            string violationExitWideImageName = ViolationImagesNameFormat.FixedHighwayShoulder(ImageName.Plate(violationImagesNames[i + 3]), ImageName.Day(violationImagesNames[i + 3]), ImageName.Hour(violationImagesNames[i + 3]),
                                 ImageName.ImageType(violationImagesNames[i + 3]), ImageName.PlaceNo(violationImagesNames[i + 3]), ImageName.PlaceName(violationImagesNames[i + 3]));


                            List<string> values = new List<string>();


                            DateTime entryDate = ViolationsDate.StringDateToDateTime(ImageName.Day(entryNarrowImageName), ImageName.Hour(entryNarrowImageName));
                            DateTime exitDate = ViolationsDate.StringDateToDateTime(ImageName.Day(exitNarrowImageName), ImageName.Hour(exitNarrowImageName));

                            TimeSpan result = exitDate.Subtract(entryDate);
                            double distance = double.Parse(settings.m_distance.ToString()) / 1000;
                            double speed = distance / result.TotalHours;

                            values = new List<string>(new string[] { violationPlate, violationEntryDay, violationEntryHour, violationExitDay, violationExitHour, Convert.ToInt32(speed).ToString(),
                                    violationEntryNarrowImageName,
                    violationEntryWideImageName, violationExitNarrowImageName, violationExitWideImageName, settings.m_thumbNailImagesPath });



                            //int insertResultValue = eaevdo.Insert(values);

                            Task<int> taskInsertViolation = eaevdo.AsyncInsert(values);
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

                                //MainForm.m_digitalSignature.SignFile(eaevs.m_violationImagesPath + "\\" + violationEntryNarrowImageName);
                                //MainForm.m_digitalSignature.SignFile(eaevs.m_violationImagesPath + "\\" + violationEntryWideImageName);
                                //MainForm.m_digitalSignature.SignFile(eaevs.m_violationImagesPath + "\\" + violationExitNarrowImageName);
                                //MainForm.m_digitalSignature.SignFile(eaevs.m_violationImagesPath + "\\" + violationExitWideImageName);
                            }

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
                    List<string> notValidatedImages = new List<string>();

                    List<string> entryInvalid = new List<string>();
                    List<string> exitInvalid = new List<string>(); 

                    entryInvalid.AddRange(CheckImageNameOrList.ValidateImageNameList(itemPlate, wantedImageTypeAndRangeList,
                        Enums.Validate.InValid, entryWantedTypes));
                    exitInvalid.AddRange(CheckImageNameOrList.ValidateImageNameList(itemPlate, wantedImageTypeAndRangeList,
                        Enums.Validate.InValid, exitWantedTypes));
                      
                    foreach (string item in entryInvalid)
                    {
                        if (!notViolationImageNames.Contains(item))
                        {
                            string day = ImageName.Day(item);
                            string hour = ImageName.Hour(item);

                            DateTime dr = ViolationsDate.StringDateToDateTime(day, hour);

                            bool sonuc = ViolationsDate.CikisTarig(dr, Convert.ToDouble(settings.m_distance), Convert.ToDouble(settings.m_speed), Convert.ToDouble(settings.m_tolerancePercentage));

                            if (sonuc)
                                notViolationImageNames.Add(item);
                        }
                    }

                    foreach (string item in exitInvalid)
                    {
                        if (!notViolationImageNames.Contains(item))
                        {
                            string day = ImageName.Day(item);
                            string hour = ImageName.Hour(item);

                            DateTime dr = ViolationsDate.StringDateToDateTime(day, hour);
                            DateTime drlimit = dr.AddMinutes(1);
                              
                            if (DateTime.Now > drlimit)
                                notViolationImageNames.Add(item);
                        }
                    }  

                    List<string> notViolationDate = new List<string>();

                    //bakılacak
                    int speedTolerance = 0;

                    if (settings.m_applyTolerance)
                        speedTolerance = settings.m_tolerancePercentage; 

                    notViolationDate = ViolationsDate.Date(violationDateInDatabase, Enums.Violation.NotViolation, settings.m_protectViolationTime, entryCheckedWorkPlan, exitCheckedWorkPlan,
                        settings.m_distance, settings.m_speed, speedTolerance); 

                    if (notViolationDate.Count > 0)
                    {
                        List<string> notViolationDateImagesNames = new List<string>();

                        List<DateTime> entryDate = new List<DateTime>();
                        List<DateTime> exitDate = new List<DateTime>();

                        foreach (var item in notViolationDate)
                        {
                            if (item.Split(',')[1] == "Entry")
                                entryDate.Add(DateTime.Parse(item.Split(',')[0]));
                            else if (item.Split(',')[1] == "Exit")
                                exitDate.Add(DateTime.Parse(item.Split(',')[0]));

                        }

                        notViolationDateImagesNames.AddRange(ImageName.ViolationImagesNameList(entryDate, entryCheckedWorkPlan));
                        notViolationDateImagesNames.AddRange(ImageName.ViolationImagesNameList(exitDate, exitCheckedWorkPlan));

                        if (notViolationDateImagesNames.Count > 0)
                        {
                            foreach (string item in notViolationDateImagesNames)
                                if (!notViolationImageNames.Contains(item))
                                    notViolationImageNames.Add(item);
                        }
                    }
                    else
                    {
                        if ((entryCheckedWorkPlan.Count == 0) && (exitCheckedWorkPlan.Count > 0))
                            notViolationImageNames.AddRange(exitCheckedWorkPlan);
                    }


                    List<string> outWorkPlan = new List<string>();

                    outWorkPlan.AddRange(ViolationsDate.CheckProgramWorkPlan(validatedImageNameList, workPlan, Enums.WorkPlan.Out));

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
                List<string> notWantedTypeAndRange = new List<string>();
                notWantedTypeAndRange.AddRange(CheckImageNameOrList.FindTypeAndRangeList(imageNameList, Enums.TypeAndRange.NotWanted, 0050, 0299, wantedTypes)); 

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
