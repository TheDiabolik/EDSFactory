using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDSFactory
{
    class CheckImageNameOrList
    {
        private static bool CheckImageName(string imageName, bool hasPrefix, string prefix)
        {
            //try
            //{
                string plate = ImageName.Plate(imageName);
                string date = ImageName.Day(imageName);
                string hour = ImageName.Hour(imageName);
                string lastThreeDigit = ImageName.LastThreeDigit(imageName);
                string imageType = ImageName.ImageType(imageName);
                string placeNo = ImageName.PlaceNo(imageName);

                string edsPrefix = "";
                bool isPrefixEqual = false;

                if (hasPrefix)
                {
                    edsPrefix = ImageName.Prefix(imageName);
                    isPrefixEqual = edsPrefix.Equals(prefix);
                }

                string placeName = ImageName.PlaceName(imageName);

                bool isPlateOk = string.IsNullOrEmpty(plate);
                bool isDateOk = string.IsNullOrEmpty(date);
                bool isHourOk = string.IsNullOrEmpty(hour);
                bool isLastThreeDigitOk = string.IsNullOrEmpty(lastThreeDigit);
                bool isImageTypeOk = string.IsNullOrEmpty(imageType);
                bool isPlaceNoOk = string.IsNullOrEmpty(placeNo);
                bool isPrefixOk = string.IsNullOrEmpty(edsPrefix);
                bool isPlaceNameOk = string.IsNullOrEmpty(placeName);

                if (hasPrefix)
                {
                    if (!isPlateOk && !isDateOk && !isHourOk && !isLastThreeDigitOk && !isImageTypeOk && !isPlaceNoOk && !isPlaceNameOk && !isPrefixOk && isPrefixEqual)
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (!isPlateOk && !isDateOk && !isHourOk && !isLastThreeDigitOk && !isImageTypeOk && !isPlaceNoOk && !isPlaceNameOk)
                        return true;
                    else
                        return false;
                }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ExceptionMessages.CheckImageNameExceptionMessage, ex);
            //}
        }

        public static List<string> CheckImageNameList(List<string> imageNameList, Enums.Validate v, bool hasPrefix, string prefix)
        {
            try
            {
                List<string> imageNames = new List<string>();
                List<string> notValiditeImageNames = new List<string>();

                imageNames.AddRange(imageNameList);
                imageNames.Reverse();

                for (int i = imageNames.Count - 1; i >= 0; i--)
                { 
                    //bool isConfirmImageName = CheckImageName(imageNames[i], hasPrefix, prefix);

                     bool isConfirmImageName; 
                    
                    if(hasPrefix)
                        isConfirmImageName = VerificateImageName.ImageNameValidatorWithPrefix(imageNames[i]);
                    else
                        isConfirmImageName = VerificateImageName.ImageNameValidator(imageNames[i]);

                    if (!isConfirmImageName)
                    {
                        if (v == Enums.Validate.Valid)
                        {
                            imageNames.Remove(imageNames[i]);
                        }
                        else if (v == Enums.Validate.InValid)
                        {
                            notValiditeImageNames.Add(imageNames[i]);
                            imageNames.Remove(imageNames[i]);
                        }
                    }
                }

                if (v == Enums.Validate.InValid)
                {
                    imageNames.Clear();
                    imageNames.AddRange(notValiditeImageNames);
                }

                return imageNames;
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.CheckImageNameExceptionMessage, ex);
            }
        }

        public static List<string> FindTypeAndRangeList(List<string> confirmedImageNameList, Enums.TypeAndRange tar, int placeMinRange, int placeMaxRange, params string[] imageTypes)
        {
            try
            {
                List<string> wantedTypeAndRangeList = new List<string>();

                if (tar == Enums.TypeAndRange.Wanted)
                {
                    foreach (string imageType in imageTypes)
                    {
                        wantedTypeAndRangeList.AddRange(confirmedImageNameList.FindAll(elements => (int.Parse(ImageName.PlaceNo(elements)) >= placeMinRange) &&
                        (int.Parse(ImageName.PlaceNo(elements)) <= placeMaxRange) && ImageName.ImageType(elements) == imageType));
                    }
                }
                else if (tar == Enums.TypeAndRange.NotWanted)
                {
                    //istenilen araklık olmayan resimler
                    wantedTypeAndRangeList.AddRange(confirmedImageNameList.FindAll(elements => (int.Parse(ImageName.PlaceNo(elements)) <= placeMinRange) || (int.Parse(ImageName.PlaceNo(elements)) >= placeMaxRange)));
                    //ihlal tipleri
                    HashSet<string> notWantedType = new HashSet<string>(new List<string>(confirmedImageNameList.ConvertAll(x => ImageName.ImageType(x))));
                    //istenmiyen ihlal tiplerini buluyoruz
                    foreach (string item in imageTypes)
                        notWantedType.Remove(item);

                    foreach (string item in notWantedType)
                    {
                        List<string> findingNotWantedImageType = confirmedImageNameList.FindAll(elements => ImageName.ImageType(elements) == item);

                        foreach (var notWantedTypeItem in findingNotWantedImageType)
                        {
                            bool hasBefore = wantedTypeAndRangeList.Exists(x => x == notWantedTypeItem);

                            if (!hasBefore)
                                wantedTypeAndRangeList.Add(notWantedTypeItem);
                        }
                    }
                }

                return wantedTypeAndRangeList;
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.CheckTypeAndRangeExceptionMessage, ex);
            }
        }

        public static List<DateTime> CheckViolationTime(List<DateTime> violationDatesInDatabase, List<DateTime> violationDatesInList, Enums.Violation v, int protectViolationTime)
        {
            try
            {

                List<DateTime> violationTime = new List<DateTime>(violationDatesInList);
                List<DateTime> notViolationTime = new List<DateTime>();

                violationTime.Reverse();

                for (int i = violationTime.Count - 1; i >= 0; i--)
                {
                    DateTime violationDate = violationTime[i];

                    //plakayı tekrar ceza kesilmesin diye gelen ihlal zamanının 2 saat öncesei ve sonrasını belirliyoruz
                    DateTime maxViolationDate = ViolationsDate.FindDate(violationDate, protectViolationTime, 0, 0, Enums.Date.Add);
                    DateTime minViolationDate = ViolationsDate.FindDate(violationDate, protectViolationTime, 0, 0, Enums.Date.Subtract);

                    if (v == Enums.Violation.Violation)
                    {
                        //2 saat öncesine veya sonrasına ait ihlal var mı diye bakıyoruz?
                        bool hasViolationInDatabase = violationDatesInDatabase.Exists(elements => ((minViolationDate < elements) && (elements < maxViolationDate)));

                        if (hasViolationInDatabase)
                        {
                            violationTime.RemoveAll(elements => ((minViolationDate < elements) && (elements < maxViolationDate)));
                            i = violationTime.Count;
                        }
                        else
                        {
                            //min ve max zaman aralığında ceza kesilmiş mi?
                            bool hasViolationInList = violationTime.Exists(elements => ((elements != violationDate) && (minViolationDate < elements) && (elements < maxViolationDate)));

                            if (hasViolationInList)
                            {
                                violationTime.RemoveAll(elements => ((elements != violationDate) && (minViolationDate < elements) && (elements < maxViolationDate)));
                                i = violationTime.Count;
                            }
                        }
                    }
                    else if (v == Enums.Violation.NotViolation)
                    {
                        //2 saat öncesine veya sonrasına ait ihlal var mı diye bakıyoruz?
                        bool hasViolationInDatabase = violationDatesInDatabase.Exists(elements => ((minViolationDate < elements) && (elements < maxViolationDate)));

                        if (hasViolationInDatabase)
                        {
                            notViolationTime.AddRange(violationTime.FindAll(elements => ((minViolationDate < elements) && (elements < maxViolationDate))));

                            violationTime.RemoveAll(elements => ((minViolationDate < elements) && (elements < maxViolationDate)));
                            i = violationTime.Count;
                        }
                        else
                        {
                            //min ve max zaman aralığında ceza kesilmiş mi?
                            bool hasViolationInList = violationTime.Exists(elements => ((elements != violationDate) && (minViolationDate < elements) && (elements < maxViolationDate)));

                            if (hasViolationInList)
                            {
                                notViolationTime.AddRange(violationTime.FindAll(elements => ((elements != violationDate) && (minViolationDate < elements) && (elements < maxViolationDate))));

                                violationTime.RemoveAll(elements => ((minViolationDate < elements) && (elements < maxViolationDate)));
                                i = violationTime.Count;
                            }
                        }
                    }
                }

                if (v == Enums.Violation.NotViolation)
                {
                    violationTime.Clear();
                    violationTime.AddRange(notViolationTime);
                }

                violationTime.Sort();

                return violationTime;
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.CheckViolationExceptionMessage, ex);
            }
        }

        public static bool IsWantedType(string imageName, params string[] imageType)
        {
            lock (imageName)
            {
                bool isWantedType = false;

                for (int i = 0; i < imageType.Length; i++)
                {
                    if (ImageName.ImageType(imageName) == imageType[i])
                    {
                        isWantedType = true;
                        break;
                    }
                }

                return isWantedType;
            }
        }

        public static List<string> ValidateImageNameList(string plate, List<string> confirmedImageNameList, Enums.Validate v, params string[] imageType)
        {
            try
            {
                List<string> willValidateList = new List<string>();

                List<string> willplate = new List<string>();
                List<string> willnotplate = new List<string>();

                List<string> will = new List<string>();

                confirmedImageNameList.Reverse();
                willValidateList.AddRange(confirmedImageNameList);


                List<string> plates = new List<string>(); 
                List<string> toBeValidatePlateList = new List<string>();  

                plates.AddRange(confirmedImageNameList.FindAll(elements => ImageName.Plate(elements).Equals(plate)));

                for (int j = 0; j < imageType.Length; j++)
                    toBeValidatePlateList.AddRange(plates.FindAll(elements => ImageName.ImageType(elements) == imageType[j]));   

                List<DateTime> plateDateList = ImageName.ImagesDate(toBeValidatePlateList);

                for (int i = 0; i < plateDateList.Count; i++)
                {
                    string day = StringFormatOperation.Date(plateDateList[i].ToShortDateString());
                    string hour = StringFormatOperation.Hour(plateDateList[i].ToLongTimeString()); 

                    List<string> validityIt = toBeValidatePlateList.FindAll(elements => ImageName.Day(elements) == day && ImageName.Hour(elements) == hour  );

                    List<string> placeNames = validityIt.ConvertAll(x => ImageName.PlaceNo(x));
                    List<string> placeNumbers = validityIt.ConvertAll(x => ImageName.PlaceName(x));

                    bool comparetedPlaceNumber = CompareString(placeNumbers);
                    bool comparetedPlaceName = CompareString(placeNames);
                    bool hasAllType = HasAllType(validityIt, imageType);

                    if (v == Enums.Validate.Valid)
                    {
                        if (comparetedPlaceNumber && comparetedPlaceName && hasAllType)
                            willplate.AddRange(validityIt);
                    }
                    else if (v == Enums.Validate.InValid)
                    {
                        if (!comparetedPlaceNumber || !comparetedPlaceName || !hasAllType)
                            willnotplate.AddRange(validityIt);
                    }
                }

                if (v == Enums.Validate.Valid)
                    will.AddRange(willplate);
                else if (v == Enums.Validate.InValid)
                    will.AddRange(willnotplate);

                return will;

            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.ValidateImageNameExceptionMessage, ex);
            }
        }

        private static bool HasAllType(List<string> list, params string[] imageType)
        {
            try
            {
                List<bool> hasAllImagesType = new List<bool>();

                for (int i = 0; i < imageType.Length; i++)
                    hasAllImagesType.Add(list.Exists(x=> ImageName.ImageType(x) == imageType[i]));

                bool notAllImagesTypeSame = hasAllImagesType.Contains(false);

                if (notAllImagesTypeSame)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.HasAllTypeExceptionMessage, ex);
            }
        }

        private static bool CompareString(params string[] compareString)
        {
            try
            {
                string firstElement = compareString[0];

                List<bool> isAllElementSame = new List<bool>();

                foreach (string compare in compareString)
                {
                    bool isElementSame = firstElement.Equals(compare);
                    isAllElementSame.Add(isElementSame);
                }

                List<bool> controlAllElement = isAllElementSame.FindAll(elements => elements == true);

                if (controlAllElement.Count == isAllElementSame.Count)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.CompareStringExceptionMessage, ex);
            }

        }

        private static bool CompareString(List<string> compareString)
        {
            try
            {
                bool isSame = true;

                if ((compareString.Count != 0))
                //if ((compareString.Count != 0) && (compareString.Count % 2 == 0))
                {
                    List<int> isAllElementSame = new List<int>();
                    string compare = compareString[0];

                    foreach (string item in compareString)
                    {
                        isAllElementSame.Add(string.Compare(compare, item));
                    }

                  

                    //for (int i = 0; i < compareString.Count; i += 2)
                    //{
                        
                    //}

                    isSame = isAllElementSame.Exists(x => (x < 0) && (x > 0));
                }

                if (!isSame)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.CompareStringExceptionMessage, ex);
            }
        }


