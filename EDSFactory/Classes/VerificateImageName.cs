using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EDSFactory
{
    class VerificateImageName
    {
        static object locker = new object();

        public static bool Name(string imageName, bool hasPrefix)//exception tamamlandı
        {
            lock (locker)
            {
                bool isVerified = false;

                try
                {
                    if (ImageNameValidator(imageName))
                    {
                        isVerified = true;
                    }

                    return isVerified;
                }
                catch
                {
                    isVerified = false;
                    return isVerified;
                }
            }
        }


        public static bool ImageNameValidator(string name)
        {
            bool isValid = false;

            name = name.Replace("(", string.Empty).Replace(")", string.Empty);

            Regex OneCharFourDigit = new Regex("[0-9]{2}[a-zA-Z][0-9]{4}#[0-9]{4}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{3}-[Ll][0-9]{1}-[Cc][0-9]{1}-[0-9]{4}-");
            Regex OneCharFiveDigit = new Regex("[0-9]{2}[a-zA-Z][0-9]{5}#[0-9]{4}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{3}-[Ll][0-9]{1}-[Cc][0-9]{1}-[0-9]{4}-");

            Regex TwoCharThreeDigit = new Regex("[0-9]{2}[a-zA-Z]{2}[0-9]{3}#[0-9]{4}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{3}-[Ll][0-9]{1}-[Cc][0-9]{1}-[0-9]{4}-");
            Regex TwoCharFourDigit = new Regex("[0-9]{2}[a-zA-Z]{2}[0-9]{4}#[0-9]{4}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{3}-[Ll][0-9]{1}-[Cc][0-9]{1}-[0-9]{4}-");

            Regex ThreeCharTwoDigit = new Regex("[0-9]{2}[a-zA-Z]{3}[0-9]{2}#[0-9]{4}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{3}-[Ll][0-9]{1}-[Cc][0-9]{1}-[0-9]{4}-");

            if ((OneCharFourDigit.IsMatch(name) || OneCharFiveDigit.IsMatch(name) || TwoCharThreeDigit.IsMatch(name) || TwoCharFourDigit.IsMatch(name) || ThreeCharTwoDigit.IsMatch(name)))
                isValid = true;

            return isValid;
        }

        public static bool ImageNameValidatorWithPrefix(string name)
        {
            bool isValid = false;

            name = name.Replace("(", string.Empty).Replace(")", string.Empty);

            Regex OneCharFourDigitMobil = new Regex("[0-9]{2}[a-zA-Z][0-9]{4}#[0-9]{4}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{3}-[Ll][0-9]{1}-[Cc][0-9]{1}-[0-9]{4}-[Mm][Oo][Bb][İi][Ll] [Ee][Dd][Ss] ");
            Regex OneCharFiveDigitMobil = new Regex("[0-9]{2}[a-zA-Z][0-9]{5}#[0-9]{4}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{3}-[Ll][0-9]{1}-[Cc][0-9]{1}-[0-9]{4}-[Mm][Oo][Bb][İi][Ll] [Ee][Dd][Ss] ");

            Regex TwoCharThreeDigitMobil = new Regex("[0-9]{2}[a-zA-Z]{2}[0-9]{3}#[0-9]{4}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{3}-[Ll][0-9]{1}-[Cc][0-9]{1}-[0-9]{4}-[Mm][Oo][Bb][İi][Ll] [Ee][Dd][Ss] ");
            Regex TwoCharFourDigitMobil = new Regex("[0-9]{2}[a-zA-Z]{2}[0-9]{4}#[0-9]{4}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{3}-[Ll][0-9]{1}-[Cc][0-9]{1}-[0-9]{4}-[Mm][Oo][Bb][İi][Ll] [Ee][Dd][Ss] ");

            Regex ThreeCharTwoDigitMobil = new Regex("[0-9]{2}[a-zA-Z]{3}[0-9]{2}#[0-9]{4}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{3}-[Ll][0-9]{1}-[Cc][0-9]{1}-[0-9]{4}-[Mm][Oo][Bb][İi][Ll] [Ee][Dd][Ss] ");

            if ((OneCharFourDigitMobil.IsMatch(name) || OneCharFiveDigitMobil.IsMatch(name) || TwoCharThreeDigitMobil.IsMatch(name) || TwoCharFourDigitMobil.IsMatch(name) || ThreeCharTwoDigitMobil.IsMatch(name)))
                isValid = true;

            return isValid;
        }

        //resim ismini döndüren metot
        private static bool Format(string imageName)//exception tamamlandı
        {
            bool isVerified = false;

            try
            {
                string plate = imageName.Substring(0, imageName.IndexOf("#") ); 
                imageName = imageName.Substring(imageName.IndexOf("#") + 1);

                char[] fullName = imageName.ToCharArray();

                char[] cplate = plate.ToArray();
                
                char[] date = new char[13];
                char[] hour = new char[10];
                char[] lastThereDigit = new char[5];
                char[] imageType = new char[8];
                char[] placeNumber = new char[5];
                char[] placeName = new char[imageName.Length - date.Length - hour.Length - lastThereDigit.Length - imageType.Length - placeNumber.Length];


                char[] prefix = imageName.ToCharArray();


                imageName.CopyTo(0, date, 0, date.Length);
                imageName.CopyTo(date.Length, hour, 0, hour.Length);
                imageName.CopyTo(hour.Length + date.Length, lastThereDigit, 0, lastThereDigit.Length);
                imageName.CopyTo(hour.Length + date.Length + lastThereDigit.Length, imageType, 0, imageType.Length);
                imageName.CopyTo(hour.Length + date.Length + lastThereDigit.Length + imageType.Length, placeNumber, 0, placeNumber.Length);
                imageName.CopyTo(hour.Length + date.Length + lastThereDigit.Length + imageType.Length + placeNumber.Length, placeName, 0, placeName.Length);


                bool isPlateValid = Plate(cplate);
                bool isDateValid = Date(date);
                bool isHourValid = Hour(hour);
                bool islastThereDigitValid = LastThreeDigit(lastThereDigit);
                bool isImageTypeValid = ImageType(imageType);
                bool isPlaceNumberValid = PlaceNumber(placeNumber);

                bool isPrValid = Prefix(placeName);


                if (isPlateValid && isDateValid && isHourValid && islastThereDigitValid && isImageTypeValid && isPlaceNumberValid)
                    isVerified = true;

                return isVerified;
            }
            catch
            {
                isVerified = false;
                return isVerified;
            }

        }

        private static bool Plate(char[] plate)
        {
            string desensayı = "[0-9]";
            string desenyazı = "[A-Z]";
            string sehir = "";
            string arayazi = "";
            string sonsayi = "";


            for (int i = 0; i < plate.Length; i++)
            {
                Match eslesme = Regex.Match(plate[i].ToString(), desensayı, RegexOptions.IgnoreCase);

                if (eslesme.Success)
                {
                    if (arayazi.Length <= 0)
                        sehir += eslesme.Value.ToString();
                    else
                        sonsayi += eslesme.Value.ToString();
                }
                else
                {
                    Match eslesme3 = Regex.Match(plate[i].ToString(), desenyazı, RegexOptions.None);

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
                    if (last == 2 )
                        result = 1;
                }
            }

            if (result == 1)
                return true;
            else
                return false;
       }

         //resim ismini döndüren metot
        private static bool Date(char[] date)//exception tamamlandı
        {
            bool isVerified = false;

            try
            {
                if (date[0] == '(' && date[5] == '-' && date[8] == '-' && date[11] == ')' && date[12] == '-')
                {
                    if (date[1] == '2' && date[2] == '0' && int.Parse((date[3] + "" + date[4]).ToString()) <= 20 && int.Parse((date[6] + "" + date[7]).ToString()) <= 12 &&
                        int.Parse((date[9] + "" + date[10]).ToString()) <= 31)
                    {
                        isVerified = true;

                    }
                }

                return isVerified;
            }
            catch
            {
                isVerified = false;
                return isVerified;
            }

        }

        private static bool Hour(char[] hour)//exception tamamlandı
        {
            bool isVerified = false;

            try
            {
                if (hour[0] == '(' && hour[3] == '-' && hour[6] == '-' && hour[9] == '-')
                {
                    if ((hour[1] >= '0' && hour[1] <= '2') && (hour[2] >= '0' && hour[2] <= '9') && (hour[4] >= '0' && hour[4] <= '5') && (hour[5] >= '0' && hour[5] <= '9')
                         && (hour[7] >= '0' && hour[7] <= '5') && (hour[8] >= '0' && hour[7] <= '9'))
                
                    {
                        isVerified = true;

                    }
                }

                return isVerified;
            }
            catch
            {
                isVerified = false;
                return isVerified;
            }

        }


        private static bool LastThreeDigit(char[] lastThereDigit)//exception tamamlandı
        {
            bool isVerified = false;

            try
            {
                if ((lastThereDigit[0] >= '0' && lastThereDigit[0] <= '9') && (lastThereDigit[1] >= '0' && lastThereDigit[1] <= '9') && (lastThereDigit[2] >= '0' && lastThereDigit[2] <= '9') &&
                    lastThereDigit[3] == ')' && lastThereDigit[4] == '-')
                {
                    isVerified = true;

                }

                return isVerified;
            }
            catch
            {
                isVerified = false;
                return isVerified;
            }

        }

        private static bool ImageType(char[] imageType)//exception tamamlandı
        {
            bool isVerified = false;

            try
            {
                 if (imageType[0] == '(' && imageType[1] == 'L' && imageType[3] == '-' && imageType[4] == 'C' && imageType[6] == ')' && imageType[7] == '-')
                {
                    if ((imageType[2] >= '1' && imageType[2] <= '9') && (imageType[5] >= '0' && imageType[5] <= '9'))
                    {
                        isVerified = true;
                    }
                }

                return isVerified;
            }
            catch
            {
                isVerified = false;
                return isVerified;
            }

        }
        private static bool PlaceNumber(char[] placeNumber)//exception tamamlandı
        {
            bool isVerified = false;

            try
            {
                if ((placeNumber[0] >= '0' && placeNumber[0] <= '9') && (placeNumber[1] >= '0' && placeNumber[1] <= '9') && (placeNumber[2] >= '0' && placeNumber[2] <= '9')
                    && (placeNumber[3] >= '0' && placeNumber[3] <= '9') &&
                    placeNumber[4] == '-')
                {
                    isVerified = true;

                }

                return isVerified;
            }
            catch
            {
                isVerified = false;
                return isVerified;
            }
             
        }

        private static bool Prefix(char[] prefix)//exception tamamlandı
        {
            bool isVerified = false;

            try
            {
                if (prefix[0] == 'M' && prefix[1] == 'O'&& prefix[2] == 'B' && prefix[3] == 'İ'
                    && prefix[4] == 'L' && prefix[5] == ' ' && prefix[6] == 'E' && prefix[7] == 'D' &&
                    prefix[8] == 'S' && prefix[9] == ' ')
                {
                    isVerified = true;

                }

                return isVerified;
            }
            catch
            {
                isVerified = false;
                return isVerified;
            }
           
        }



    }
}
