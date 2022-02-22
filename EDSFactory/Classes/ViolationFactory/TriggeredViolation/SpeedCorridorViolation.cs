using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    class CorridorSpeedViolation :  ITriggeredViolation
    {
        private static CorridorSpeedViolation m_do;
        DataSourceWatcher m_watcher;
        public CorridorSpeedViolation()
        {
            m_watcher = new DataSourceWatcher();
            m_watcher.AddWatcher(CorridorSpeed.Singleton(MainForm.m_mf));
        }

        public static CorridorSpeedViolation Singleton()
        {
            if (m_do == null)
                m_do = new CorridorSpeedViolation();

            return m_do;
        } 


        public void Violation(List<string> violationImageNames)
        {
            Settings.CorridorSpeedSettings eaevs = Settings.CorridorSpeedSettings.Singleton();
            DatabaseOperation.CorridorSpeed eaevdo = DatabaseOperation.CorridorSpeed.Singleton();
            Settings.WorkPlanAndMainSettings m_wpams = Settings.WorkPlanAndMainSettings.Singleton();
            DatabaseOperation.NTP ntpDatabase = DatabaseOperation.NTP.Singleton();

            eaevs = eaevs.DeSerialize(eaevs);
            m_wpams = m_wpams.DeSerialize(m_wpams);

            List<string> workPlan = m_wpams.SpeedCorridorWorkingPlan;  

            List<string> checkedWorkPlan = ViolationsDate.CheckProgramWorkPlan(violationImageNames, workPlan, Enums.WorkPlan.In); 


            if (checkedWorkPlan.Count > 0)//veritabanına kaydediyoruz
            {
                //ihlal resimlerini sıralıyoruz
                checkedWorkPlan.Sort();

                //ihlal resimlerini veri tabanına kaydediyoruz
                for (int i = 0; i < checkedWorkPlan.Count; i += 2)
                {
                    string violationPlate = ImageName.Plate(checkedWorkPlan[i]);
                    string violationEntryDay = StringFormatOperation.Date(ImageName.Day(checkedWorkPlan[i]));
                    string violationEntryHour = StringFormatOperation.Hour(ImageName.Hour(checkedWorkPlan[i]));
                    string violationExitDay = StringFormatOperation.Date(ImageName.Day(checkedWorkPlan[i + 1]));
                    string violationExitHour = StringFormatOperation.Hour(ImageName.Hour(checkedWorkPlan[i + 1]));

                    string entryImageName = ImageName.Name(checkedWorkPlan[i]);
                    string exitImageName = ImageName.Name(checkedWorkPlan[i + 1]); 

                    DateTime entryDate = ViolationsDate.StringDateToDateTime(ImageName.Day(entryImageName), ImageName.Hour(entryImageName));
                    DateTime exitDate = ViolationsDate.StringDateToDateTime(ImageName.Day(exitImageName), ImageName.Hour(exitImageName));

                    if (!string.IsNullOrEmpty(entryImageName) && !string.IsNullOrEmpty(exitImageName))
                    {

                        TimeSpan result = exitDate.Subtract(entryDate);
                        double distance = double.Parse(eaevs.m_distance.ToString()) / 1000;
                        double speed = distance / result.TotalHours;


                        string violationEntryNarrowImageName = ViolationImagesNameFormat.SpeedCorridor(ImageName.Plate(entryImageName), ImageName.Day(entryImageName), ImageName.Hour(entryImageName),
                           eaevs.m_speed, Convert.ToInt32(speed), ImageName.ImageType(entryImageName), ImageName.PlaceNo(entryImageName), ImageName.PlaceName(entryImageName));

                        string violationExitNarrowImageName = ViolationImagesNameFormat.SpeedCorridor(ImageName.Plate(exitImageName), ImageName.Day(exitImageName), ImageName.Hour(exitImageName),
                         eaevs.m_speed, Convert.ToInt32(speed), ImageName.ImageType(exitImageName), ImageName.PlaceNo(exitImageName), ImageName.PlaceName(exitImageName));
 
                        List<string> values = new List<string>(new string[] { violationPlate, violationEntryDay, violationEntryHour, violationExitDay, violationExitHour,eaevs.m_speed.ToString(), eaevs.m_tolerancePercentage.ToString(), 
                            Convert.ToInt32(speed).ToString(), violationEntryNarrowImageName, violationExitNarrowImageName,  eaevs.m_thumbNailImagesPath });

                     
                        Task<int> taskInsertViolation = eaevdo.AsyncInsert(values);
                        taskInsertViolation.Wait();

                        if (taskInsertViolation.Result > 0)
                        {
                            FileOperation.Copy(eaevs.m_imagePath + "\\" + "sync"  , new List<string>(new string[] { entryImageName, exitImageName }), eaevs.m_thumbNailImagesPath,
                       new List<string>(new string[] { violationEntryNarrowImageName, violationExitNarrowImageName }));


                                ////resimler ihlal resimleri klasörüne
                            FileOperation.Move(eaevs.m_imagePath + "\\" + "sync", new List<string>(new string[] { entryImageName, exitImageName }), eaevs.m_violationImagesPath,
                                    new List<string>(new string[] { violationEntryNarrowImageName, violationExitNarrowImageName })); 

                          
                            m_watcher.SyncFileCreated(values); 
                            //MainForm.m_digitalSignature.SignFile(eaevs.m_violationImagesPath + "\\" + violationEntryNarrowImageName);
                            //MainForm.m_digitalSignature.SignFile(eaevs.m_violationImagesPath + "\\" + violationExitNarrowImageName);
                         }

                        //Task taskvi = ntpDatabase.AsyncDelete(entryImageName);
                        //Task taskvi1 = ntpDatabase.AsyncDelete(exitImageName);

                        //Task.WaitAll(taskvi, taskvi1);

                    }
                }

                checkedWorkPlan.Clear();
            }


        }
    }
}