//        public static void ValidatedImageNames1(List<string> workPlan1, Settings.EntryAndExitViolationTwoImages eaevs, EventsMethods.EnteranceAndExitViolationTwoImages eaeve,
//List<string> workPlan, DatabaseOperation.EnteranceAndExit eaevdo, params string[] imageTypes)
//        {
           

//            List<string> validatedImageNameList = new List<string>();
//            List<string> imageNameList = new List<string>();

//            imageNameList = FileOperation.Find(eaevs.m_imagePath);

//            //eds formatında olmayan resimleri siliyoruz
//            List<string> confirmedImageNameList = CheckImageNameOrList.CheckImageNameList(imageNameList, Enums.Validate.Valid, eaeve.m_hasPrefix, eaeve.m_prefix);

//            //resimler istenlen tipte ve aralıkta ise 
//            List<string> wantedImageTypeAndRangeList = new List<string>();


//            wantedImageTypeAndRangeList = CheckImageNameOrList.FindTypeAndRangeList(confirmedImageNameList, Enums.TypeAndRange.Wanted, eaeve.m_minPlaceRange, eaeve.m_maxPlaceRange, imageTypes);


//            //resimler klasöründeki plakaları alıyoruz
//            HashSet<string> plate = new HashSet<string>(wantedImageTypeAndRangeList.ConvertAll(x => ImageName.Plate(x)));


