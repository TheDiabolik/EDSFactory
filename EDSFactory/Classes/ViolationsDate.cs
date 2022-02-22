using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EDSFactory
{
    class ViolationsDate
    {
        public static List<DateTime> Date(List<DateTime> violationDateInDatabase, Enums.Violation v, int protectViolationTime, List<string> validatedImageNameList)
        {
            //listedeki tarihler
            List<DateTime> violationDateInList = ImageName.ImagesDate(validatedImageNameList);

            //ihlal saatlerini kontrol ediyoruz, veri tabanında var
            List<DateTime> time = CheckImageNameOrList.CheckViolationTime(violationDateInDatabase, violationDateInList, v, protectViolationTime);

            return time;
        }

        public static List<DateTime> Date(List<DateTime> violationDateInDatabase, Enums.Violation v, int protectViolationTime, List<string> validatedImageNameList, TimeSpan minViolationTime, TimeSpan maxViolationTime)
        {
            List<DateTime> checkedTime = new List<DateTime>();

            //ihlal oluşturanlaran zamanlardan ihlal olmayan zamanları bulmak için 
            //ihlal olan giriş çıkış zamanları(listeden)//giriş çıkış ihlal zamanı olmayanlar
            List<string> entranceAndExitViolationDates = ViolationsDate.EntranceExitViolationDates(ImageName.ImagesDate(validatedImageNameList), minViolationTime, maxViolationTime, Enums.Violation.Violation);

            if (v == Enums.Violation.Violation)
            {
                //ihlal oluşturan zamanların çıkış zamanlarını alıyoruz
                List<DateTime> toBeCheckedExitViolationDates = ViolationsDate.FindDate(entranceAndExitViolationDates, Enums.FindDate.Exit);
                //ihlal saatlerini kontrol ediyoruz, veri tabanında var
                List<DateTime> checkedExitViolationDates = CheckImageNameOrList.CheckViolationTime(violationDateInDatabase, toBeCheckedExitViolationDates, v, protectViolationTime);
                //ihlal oluşturan ve zamanları kontrol edilmiş ihlal zamanları
                List<string> checkedEntranceAndExitViolationDates = ViolationsDate.FindDate(checkedExitViolationDates, entranceAndExitViolationDates, Enums.FindDate.Exit);

                List<DateTime> checkedEnterance = ViolationsDate.FindDate(checkedEntranceAndExitViolationDates, Enums.FindDate.Enterance);

                checkedTime.AddRange(checkedEnterance);
                checkedTime.AddRange(checkedExitViolationDates);
            }
            else if (v == Enums.Violation.NotViolation)
            {
                //burası giriş çıkış ihlali oluşturmayan zamanlar
                List<string> notViolationDates = ViolationsDate.EntranceExitViolationDates(ImageName.ImagesDate(validatedImageNameList), minViolationTime, maxViolationTime, Enums.Violation.NotViolation);

                //ihlal oluşturan zamanların çıkış zamanlarını alıyoruz
                List<DateTime> toBeCheckedExitViolationDates = ViolationsDate.FindDate(entranceAndExitViolationDates, Enums.FindDate.Exit);
                //ihlal saatlerini kontrol ediyoruz, veri tabanında var
                List<DateTime> checkedExitViolationDates = CheckImageNameOrList.CheckViolationTime(violationDateInDatabase, toBeCheckedExitViolationDates, v, protectViolationTime);
                //ihlal oluşturan ve zamanları kontrol edilmiş ihlal zamanları
                List<string> checkedEntranceAndExitViolationDates = ViolationsDate.FindDate(checkedExitViolationDates, entranceAndExitViolationDates, Enums.FindDate.Exit);
                List<DateTime> checkedEnterance = ViolationsDate.FindDate(checkedEntranceAndExitViolationDates, Enums.FindDate.Enterance);

                checkedTime.AddRange(notViolationDates.ConvertAll(x => DateTime.Parse(x)));
                checkedTime.AddRange(checkedEnterance);
                checkedTime.AddRange(checkedExitViolationDates);
            }

            checkedTime.Sort();

            return checkedTime;
        }

        public static List<string> Date(List<DateTime> violationDateInDatabase, Enums.Violation v, int protectViolationTime, List<string> entryValidatedImageNameList, List<string> exitValidatedImageNameList, double distance, double speed, int tolerance)
        {
            List<string> checkedTime = new List<string>();

            //ihlal oluşturanlaran zamanlardan ihlal olmayan zamanları bulmak için 
            //ihlal olan giriş çıkış zamanları(listeden)//giriş çıkış ihlal zamanı olmayanlar
            List<string> entranceAndExitViolationDates = ViolationsDate.EntranceExitViolationDates(ImageName.ImagesDate(entryValidatedImageNameList), ImageName.ImagesDate(exitValidatedImageNameList),
                   Enums.Violation.Violation, distance, speed, tolerance).ConvertAll(x => (string)x);

            if (v == Enums.Violation.Violation)
            {
                //ihlal oluşturan zamanların çıkış zamanlarını alıyoruz
                List<DateTime> toBeCheckedExitViolationDates = ViolationsDate.FindDate(entranceAndExitViolationDates, Enums.FindDate.Exit);
                //ihlal saatlerini kontrol ediyoruz, veri tabanında var
                List<DateTime> checkedExitViolationDates = CheckImageNameOrList.CheckViolationTime(violationDateInDatabase, toBeCheckedExitViolationDates, v, protectViolationTime);
                //ihlal oluşturan ve zamanları kontrol edilmiş ihlal zamanları
                List<string> checkedEntranceAndExitViolationDates = ViolationsDate.FindDate(checkedExitViolationDates, entranceAndExitViolationDates, Enums.FindDate.Exit);

                List<DateTime> checkedEnterance = ViolationsDate.FindDate(checkedEntranceAndExitViolationDates, Enums.FindDate.Enterance);

                foreach (DateTime item in checkedEnterance)
                    checkedTime.Add(item.ToString()+",Entry");

                foreach (DateTime item in checkedExitViolationDates)
                    checkedTime.Add(item.ToString() + ",Exit");

                //checkedTime..AddRange(checkedEnterance);
                //checkedTime.AddRange(checkedExitViolationDates);
            }
            else if (v == Enums.Violation.NotViolation)
            {
                //burası giriş çıkış ihlali oluşturmayan zamanlar
                List<string> notViolationDates = ViolationsDate.EntranceExitViolationDates(ImageName.ImagesDate(entryValidatedImageNameList), ImageName.ImagesDate(exitValidatedImageNameList),
                    Enums.Violation.NotViolation, distance, speed, tolerance);

                //ihlal oluşturan zamanların çıkış zamanlarını alıyoruz
                List<DateTime> toBeCheckedExitViolationDates = ViolationsDate.FindDate(entranceAndExitViolationDates, Enums.FindDate.Exit);
                //ihlal saatlerini kontrol ediyoruz, veri tabanında var
                List<DateTime> checkedExitViolationDates = CheckImageNameOrList.CheckViolationTime(violationDateInDatabase, toBeCheckedExitViolationDates, v, protectViolationTime);
                //ihlal oluşturan ve zamanları kontrol edilmiş ihlal zamanları
                List<string> checkedEntranceAndExitViolationDates = ViolationsDate.FindDate(checkedExitViolationDates, entranceAndExitViolationDates, Enums.FindDate.Exit);
                List<DateTime> checkedEnterance = ViolationsDate.FindDate(checkedEntranceAndExitViolationDates, Enums.FindDate.Enterance);

                checkedTime.AddRange(notViolationDates);

                foreach (DateTime item in checkedEnterance)
                    checkedTime.Add(item.ToString() + ",Entry");

                foreach (DateTime item in checkedExitViolationDates)
                    checkedTime.Add(item.ToString() + ",Exit");
                //checkedTime.AddRange(checkedEnterance);
                //checkedTime.AddRange(checkedExitViolationDates);
            }

            //checkedTime.Keys.s.Sort();

            return checkedTime;
        }




        //public static List<string> Date(List<DateTime> violationDateInDatabase, Enums.Violation v, int protectViolationTime, List<string> entryValidatedImageNameList, List<string> exitValidatedImageNameList, TimeSpan minViolationTime, TimeSpan maxViolationTime)
        //{
        //    List<string> checkedTime = new List<string>();

        //    //ihlal oluşturanlaran zamanlardan ihlal olmayan zamanları bulmak için 
        //    //ihlal olan giriş çıkış zamanları(listeden)//giriş çıkış ihlal zamanı olmayanlar
        //    List<string> entranceAndExitViolationDates = ViolationsDate.EntranceExitViolationDates(ImageName.ImagesDate(entryValidatedImageNameList), ImageName.ImagesDate(exitValidatedImageNameList),
        //            minViolationTime, maxViolationTime, Enums.Violation.Violation).ConvertAll(x => (string)x);

        //    if (v == Enums.Violation.Violation)
        //    {
        //        //ihlal oluşturan zamanların çıkış zamanlarını alıyoruz
        //        List<DateTime> toBeCheckedExitViolationDates = ViolationsDate.FindDate(entranceAndExitViolationDates, Enums.FindDate.Exit);
        //        //ihlal saatlerini kontrol ediyoruz, veri tabanında var
        //        List<DateTime> checkedExitViolationDates = CheckImageNameOrList.CheckViolationTime(violationDateInDatabase, toBeCheckedExitViolationDates, v, protectViolationTime);
        //        //ihlal oluşturan ve zamanları kontrol edilmiş ihlal zamanları
        //        List<string> checkedEntranceAndExitViolationDates = ViolationsDate.FindDate(checkedExitViolationDates, entranceAndExitViolationDates, Enums.FindDate.Exit);

        //        List<DateTime> checkedEnterance = ViolationsDate.FindDate(checkedEntranceAndExitViolationDates, Enums.FindDate.Enterance);

        //        foreach (DateTime item in checkedEnterance)
        //            checkedTime.Add(item.ToString() + ",Entry");

        //        foreach (DateTime item in checkedExitViolationDates)
        //            checkedTime.Add(item.ToString() + ",Exit");

        //        //checkedTime..AddRange(checkedEnterance);
        //        //checkedTime.AddRange(checkedExitViolationDates);
        //    }
        //    else if (v == Enums.Violation.NotViolation)
        //    {
        //        //burası giriş çıkış ihlali oluşturmayan zamanlar
        //        List<string> notViolationDates = ViolationsDate.EntranceExitViolationDates(ImageName.ImagesDate(entryValidatedImageNameList), ImageName.ImagesDate(exitValidatedImageNameList), minViolationTime, maxViolationTime, Enums.Violation.NotViolation);

        //        //ihlal oluşturan zamanların çıkış zamanlarını alıyoruz
        //        List<DateTime> toBeCheckedExitViolationDates = ViolationsDate.FindDate(entranceAndExitViolationDates, Enums.FindDate.Exit);
        //        //ihlal saatlerini kontrol ediyoruz, veri tabanında var
        //        List<DateTime> checkedExitViolationDates = CheckImageNameOrList.CheckViolationTime(violationDateInDatabase, toBeCheckedExitViolationDates, v, protectViolationTime);
        //        //ihlal oluşturan ve zamanları kontrol edilmiş ihlal zamanları
        //        List<string> checkedEntranceAndExitViolationDates = ViolationsDate.FindDate(checkedExitViolationDates, entranceAndExitViolationDates, Enums.FindDate.Exit);
        //        List<DateTime> checkedEnterance = ViolationsDate.FindDate(checkedEntranceAndExitViolationDates, Enums.FindDate.Enterance);

        //        checkedTime.AddRange(notViolationDates);

        //        foreach (DateTime item in checkedEnterance)
        //            checkedTime.Add(item.ToString() + ",Entry");

        //        foreach (DateTime item in checkedExitViolationDates)
        //            checkedTime.Add(item.ToString() + ",Exit");
        //        //checkedTime.AddRange(checkedEnterance);
        //        //checkedTime.AddRange(checkedExitViolationDates);
        //    }

        //    //checkedTime.Keys.s.Sort();

        //    return checkedTime;
        //}
















        public static List<DateTime> Date(List<DateTime> violationDateInDatabase, Enums.Violation v, int protectViolationTime, List<string> validatedImageNameList, TimeSpan minViolationTime, TimeSpan maxViolationTime, params string[] imageType)
        {
            List<DateTime> checkedTime = new List<DateTime>();


            List<string> entry = validatedImageNameList.FindAll(x => (ImageName.ImageType(x) == imageType[0]) || (ImageName.ImageType(x) == imageType[1]));
            List<string> exit = validatedImageNameList.FindAll(x => (ImageName.ImageType(x) == imageType[2]) || (ImageName.ImageType(x) == imageType[3]));

            //ihlal oluşturanlaran zamanlardan ihlal olmayan zamanları bulmak için 
            //ihlal olan giriş çıkış zamanları(listeden)//giriş çıkış ihlal zamanı olmayanlar
            List<string> entranceAndExitViolationDates = ViolationsDate.EntranceExitViolationDates(ImageName.ImagesDate(entry), ImageName.ImagesDate(exit), minViolationTime, maxViolationTime, Enums.Violation.Violation).ConvertAll(x => (string)x);

            if (v == Enums.Violation.Violation)
            {
                //ihlal oluşturan zamanların çıkış zamanlarını alıyoruz
                List<DateTime> toBeCheckedExitViolationDates = ViolationsDate.FindDate(entranceAndExitViolationDates, Enums.FindDate.Exit);
                //ihlal saatlerini kontrol ediyoruz, veri tabanında var
                List<DateTime> checkedExitViolationDates = CheckImageNameOrList.CheckViolationTime(violationDateInDatabase, toBeCheckedExitViolationDates, v, protectViolationTime);
                //ihlal oluşturan ve zamanları kontrol edilmiş ihlal zamanları
                List<string> checkedEntranceAndExitViolationDates = ViolationsDate.FindDate(checkedExitViolationDates, entranceAndExitViolationDates, Enums.FindDate.Exit);

                List<DateTime> checkedEnterance = ViolationsDate.FindDate(checkedEntranceAndExitViolationDates, Enums.FindDate.Enterance);

                checkedTime.AddRange(checkedEnterance);
                checkedTime.AddRange(checkedExitViolationDates);
            }
            else if (v == Enums.Violation.NotViolation)
            {
                //burası giriş çıkış ihlali oluşturmayan zamanlar
                List<string> notViolationDates = ViolationsDate.EntranceExitViolationDates(ImageName.ImagesDate(entry), ImageName.ImagesDate(exit), minViolationTime, maxViolationTime, Enums.Violation.NotViolation).ConvertAll(x => (string)x);

                //ihlal oluşturan zamanların çıkış zamanlarını alıyoruz
                List<DateTime> toBeCheckedExitViolationDates = ViolationsDate.FindDate(entranceAndExitViolationDates, Enums.FindDate.Exit);
                //ihlal saatlerini kontrol ediyoruz, veri tabanında var
                List<DateTime> checkedExitViolationDates = CheckImageNameOrList.CheckViolationTime(violationDateInDatabase, toBeCheckedExitViolationDates, v, protectViolationTime);
                //ihlal oluşturan ve zamanları kontrol edilmiş ihlal zamanları
                List<string> checkedEntranceAndExitViolationDates = ViolationsDate.FindDate(checkedExitViolationDates, entranceAndExitViolationDates, Enums.FindDate.Exit);
                List<DateTime> checkedEnterance = ViolationsDate.FindDate(checkedEntranceAndExitViolationDates, Enums.FindDate.Enterance);

                checkedTime.AddRange(notViolationDates.ConvertAll(x => DateTime.Parse(x)));
                checkedTime.AddRange(checkedEnterance);
                checkedTime.AddRange(checkedExitViolationDates);
            }

            checkedTime.Sort();

            return checkedTime;
        }


        public static bool CikisTarig(DateTime dt, double distance, double speed, double tolerance)
        {
            double speedTolerance = (speed / 100) *  tolerance ;
            double speedWithTolerance = speed + speedTolerance + 1;

            double hour = ((distance / 1000) / speedWithTolerance);
            double minute = hour * 60;

            DateTime exitDatesLimit = dt.AddMinutes(minute);

            if (DateTime.Now > exitDatesLimit)
                return true;
            else
                return false;
        }

        public static bool GirisTarig(DateTime dt, double distance, double speed, double tolerance)
        {
            double speedTolerance = (speed / 100) * tolerance;
            double speedWithTolerance = speed + speedTolerance + 1;

            double hour = ((distance / 1000) / speedWithTolerance);
            double minute = hour * 60;

            DateTime entryDatesLimit = dt.AddMinutes(-minute);

            if (DateTime.Now > entryDatesLimit)
                return true;
            else
                return false;
        }

        public static List<string>
            EntranceExitViolationDates(List<DateTime> enrtyDates, List<DateTime> exitDates, Enums.Violation v, double distance, double speed, int tolerance)
        {

        

            List<DateTime> enrtyDatesInList = new List<DateTime>(enrtyDates);
            List<DateTime> exitDatesInList = new List<DateTime>(exitDates);

            List<string> notViolationDate = new List<string>();
            List<string> violationDate = new List<string>();

            List<string> returnDate = new List<string>();

            double speedTolerance = (speed / 100) * Convert.ToDouble(tolerance);
            double speedWithTolerance = speed + speedTolerance + 1;

            double hour = ((distance / 1000) / speedWithTolerance);
            double minute = hour * 60;


            foreach (DateTime item in exitDatesInList)
            {
                DateTime entryDatesLimit = item.AddMinutes(-minute);

                DateTime violationDates = enrtyDatesInList.Find(x => (x < item) && (x > entryDatesLimit));

                if (violationDates.Date != new DateTime(0001, 01, 01, 00, 00, 00))
                {
                    violationDate.Add(violationDates.ToString() + "-" + item.ToString());
                    enrtyDatesInList.Remove(violationDates);
                }

            } 

            //foreach (DateTime item in enrtyDatesInList)
            //{
            //    DateTime exitDatesLimit = item.AddMinutes(minute);

            //    DateTime violationDates = exitDatesInList.Find(x => x < exitDatesLimit);

            //    if (violationDates.Date != new DateTime(0001, 01, 01, 00, 00, 00))
            //    {
            //        violationDate.Add(item.ToString() + "-" + violationDates.ToString());
            //        exitDatesInList.Remove(violationDates);
            //    }
             
            //}

            if (v == Enums.Violation.Violation)
            {
                returnDate = violationDate;
            }

            else if(v == Enums.Violation.NotViolation)
            {
                foreach (string item in violationDate)
                {
                    string entryDate = item.Split('-')[0];
                    enrtyDatesInList.Remove(DateTime.Parse(entryDate));

                    string exitDate = item.Split('-')[1];
                    exitDatesInList.Remove(DateTime.Parse(exitDate));
                }

                foreach (DateTime item in enrtyDatesInList)
                {
                    DateTime exitDatesLimit = item.AddMinutes(minute);

                    if(DateTime.Now > exitDatesLimit)
                        notViolationDate.Add(item.ToString() + ",Entry"); 
                }


                notViolationDate.AddRange(exitDatesInList.ConvertAll(x => x.ToString() + ",Exit"));

                returnDate = notViolationDate;
            }


            return returnDate;
        }


        public static List<string> 
            EntranceExitViolationDates(List<DateTime> imagesDate, TimeSpan minViolationTime, TimeSpan maxViolationTime, Enums.Violation v)
        {
            try
            {
                List<DateTime> violationDatesInList = new List<DateTime>(imagesDate);

                List<DateTime> temp = new List<DateTime>(imagesDate);
               
                List<string> violationDate = new List<string>();
                List<DateTime> notviolationtime = new List<DateTime>();

                violationDatesInList.Reverse();

                for (int i = violationDatesInList.Count - 1; i >= 0; i--)
                {
                    DateTime entryTime = violationDatesInList[i];
                    DateTime findMinExitTime = FindDate(entryTime, minViolationTime, Enums.Date.Add);
                    DateTime findMaxExitTime = FindDate(entryTime, maxViolationTime, Enums.Date.Add);

                    DateTime exitTime = temp.Find(elements => (elements != entryTime) && (elements < findMaxExitTime) && (elements > findMinExitTime));

                    TimeSpan t = DateTime.Now.Subtract(entryTime);

                    if (exitTime.Date != new DateTime(0001, 01, 01, 00, 00, 00))
                    {
                        violationDate.Add(entryTime.ToString() + "-" + exitTime.ToString());
                        violationDatesInList.Remove(entryTime);
                        violationDatesInList.Remove(exitTime);
                        temp.Remove(entryTime);
                        temp.Remove(exitTime);
                        i = violationDatesInList.Count;
                    }
                    else if ((exitTime.Date == new DateTime(0001, 01, 01, 00, 00, 00)) && (DateTime.Now.Subtract(entryTime) > maxViolationTime))
                    {
                        notviolationtime.Add(entryTime);
                        violationDatesInList.Remove(entryTime);
                        temp.Remove(entryTime);
                        i = violationDatesInList.Count;
                    }
                }

                if (v == Enums.Violation.Violation)//imagedate
                {
                    violationDate.Sort();
                    return violationDate;
                }
                else//ihlal değilse
                {
                    List<string> notViolation = notviolationtime.ConvertAll(x => x.ToString());
                    return notViolation;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.FindEntranceExitViolationDatesExceptionMessage, ex);
            }
        }



        public static List<string>
         EntranceExitViolationDates (List<DateTime> entryDate, List<DateTime> exitDate, TimeSpan minViolationTime, TimeSpan maxViolationTime, Enums.Violation v)
        {
            try
            {
                List<DateTime> entryDatesInList = new List<DateTime>(entryDate);
                List<DateTime> exitDatesInList = new List<DateTime>(exitDate);

                List<DateTime> tempEntryDates = new List<DateTime>(entryDate);
                List<DateTime> tempExitDates = new List<DateTime>(exitDate);

                List<string> violationDate = new List<string>();
                List<string> notviolationtime = new List<string>();

                entryDatesInList.Reverse();
                
                for (int i = entryDatesInList.Count - 1; i >= 0; i--)
                {
                    DateTime entryTime = entryDatesInList[i];
                    DateTime findMinExitTime = FindDate(entryTime, minViolationTime, Enums.Date.Add);
                    DateTime findMaxExitTime = FindDate(entryTime, maxViolationTime, Enums.Date.Add);

                    DateTime exitTime = tempExitDates.Find(elements => (elements != entryTime) && (elements < findMaxExitTime) && (elements > findMinExitTime));

                    TimeSpan t = DateTime.Now.Subtract(entryTime);


                    if (exitTime.Date != new DateTime(0001, 01, 01, 00, 00, 00))
                    {
                        violationDate.Add(entryTime.ToString() + "-" + exitTime.ToString());
                        entryDatesInList.Remove(entryTime);
                        exitDatesInList.Remove(exitTime);
                        tempEntryDates.Remove(entryTime);
                        tempExitDates.Remove(exitTime);
                        i = entryDatesInList.Count;
                    }
                    else if ((exitTime.Date == new DateTime(0001, 01, 01, 00, 00, 00)) && (DateTime.Now.Subtract(entryTime) > maxViolationTime))
                    {
                        notviolationtime.Add(entryTime+",Entry");
                        entryDatesInList.Remove(entryTime);
                        tempEntryDates.Remove(entryTime);
                        i = entryDatesInList.Count;

                        if(i == 0)
                        {
                            foreach (var item in tempExitDates)
                            {
                                notviolationtime.Add(item + ",Exit");
                            }
                           
                        }

                    }

                }
                if (v == Enums.Violation.Violation)//imagedate
                {
                    violationDate.Sort();
                    return violationDate;
                }
                else//ihlal değilse
                { 
                    return notviolationtime;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.FindEntranceExitViolationDatesExceptionMessage, ex);
            }
        }
      

        internal static List<DateTime> FindDate(List<string> enteranceAndExitDate, Enums.FindDate fd)
        {
            lock (m_lockFindDate)
            {
                try
                {
                    List<string> eaed = new List<string>(enteranceAndExitDate);
                    List<DateTime> date = new List<DateTime>();

                    if (fd == Enums.FindDate.Enterance)
                    {
                        date.AddRange(eaed.ConvertAll(x => DateTime.Parse(x.Split('-')[0])));
                    }
                    else if (fd == Enums.FindDate.Exit)
                    {
                        date.AddRange(eaed.ConvertAll(x => DateTime.Parse(x.Split('-')[1])));
                    }

                    return date;
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.FindEntranceOrExitViolationDatesExceptionMessage, ex);
                }
            }
        }


        internal static List<string> FindDate(List<DateTime> enteranceOrExitDate, List<string> enteranceAndExitDate, Enums.FindDate fd)
        {
            lock (m_lockFindDate)
            {
                try
                {
                    List<DateTime> eaed = new List<DateTime>(enteranceOrExitDate);

                    List<string> ddde = new List<string>();

                    List<DateTime> date = new List<DateTime>();

                    if (fd == Enums.FindDate.Enterance)
                    {
                        for (int i = 0; i < enteranceOrExitDate.Count; i++)
                        {
                            ddde.Add(enteranceAndExitDate.Find(x => enteranceOrExitDate[i] == DateTime.Parse(x.Split('-')[0])));
                        }
                    }
                    else if (fd == Enums.FindDate.Exit)
                    {
                        for (int i = 0; i < enteranceOrExitDate.Count; i++)
                        {
                            ddde.Add(enteranceAndExitDate.Find(x => enteranceOrExitDate[i] == DateTime.Parse(x.Split('-')[1])));
                        }
                    }

                    return ddde;
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.FindEntranceOrExitViolationDatesExceptionMessage, ex);
                }
            }
        }

        internal static List<string> FindDate(List<DateTime> enteranceOrExitTime)
        {
            return null;
        }

        static object m_lockFindDate = new object();
        internal static DateTime FindDate(DateTime date, int hour, int minute, int second, Enums.Date d)
        {
            lock (m_lockFindDate)
            {
                try
                {
                    DateTime newDate = new DateTime(date.Year, date.Month, date.Day,
                        date.Hour, date.Minute, date.Second);
                    DateTime violationDate = new DateTime();
                    
                    TimeSpan ts = new TimeSpan(hour, minute, second);

                    switch (d)
                    {
                        case Enums.Date.Add:
                            {
                                violationDate = newDate.Add(ts);
                                break;
                            }
                        case Enums.Date.Subtract:
                            {
                                violationDate = newDate.Subtract(ts);
                                break;
                            }
                    }

                    return violationDate;
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.FindEntranceOrExitViolationDatesExceptionMessage, ex);
                }
            }
        }


        internal static DateTime FindDate(DateTime date, TimeSpan time, Enums.Date d)
        {
            lock (m_lockFindDate)
            {
                try
                {
                    DateTime newDate = new DateTime(date.Year, date.Month, date.Day,
                        date.Hour, date.Minute, date.Second);
                    DateTime violationDate = new DateTime();

                    switch (d)
                    {
                        case Enums.Date.Add:
                            {
                                violationDate = newDate.Add(time);
                                break;
                            }
                        case Enums.Date.Subtract:
                            {
                                violationDate = newDate.Subtract(time);
                                break;
                            }
                    }

                    return violationDate;
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.FindEntranceOrExitViolationDatesExceptionMessage, ex);
                }
            }
        }

        public static DateTime StringDateToDateTime(string date, string time)
        {
            lock (date)
            {
                try
                {
                    int dateDot = date.IndexOf(".");
                    int dateDash = date.IndexOf("-");

                    int timeSemiColon = time.IndexOf(":");
                    int timeDash = time.IndexOf("-");

                    int year = 0, month = 0, day = 0, hour = 0, minute = 0, second = 0;

                    if (dateDot > 0)
                    {
                        string[] splitDate = date.Split('.');
                        year = int.Parse(splitDate[2]);
                        month = int.Parse(splitDate[1]);
                        day = int.Parse(splitDate[0]);

                    }
                    else if (dateDash > 0)
                    {
                        string[] splitDate = date.Split('-');
                        year = int.Parse(splitDate[0]);
                        month = int.Parse(splitDate[1]);
                        day = int.Parse(splitDate[2]);
                    }

                    if (timeSemiColon > 0)
                    {
                        string[] splitHour = time.Split(':');
                        hour = int.Parse(splitHour[0]);
                        minute = int.Parse(splitHour[1]);
                        second = int.Parse(splitHour[2]);

                    }
                    else if (timeDash > 0)
                    {
                        string[] splitHour = time.Split('-');
                        hour = int.Parse(splitHour[0]);
                        minute = int.Parse(splitHour[1]);
                        second = int.Parse(splitHour[2]);
                    }

                    return new DateTime(year, month, day, hour, minute, second);
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.StringDateToDateTimeExceptionMessage, ex);
                }
            }
        }

        private static List<TimeSpan> FindWorkPlanHour(List<string> workPlan, string day)
        {
            List<TimeSpan> workHour = new List<TimeSpan>();
 
            workHour.AddRange(workPlan.FindAll(x => x.Split(',')[0] == day).ConvertAll(x => TimeSpan.Parse(x.Split(',')[1].Split('-')[0])));
            workHour.AddRange(workPlan.FindAll(x => x.Split(',')[0] == day).ConvertAll(x => TimeSpan.Parse(x.Split(',')[1].Split('-')[1])));

            workHour.Sort();
 
            return workHour;
        }

        private static List<DateTime> CheckDateIsInWorkSchedule(List<TimeSpan> workHour, DateTime date, Enums.WorkPlan wp)
        {
            List<DateTime> inWorkPlanDate = new List<DateTime>();
            List<DateTime> outWorkPlanDate = new List<DateTime>();

            for (int j = 0; j < workHour.Count; j += 2)
            {
                if ((workHour[j] <= (new TimeSpan(date.Hour, date.Minute, date.Second))) &&
                    (workHour[j + 1] >= (new TimeSpan(date.Hour, date.Minute, date.Second))))
                    inWorkPlanDate.Add(date);
            }

            if(!inWorkPlanDate.Contains(date))
                outWorkPlanDate.Add(date);


            if(wp == Enums.WorkPlan.In)
                return inWorkPlanDate;
            else
                return outWorkPlanDate;
        }


        public static List<string> CheckProgramWorkPlan(List<string> validatedImageNameList, List<string> workPlan, Enums.WorkPlan wp)
        {
            List<DateTime> workPlanDate = new List<DateTime>();
            List<string> checkedWorkPlanImageNameList = new List<string>();
        
            if(workPlan.Count > 0 && validatedImageNameList.Count > 0)
            {
                HashSet<string> workPlanDay = new HashSet<string>(workPlan.ConvertAll(x => x.Split(',')[0]));
                List<TimeSpan> mondayWorkHour = new List<TimeSpan>();
                List<TimeSpan> tuesdayWorkHour = new List<TimeSpan>();
                List<TimeSpan> wednesdayWorkHour = new List<TimeSpan>();
                List<TimeSpan> thursdayWorkHour = new List<TimeSpan>();
                List<TimeSpan> fridayWorkHour = new List<TimeSpan>();
                List<TimeSpan> saturdayWorkHour = new List<TimeSpan>();
                List<TimeSpan> sundayWorkHour = new List<TimeSpan>();

                foreach (string day in workPlanDay)
                {
                    switch (day)
                    {
                        case "Pazartesi":
                            {
                                mondayWorkHour.AddRange(FindWorkPlanHour(workPlan, day));
                                break;
                            }
                        case "Salı":
                            {
                                tuesdayWorkHour.AddRange(FindWorkPlanHour(workPlan, day));
                                break;
                            }
                        case "Çarşamba":
                            {
                                wednesdayWorkHour.AddRange(FindWorkPlanHour(workPlan, day));
                                break;
                            }
                        case "Perşembe":
                            {
                                thursdayWorkHour.AddRange(FindWorkPlanHour(workPlan, day));
                                break;
                            }
                        case "Cuma":
                            {
                                fridayWorkHour.AddRange(FindWorkPlanHour(workPlan, day));
                                break;
                            }
                        case "Cumartesi":
                            {
                                saturdayWorkHour.AddRange(FindWorkPlanHour(workPlan, day));
                                break;
                            }
                        case "Pazar":
                            {
                                sundayWorkHour.AddRange(FindWorkPlanHour(workPlan, day));
                                break;
                            }
                    }
                }

                List<DateTime> plateDateList = ImageName.ImagesDate(validatedImageNameList);

                for (int i = 0; i < plateDateList.Count; i++)
                {
                    switch (plateDateList[i].DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            {
                                List<DateTime> violationDates = CheckDateIsInWorkSchedule(mondayWorkHour, plateDateList[i], wp);
                                checkedWorkPlanImageNameList.AddRange(ImageName.ViolationImagesNameList(violationDates, validatedImageNameList));
                                break;
                            }
                        case DayOfWeek.Tuesday:
                            {
                                List<DateTime> violationDates = CheckDateIsInWorkSchedule(tuesdayWorkHour, plateDateList[i], wp);
                                checkedWorkPlanImageNameList.AddRange(ImageName.ViolationImagesNameList(violationDates, validatedImageNameList));
                                break;
                            }
                        case DayOfWeek.Wednesday:
                            {
                                List<DateTime> violationDates = CheckDateIsInWorkSchedule(wednesdayWorkHour, plateDateList[i], wp);
                                checkedWorkPlanImageNameList.AddRange(ImageName.ViolationImagesNameList(violationDates, validatedImageNameList));
                                break;
                            }
                        case DayOfWeek.Thursday:
                            {
                                List<DateTime> violationDates = CheckDateIsInWorkSchedule(thursdayWorkHour, plateDateList[i], wp);
                                checkedWorkPlanImageNameList.AddRange(ImageName.ViolationImagesNameList(violationDates, validatedImageNameList));
                                break;
                            }
                        case DayOfWeek.Friday:
                            {
                                List<DateTime> violationDates = CheckDateIsInWorkSchedule(fridayWorkHour, plateDateList[i], wp);
                                checkedWorkPlanImageNameList.AddRange(ImageName.ViolationImagesNameList(violationDates, validatedImageNameList));
                                break;
                            }
                        case DayOfWeek.Saturday:
                            {
                                List<DateTime> violationDates = CheckDateIsInWorkSchedule(saturdayWorkHour, plateDateList[i], wp);
                                checkedWorkPlanImageNameList.AddRange(ImageName.ViolationImagesNameList(violationDates, validatedImageNameList));
                                break;
                            }
                        case DayOfWeek.Sunday:
                            {
                                List<DateTime> violationDates = CheckDateIsInWorkSchedule(sundayWorkHour, plateDateList[i], wp);
                                checkedWorkPlanImageNameList.AddRange(ImageName.ViolationImagesNameList(violationDates, validatedImageNameList));
                                break;
                            }
                    }
                }
            }

            return checkedWorkPlanImageNameList;
        }

        public static void CheckProgramWorkPlan3(List<string> validatedImageNameList, List<string> workPlan, Enums.WorkPlan wp)
        {
            List<DateTime> workPlanDate = new List<DateTime>();
            List<string> checkedWorkPlanImageNameList = new List<string>();

            if (workPlan.Count > 0 && validatedImageNameList.Count > 0)
            {
                HashSet<string> workPlanDay = new HashSet<string>(workPlan.ConvertAll(x => x.Split(',')[0]));
                List<TimeSpan> mondayWorkHour = new List<TimeSpan>();
                List<TimeSpan> tuesdayWorkHour = new List<TimeSpan>();
                List<TimeSpan> wednesdayWorkHour = new List<TimeSpan>();
                List<TimeSpan> thursdayWorkHour = new List<TimeSpan>();
                List<TimeSpan> fridayWorkHour = new List<TimeSpan>();
                List<TimeSpan> saturdayWorkHour = new List<TimeSpan>();
                List<TimeSpan> sundayWorkHour = new List<TimeSpan>();

                foreach (string day in workPlanDay)
                {
                    switch (day)
                    {
                        case "Pazartesi":
                            {
                                mondayWorkHour.AddRange(FindWorkPlanHour(workPlan, day));
                                break;
                            }
                        case "Salı":
                            {
                                tuesdayWorkHour.AddRange(FindWorkPlanHour(workPlan, day));
                                break;
                            }
                        case "Çarşamba":
                            {
                                wednesdayWorkHour.AddRange(FindWorkPlanHour(workPlan, day));
                                break;
                            }
                        case "Perşembe":
                            {
                                thursdayWorkHour.AddRange(FindWorkPlanHour(workPlan, day));
                                break;
                            }
                        case "Cuma":
                            {
                                fridayWorkHour.AddRange(FindWorkPlanHour(workPlan, day));
                                break;
                            }
                        case "Cumartesi":
                            {
                                saturdayWorkHour.AddRange(FindWorkPlanHour(workPlan, day));
                                break;
                            }
                        case "Pazar":
                            {
                                sundayWorkHour.AddRange(FindWorkPlanHour(workPlan, day));
                                break;
                            }
                    }
                }

                List<DateTime> plateDateList = ImageName.ImagesDate(validatedImageNameList);

                for (int i = 0; i < plateDateList.Count; i++)
                {
                    switch (plateDateList[i].DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            {
                                List<DateTime> violationDates = CheckDateIsInWorkSchedule(mondayWorkHour, plateDateList[i], wp);
                                checkedWorkPlanImageNameList.AddRange(ImageName.ViolationImagesNameList(violationDates, validatedImageNameList));
                                break;
                            }
                        case DayOfWeek.Tuesday:
                            {
                                List<DateTime> violationDates = CheckDateIsInWorkSchedule(tuesdayWorkHour, plateDateList[i], wp);
                                checkedWorkPlanImageNameList.AddRange(ImageName.ViolationImagesNameList(violationDates, validatedImageNameList));
                                break;
                            }
                        case DayOfWeek.Wednesday:
                            {
                                List<DateTime> violationDates = CheckDateIsInWorkSchedule(wednesdayWorkHour, plateDateList[i], wp);
                                checkedWorkPlanImageNameList.AddRange(ImageName.ViolationImagesNameList(violationDates, validatedImageNameList));
                                break;
                            }
                        case DayOfWeek.Thursday:
                            {
                                List<DateTime> violationDates = CheckDateIsInWorkSchedule(thursdayWorkHour, plateDateList[i], wp);
                                checkedWorkPlanImageNameList.AddRange(ImageName.ViolationImagesNameList(violationDates, validatedImageNameList));
                                break;
                            }
                        case DayOfWeek.Friday:
                            {
                                List<DateTime> violationDates = CheckDateIsInWorkSchedule(fridayWorkHour, plateDateList[i], wp);
                                checkedWorkPlanImageNameList.AddRange(ImageName.ViolationImagesNameList(violationDates, validatedImageNameList));
                                break;
                            }
                        case DayOfWeek.Saturday:
                            {
                                List<DateTime> violationDates = CheckDateIsInWorkSchedule(saturdayWorkHour, plateDateList[i], wp);
                                checkedWorkPlanImageNameList.AddRange(ImageName.ViolationImagesNameList(violationDates, validatedImageNameList));
                                break;
                            }
                        case DayOfWeek.Sunday:
                            {
                                List<DateTime> violationDates = CheckDateIsInWorkSchedule(sundayWorkHour, plateDateList[i], wp);
                                checkedWorkPlanImageNameList.AddRange(ImageName.ViolationImagesNameList(violationDates, validatedImageNameList));
                                break;
                            }
                    }
                }
            }

            //return checkedWorkPlanImageNameList;
        }
    }
}
