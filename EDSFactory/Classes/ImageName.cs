using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory
{
    static class ImageName
    {
        
        //resim ismini döndüren metot
        public static string Name(string imageName)//exception tamamlandı
        {
            lock (imageName)
            {
                try
                {
                    return Path.GetFileName(imageName);
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.PathGetFileExceptionMessage, ex);
                }
            }
        }

        //resim ismini döndüren metot
        public static string Prefix(string imageName)//exception tamamlandı
        {
            lock (imageName)
            {
                try
                {
                    string[] splitImagePath = imageName.Split('-');
                    string name = splitImagePath[10];

                    string[] splitImageName = name.Split(' ');
                    string prefix = splitImageName[0] + " " + splitImageName[1];
                    return prefix.ToUpper();
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.PathGetFileExceptionMessage, ex);
                }
            }
        }

        //isimdeki plakayı döndüren metot
        public static string Plate(string imageName)//exception tamamlandı
        {
            lock (imageName)
            {
                try
                {
                    string o = Path.GetFileName(imageName).Substring(0, Path.GetFileName(imageName).IndexOf("#"));
                    return o;
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.PlateExceptionMessage, ex);
                }
            }
        }

        //resim ismindeki tarihi döndüren metot
        public static string Day(string imageName)//exception tamamlandı
        {
            lock (imageName)
            {
                try
                {
                    string[] splitArray = imageName.Split('(');
                    return splitArray[1].Substring(0, splitArray[1].IndexOf(")"));
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.DayExceptionMessage, ex);
                }
            }
        }

        //resim ismindeki saati döndüren metot
        public static string Hour(string imageName)//exception tamamlandı
        {
            lock (imageName)
            {
                try
                {

                    string[] splitArray = imageName.Split('(');
                    return splitArray[2].Substring(0, splitArray[2].IndexOf(")") - 4);
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.HourExceptionMessage, ex);
                }
            }
        }

        //resim ismindeki saati döndüren metot
        public static string LastThreeDigit(string imageName)//exception tamamlandı
        {
            lock (imageName)
            {
                try
                {
                    string[] splitArray = imageName.Split('-');
                    return splitArray[6].Substring(0, 3);
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.LastThreeDigitExceptionMessage, ex);
                }
            }
        } 

        //resim ismindeki yer nosunu
        public static string ImageType(string imageName)
        {
            lock (imageName)
            {
                try
                {
                    string[] splitArray = imageName.Split('(');
                    return splitArray[3].Substring(0, 5);
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.ImageTypeExceptionMessage, ex);
                }
            }
        }

        //resim ismindeki yer nosunu
        public static string PlaceNo(string imageName)
        {
            lock (imageName)
            {
                try
                {
                    string[] splitArray = imageName.Split('-');
                    return splitArray[9];
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.PlaceNoExceptionMessage, ex);
                }
            }
        }

        //resim ismindeki yer bilgisini
        public static string PlaceName(string imageName)
        {
            lock (imageName)
            {
                try
                {
                    string[] splitArray = imageName.Split('-');
                    return Path.GetFileNameWithoutExtension(splitArray[10]);
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.PlaceNameExceptionMessage, ex);
                }
            }
        }


        //resim ismindeki yer bilgisini
        public static Point PlateFirstAxis(string imageName)
        {
            lock (imageName)
            {
                try
                {
                    string[] splitArray = imageName.Split('-');
                    return new Point(int.Parse(splitArray[11]), int.Parse(splitArray[12]));
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.PlaceNameExceptionMessage, ex);
                }
            }
        }

        //resim ismindeki yer bilgisini
        public static Point PlateSecondAxis(string imageName)
        {
            lock (imageName)
            {
                try
                {
                    string[] splitArray = imageName.Split('-');
                    return new Point(int.Parse(splitArray[13]), int.Parse(Path.GetFileNameWithoutExtension(splitArray[14]))); 
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.PlaceNameExceptionMessage, ex);
                }
            }
        }

        public static List<DateTime> ImagesDate(List<string> validatedImageNameList)
        {
            lock (validatedImageNameList)
            {
                HashSet<DateTime> date = new HashSet<DateTime>();
                try
                {

                    for (int i = 0; i < validatedImageNameList.Count; i++)
                    {
                        string day = ImageName.Day(validatedImageNameList[i]);
                        string hour = ImageName.Hour(validatedImageNameList[i]);

                        date.Add(ViolationsDate.StringDateToDateTime(day, hour));
                    }

                    List<DateTime> dates = new List<DateTime>(date.ToArray());

                    return dates;
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.FindImageDateExceptionMessage, ex);
                }
            }
        }

        public static string ImageNameFormat(string plate, string date, string hour, string lastThreeDigit, string imageType, string placeNo, string place)
        {
            lock (plate)
            {
                try
                {
                    string imageName = plate + "#(" + date + ")-(" + hour + "-" + lastThreeDigit + ")-(" + imageType + ")-" + placeNo + "-" + place + ".jpg";
                    return imageName;
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.CreateImageNameExceptionMessage, ex);
                }
            }
        }

        internal static List<string> ViolationImagesNameList(List<string> violationDates, List<string> imagesName)
        {
            lock (violationDates)
            {
                List<string> violationImagesNameList = new List<string>();

                try
                { 
                    for (int i = 0; i < violationDates.Count; i++)
                    {
                        string[] splitArray = violationDates[i].Split('-');
                        DateTime entryDate = DateTime.Parse(splitArray[0]);
                        DateTime exitDate = DateTime.Parse(splitArray[1]);

                        string entryDay = StringFormatOperation.Date(entryDate.ToShortDateString());
                        string entryHour = StringFormatOperation.Hour(entryDate.ToLongTimeString());
                        string exitDay = StringFormatOperation.Date(exitDate.ToShortDateString());
                        string exitHour = StringFormatOperation.Hour(exitDate.ToLongTimeString());

                        List<string> entryDateList = imagesName.FindAll(elements => (ImageName.Day(elements) == entryDay) && (ImageName.Hour(elements) == entryHour));
                        List<string> exitDateList = imagesName.FindAll(elements => (ImageName.Day(elements) == exitDay) && (ImageName.Hour(elements) == exitHour));

                        violationImagesNameList.AddRange(entryDateList);
                        violationImagesNameList.AddRange(exitDateList);
                    }

                    return violationImagesNameList;
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.FindViolationImagesExceptionMessage, ex);
                }
            }
        }

        internal static List<string> ViolationImagesNameList(List<DateTime> violationDates, List<string> imagesName)
        {
            lock (violationDates)
            {
                List<string> violationImagesNameList = new List<string>();

                try
                {

                    for (int i = 0; i < violationDates.Count; i++)
                    {
                        string day = StringFormatOperation.Date(violationDates[i].ToShortDateString());
                        string hour = StringFormatOperation.Hour(violationDates[i].ToLongTimeString());

                        List<string> date = imagesName.FindAll(elements => (ImageName.Day(elements) == day) &&
                            (ImageName.Hour(elements) == hour));

                        violationImagesNameList.AddRange(date);
                    }

                    return violationImagesNameList;
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.FindViolationImagesExceptionMessage, ex);
                }
            }
        } 
    }
}