//            ////doğru isim formatı, belirlenen resim tipleri ve aralıkta olan resimler için plakalarda geziyoruz
//            foreach (string itemPlate in plate)
//            {
//                bool isValid =   true;//PlateVerification(itemPlate);

//                if (isValid)
//                {
//                    List<string> toBeCheckImagesNames = wantedImageTypeAndRangeList.FindAll(x => ImageName.Plate(x) == itemPlate);
//                    List<DateTime> toBeCheckDates = new List<DateTime>();
//                    List<string> duplicateDates = new List<string>();

//                    for (int i = 0; i < toBeCheckImagesNames.Count; i++)
//                    {
//                        if (!toBeCheckDates.Contains(ViolationsDate.StringDateToDateTime(ImageName.Day(toBeCheckImagesNames[i]), ImageName.Hour(toBeCheckImagesNames[i]))))
//                            toBeCheckDates.Add(ViolationsDate.StringDateToDateTime(ImageName.Day(toBeCheckImagesNames[i]), ImageName.Hour(toBeCheckImagesNames[i])));
//                        else
//                            duplicateDates.Add(toBeCheckImagesNames[i]);
//                    }

//                    foreach (string item in duplicateDates)
//                        toBeCheckImagesNames.Remove(item);

//                    toBeCheckDates.Sort();
//                    toBeCheckDates.Reverse();

