using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    class CorridorSpeedWideViolation : ITriggeredViolation
    {
        private static CorridorSpeedWideViolation m_do;
          DataSourceWatcher m_watcher;
          public CorridorSpeedWideViolation()
        {
            m_watcher = new DataSourceWatcher();
            m_watcher.AddWatcher(CorridorSpeedWide.Singleton(MainForm.m_mf));
        }


        public static CorridorSpeedWideViolation Singleton()
        {
            if (m_do == null)
                m_do = new CorridorSpeedWideViolation();

            return m_do;
        } 



        public void Violation(List<string> violationImageNames)
        {
            Settings.CorridorSpeedWideSettings eaevs = Settings.CorridorSpeedWideSettings.Singleton();
            DatabaseOperation.CorridorSpeedWide eaevdo = DatabaseOperation.CorridorSpeedWide.Singleton();
            Settings.WorkPlanAndMainSettings m_wpams = Settings.WorkPlanAndMainSettings.Singleton();

            eaevs = eaevs.DeSerialize(eaevs);
            m_wpams = m_wpams.DeSerialize(m_wpams);

            List<string> workPlan = m_wpams.SpeedCorridorWideWorkingPlan; 

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

                    string entryImageType = ImageName.ImageType(entryImageName);
                    string exitImageType = ImageName.ImageType(exitImageName);

                    DateTime entryDate = ViolationsDate.StringDateToDateTime(ImageName.Day(entryImageName), ImageName.Hour(entryImageName));
                    DateTime exitDate = ViolationsDate.StringDateToDateTime(ImageName.Day(exitImageName), ImageName.Hour(exitImageName));

                    string entryNarrowImage = "", entryWideImage = "", exitNarrowImage = "", exitWideImage = "";

                    if (entryImageType == "L1-C0")
                    {
                        entryWideImage = entryImageName;

                        //resimler klasöründeki resimleri alıyoruz
                        entryNarrowImage = FileOperation.Find(eaevs.m_imagePath).Find(x => ImageName.Plate(x) == violationPlate && ImageName.ImageType(x) != "L1-C0" &&
                            ViolationsDate.StringDateToDateTime(ImageName.Day(x), ImageName.Hour(x)) == entryDate);

                    }
                    else if (entryImageType != "L1-C0")
                    {
                        entryNarrowImage = entryImageName;

                        //resimler klasöründeki resimleri alıyoruz
                        entryWideImage = FileOperation.Find(eaevs.m_imagePath).Find(x => ImageName.Plate(x) == violationPlate && ImageName.ImageType(x) == "L1-C0" &&
                            ViolationsDate.StringDateToDateTime(ImageName.Day(x), ImageName.Hour(x)) == entryDate);
                    }

                    if (exitImageType == "L2-C0")
                    {
                        exitWideImage = exitImageName;

                        //resimler klasöründeki resimleri alıyoruz
                        exitNarrowImage = FileOperation.Find(eaevs.m_imagePath).Find(x => ImageName.Plate(x) == violationPlate && ImageName.ImageType(x) != "L2-C0" && ViolationsDate.StringDateToDateTime(ImageName.Day(x), ImageName.Hour(x)) == exitDate);
                    }
                    else if (exitImageType != "L2-C0")
                    {
                        exitNarrowImage = exitImageName;
                        //resimler klasöründeki resimleri alıyoruz
                        exitWideImage = FileOperation.Find(eaevs.m_imagePath).Find(x => ImageName.Plate(x) == violationPlate && ImageName.ImageType(x) == "L2-C0" && ViolationsDate.StringDateToDateTime(ImageName.Day(x), ImageName.Hour(x)) == exitDate);
                    }


                    if (!string.IsNullOrEmpty(entryWideImage) && !string.IsNullOrEmpty(entryNarrowImage) && !string.IsNullOrEmpty(exitWideImage) && !string.IsNullOrEmpty(exitNarrowImage))
                    {

                        TimeSpan result = exitDate.Subtract(entryDate);
                        double distance = double.Parse(eaevs.m_distance.ToString()) / 1000;
                        double speed = distance / result.TotalHours;


                        string violationEntryNarrowImageName = ViolationImagesNameFormat.SpeedCorridor(ImageName.Plate(entryNarrowImage), ImageName.Day(entryNarrowImage), ImageName.Hour(entryNarrowImage),
                           eaevs.m_speed, Convert.ToInt32(speed), ImageName.ImageType(entryNarrowImage), ImageName.PlaceNo(entryNarrowImage), ImageName.PlaceName(entryNarrowImage));

                        string violationEntryWideImageName = ViolationImagesNameFormat.SpeedCorridor(ImageName.Plate(entryWideImage), ImageName.Day(entryWideImage), ImageName.Hour(entryWideImage),
                                                 eaevs.m_speed, Convert.ToInt32(speed), ImageName.ImageType(entryWideImage), ImageName.PlaceNo(entryWideImage), ImageName.PlaceName(entryWideImage));



                        string violationExitNarrowImageName = ViolationImagesNameFormat.SpeedCorridor(ImageName.Plate(exitNarrowImage), ImageName.Day(exitNarrowImage), ImageName.Hour(exitNarrowImage),
                         eaevs.m_speed, Convert.ToInt32(speed), ImageName.ImageType(exitNarrowImage), ImageName.PlaceNo(exitNarrowImage), ImageName.PlaceName(exitNarrowImage));

                        string violationExitWideImageName = ViolationImagesNameFormat.SpeedCorridor(ImageName.Plate(exitWideImage), ImageName.Day(exitWideImage), ImageName.Hour(exitWideImage),
                     eaevs.m_speed, Convert.ToInt32(speed), ImageName.ImageType(exitWideImage), ImageName.PlaceNo(exitWideImage), ImageName.PlaceName(exitWideImage));



                        List<string> values = new List<string>(new string[] { violationPlate, violationEntryDay, violationEntryHour, violationExitDay, violationExitHour,eaevs.m_speed.ToString(), eaevs.m_tolerancePercentage.ToString(), 
                            Convert.ToInt32(speed).ToString(), violationEntryNarrowImageName, violationEntryWideImageName, violationExitNarrowImageName, violationExitWideImageName,  eaevs.m_thumbNailImagesPath });


                        Task<int> taskInsertViolation = eaevdo.AsyncInsert(values);
                        taskInsertViolation.Wait();

                        if (taskInsertViolation.Result > 0)
                        {
                            if (eaevs.m_showLaneInWideImage)
                            {
                                //CalcCoordinate.SetLane(eaevs.m_imagePath, entryNarrowImage, entryWideImage, eaevs.m_violationImagesPath, violationEntryWideImageName);
                                //CalcCoordinate.SetLane(eaevs.m_imagePath, exitNarrowImage, exitWideImage, eaevs.m_violationImagesPath, violationExitWideImageName);

                                ////resimler ihlal resimleri klasörüne
                                FileOperation.Move(eaevs.m_imagePath, new List<string>(new string[] { entryNarrowImage, exitNarrowImage }), eaevs.m_violationImagesPath,
                                    new List<string>(new string[] { violationEntryNarrowImageName, violationExitNarrowImageName }));

                            }
                            else
                            {
                                //resimler ihlal resimleri klasörüne
                                FileOperation.Move(eaevs.m_imagePath, new List<string>(new string[] { entryNarrowImage, entryWideImage, exitNarrowImage, exitWideImage }), eaevs.m_violationImagesPath,
                                    new List<string>(new string[] { violationEntryNarrowImageName, violationEntryWideImageName, violationExitNarrowImageName, violationExitWideImageName }));
                            }

                            FileOperation.Copy(eaevs.m_violationImagesPath, new List<string>(new string[] { violationEntryNarrowImageName, violationEntryWideImageName, violationExitNarrowImageName, violationExitWideImageName }), eaevs.m_thumbNailImagesPath,
                         new List<string>(new string[] { violationEntryNarrowImageName, violationEntryWideImageName, violationExitNarrowImageName, violationExitWideImageName }));

                            m_watcher.SyncFileCreated(values); 
                            //MainForm.m_digitalSignature.SignFile(eaevs.m_violationImagesPath + "\\" + violationEntryNarrowImageName);
                            //MainForm.m_digitalSignature.SignFile(eaevs.m_violationImagesPath + "\\" + violationExitNarrowImageName);


                            //ImageSize.ReSizeImageInListAsync(eaevs.m_imagePath, new List<string>(new string[] { entryImageName, entrySecondImage, exitImageName, exitSecondImage }),
                            //    eaevs.m_thumbNailImagesPath, new List<string>(new string[] { violationEntryNarrowImageName, violationEntryImageName, violationExitNarrowImageName, violationExitImageName }), new Size(160, 120));



                            //küçük resimler için resmi boyutlandırıp tekrar kaydediyoruz.
                            //ImageSize.ReSizeImageInListAsync(eaevs.m_violationImagesPath, new List<string>(new string[] { violationEntryNarrowImageName, violationEntryImageName, violationExitNarrowImageName, violationExitImageName }),
                            //    eaevs.m_thumbNailImagesPath, new List<string>(new string[] { violationEntryNarrowImageName, violationEntryImageName, violationExitNarrowImageName, violationExitImageName }), new Size(160, 120));
                        }
                    }
                }

                checkedWorkPlan.Clear();
            }


        }
    }
}