//                    for (int i = toBeCheckDates.Count - 1; i >= 0; i--)
//                    {
//                        DateTime dt = toBeCheckDates[i];

//                        int redu = toBeCheckDates.RemoveAll(x => (x.ToShortDateString() == dt.ToShortDateString()) && (x > dt) && (x < dt.AddMinutes(1)));

//                        if (redu > 0)
//                            i = toBeCheckDates.Count;
//                    }

//                    foreach (DateTime item in toBeCheckDates)
//                    {
//                        string day = StringFormatOperation.Date(item.ToShortDateString());
//                        string hour = StringFormatOperation.Hour(item.ToLongTimeString());

//                        //validatedImageNameList.AddRange(toBeCheckImagesNames.FindAll(x => (ImageName.Day(x) == day) && (ImageName.Hour(x) == hour)));

//                        List<string> imageNames = toBeCheckImagesNames.FindAll(x => (ImageName.Day(x) == day) && (ImageName.Hour(x) == hour));

//                        List<string> checkedWorkPlan = ViolationsDate.CheckProgramWorkPlan(imageNames, workPlan, Enums.WorkPlan.In);

//                        foreach (string itcem in checkedWorkPlan)
//                        {
//                            workPlan1.Add(itcem);

//                            //string id = wantedImageTypeAndRangeList.Find(x => ImageName.Plate(x) == item);
//                            //File.Copy(eaevs.m_imagePath + "\\" + itcem, "d:\\tutu\\" + itcem);
//                        }
//                    }
//                }
//            }
//        }








        public static bool PlateVerification(string plate)
        {
            char[] redse = plate.ToCharArray();

            string desensayı = "[0-9]";
            string desenyazı = "[A-Z]";
            string sehir = "";
            string arayazi = "";
            string sonsayi = "";


            for (int i = 0; i < redse.Length; i++)
            {
                Match eslesme = Regex.Match(redse[i].ToString(), desensayı, RegexOptions.IgnoreCase);

                if (eslesme.Success)
                {
                    if (arayazi.Length <= 0)
                        sehir += eslesme.Value.ToString();
                    else
                        sonsayi += eslesme.Value.ToString();
                }
                else
                {
                    Match eslesme3 = Regex.Match(redse[i].ToString(), desenyazı, RegexOptions.IgnoreCase);

                    if (eslesme3.Success)
                        arayazi += eslesme3.Value.ToString();
                }
            }

            int city = int.Parse(sehir);
            int middle = arayazi.Length;
            int last = sonsayi.Length;


            int result = 0;

            if (city >= 1 && city <= 81)
            {
                if (middle == 1)
                {
                    if (last >= 4 && last <= 5)
                        result = 1;
                }
                if (middle == 2)
                {
                    if (last >= 3 && last <= 4)
                        result = 1;
                }
                else if (middle == 3)
                {
                    if (last >= 2)
                        result = 1;
                }
            }

            if (result == 1)
                return true;
            else
                return false;
        }













//        public static List<string> ValidatedImageNames(Settings.EntryAndExitViolationTwoImages eaevs, EventsMethods.EnteranceAndExitViolationTwoImages eaeve,
//List<string> workPlan, DatabaseOperation.EnteranceAndExit eaevdo, params string[] imageTypes)
//        {
//            List<string> validatedImageNameList = new List<string>();
//            List<string> imageNameList = new List<string>();

//            imageNameList = FileOperation.Find(eaevs.m_imagePath);
            
//            //eds formatında olmayan resimleri siliyoruz
//            List<string> confirmedImageNameList = CheckImageNameOrList.CheckImageNameList(imageNameList, Enums.Validate.Valid, eaeve.m_hasPrefix, eaeve.m_prefix);

//            //resimler istenlen tipte ve aralıkta ise 
//            List<string> wantedImageTypeAndRangeList = new List<string>();


//            wantedImageTypeAndRangeList = CheckImageNameOrList.FindTypeAndRangeList(confirmedImageNameList, Enums.TypeAndRange.Wanted, eaeve.m_minPlaceRange, eaeve.m_maxPlaceRange, imageTypes);


//            //resimler klasöründeki plakaları alıyoruz
//            HashSet<string> plate = new HashSet<string>(wantedImageTypeAndRangeList.ConvertAll(x => ImageName.Plate(x)));

       

//            List<string> checkedWorkPlan = ViolationsDate.CheckProgramWorkPlan(validatedImageNameList, workPlan, Enums.WorkPlan.In);

//            return checkedWorkPlan;
//        }


//        public static List<string> ViolationImageNames(Settings.EntryAndExitViolationTwoImages eaevs, EventsMethods.EnteranceAndExitViolationTwoImages eaeve,
//List<string> workPlan, DatabaseOperation.EnteranceAndExit eaevdo, List<string> exitImagesName)
//        {
//            List<string> entryImagesName = CheckImageNameOrList.ValidatedImageNames(eaevs, eaeve, workPlan,eaevdo, "L1-C1",
//        "L1-C2", "L1-C3", "L1-C4");

//            List<string> imageNameList = new List<string>();

//            List<string> imageNameList3 = new List<string>();

//            imageNameList.AddRange(entryImagesName);
//            imageNameList.AddRange(exitImagesName);

//            //resimler klasöründeki plakaları alıyoruz
//            HashSet<string> plate = new HashSet<string>(imageNameList.ConvertAll(x => ImageName.Plate(x)));

//            //doğru isim formatı, belirlenen resim tipleri ve aralıkta olan resimler için plakalarda geziyoruz
//            foreach (string itemPlate in plate)
//            {
//                List<string> entryPlate = entryImagesName.FindAll(x => ImageName.Plate(x) == itemPlate);
//                List<string> exityPlate = exitImagesName.FindAll(x => ImageName.Plate(x) == itemPlate);

//                List<DateTime> entryDates = ImageName.ImagesDate(entryPlate);
//                List<DateTime> exitDates = ImageName.ImagesDate(exityPlate);

//                List<string> violationDate = ViolationsDate.EntranceExitViolationDates(entryDates, exitDates, eaevs.m_distance, eaevs.m_speedLimit);
//                List<string> violationImagesNames = ImageName.ViolationImagesNameList(violationDate, imageNameList);



//                List<DateTime> violationDate4 = ViolationsDate.FindDate(violationDate, Enums.FindDate.Enterance);

//                List<string> violationImes = ImageName.ViolationImagesNameList(violationDate4, imageNameList);


//                imageNameList3.AddRange(violationImes);
//            }

//            return imageNameList3; 
//        }

         

//        public static List<byte[]> ViolationEntryImagesAndNamesToByte(Settings.EntryAndExitViolationTwoImages eaevs, EventsMethods.EnteranceAndExitViolationTwoImages eaeve,
//List<string> workPlan, DatabaseOperation.EnteranceAndExit eaevdo, List<string> exitImagesName)
//        {
//            List<string> violationImagesName = CheckImageNameOrList.ViolationImageNames(eaevs, eaeve, workPlan, eaevdo, exitImagesName);

//            List<string> wantedImageTypeAndRangeList = CheckImageNameOrList.FindTypeAndRangeList(violationImagesName, Enums.TypeAndRange.Wanted, eaeve.m_minPlaceRange, eaeve.m_maxPlaceRange, "L1-C1",
//  "L1-C2", "L1-C3", "L1-C4");

//            List<byte[]> violationEntryImagesAndNamesToByte = new List<byte[]>();
//                 byte[] byteDot = Encoding.Unicode.GetBytes("..");
//                 byte[] byteSemicolon = Encoding.Unicode.GetBytes(",,");

//            foreach (string item in wantedImageTypeAndRangeList)
//            {
//                //byte[] byteArray = Encoding.Unicode.GetBytes(item.ToCharArray());
//                ////byte[] byteImage = SocketCommunication.ReadImageFile(eaevs.m_imagePath + "\\" + item);

//                //violationEntryImagesAndNamesToByte.Add(byteArray);
//                //violationEntryImagesAndNamesToByte.Add(byteSemicolon);
//                //violationEntryImagesAndNamesToByte.Add(byteImage);
//                //violationEntryImagesAndNamesToByte.Add(byteSemicolon);
//            }

//            violationEntryImagesAndNamesToByte.Add(byteDot);
//            return violationEntryImagesAndNamesToByte;

//        }
    }
}
