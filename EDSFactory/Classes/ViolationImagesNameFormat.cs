using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory
{
    class ViolationImagesNameFormat
    {
      //  public static string FormatFinder(string plate, string date, string hour, dynamic eaevs,
      //  dynamic eaeve, string imageType, string placeNo, string place)
      //  {
      //      string imageFormat="";

      //      string EDSType = eaeve.m_EDSType;

      //      switch (EDSType)
      //      {
      //          case "FixedHighwayShoulder":
      //              {
      //                  imageFormat = FixedHighwayShoulder(plate, date, hour, eaevs,
      //   eaeve,  imageType,  placeNo,  place);
      //                  break;
      //              }

      //          case "MobileHighwayShoulder":
      //              {
      //                  imageFormat = MobileHighwayShoulder(plate, date, hour, eaevs,
      //eaeve, imageType, placeNo, place);
      //                  break;
      //              }

      //          case "FixedParking":
      //              {
      //                  imageFormat = FixedParking(plate, date, hour, eaevs,
      //eaeve, imageType, placeNo, place);
      //                  break;
      //              }

      //          case "MobileParking":
      //              {
      //                  imageFormat = MobileParking(plate, date, hour, eaevs,
      //eaeve, imageType, placeNo, place);
      //                  break;
      //              }

      //          case "Standing":
      //              {
      //                  imageFormat = Standing(plate, date, hour, eaevs,
      // eaeve, imageType, placeNo, place);
      //                  break;
      //              }

      //          case "NoVehicles":
      //              {
      //                  imageFormat = NoVehicles(plate, date, hour, eaevs,
      //eaeve, imageType, placeNo, place);
      //                  break;
      //              }
      //          case "SpeedCorridor":
      //              {
      //                  imageFormat = NoVehicles(plate, date, hour, eaevs,
      //eaeve, imageType, placeNo, place);
      //                  break;
      //              }


      //      }

      //      return imageFormat;
      //  }



      //  public static string FormatFinder(string plate, string date, string hour, dynamic eaevs,
      // dynamic eaeve, int speed, string imageType, string placeNo, string place)
      //  {
      //       string imageFormat="";

      //      string EDSType = eaeve.m_EDSType;

      //      switch (EDSType)
      //      {
      //          case "SpeedCorridor":
      //              {
      //                  imageFormat = SpeedCorridor(plate, date, hour, eaevs,
      //eaeve, speed, imageType, placeNo, place);
      //                  break;
      //              }


      //      }

      //      return imageFormat;
      //  }


        public static string SpeedCorridor(string plate, string date, string hour,  int speedLimit, int speed, string imageType, string placeNo, string place)
        {
            string fe = imageType.Split('-')[0];

            string imageName = "";

            if (fe == "L1")
                imageName = plate + "#(" + date + ")-(" + hour + "-" + speed.ToString("000") + ")-(" + imageType + ")-" + placeNo + "-" + place + ".jpg";
            else if (fe == "L2")
                imageName = plate + "#(" + date + ")-(" + hour + "-" + speedLimit.ToString("000") + ")-(" + imageType + ")-" + placeNo + "-" + place + ".jpg";
            
            return imageName;
        }

        public static string FixedHighwayShoulder(string plate, string date, string hour, string imageType, string placeNo, string place)
        {
            string imageName = plate + "#(" + date + ")-(" + hour + "-" + "000" + ")-(" + imageType + ")-" + placeNo + "-" + place + ".jpg";
            return imageName;
        }

        public static string MobileParking(string plate, string date, string hour, int minViolationTimeMinute,  string imageType, string placeNo, string place)
        {
           // string imageName = plate + "#(" + date + ")-(" + hour + "-" + eaevs.m_minViolationTimeMinute.ToString("000") + ")-(" + imageType + ")-" + placeNo + "-" + eaeve.m_prefix + " " + place + ".jpg";
            string imageName = plate + "#(" + date + ")-(" + hour + "-" + minViolationTimeMinute.ToString("000") + ")-(" + imageType + ")-" + placeNo + "-" + place + ".jpg";
            
            return imageName;
        
        }

        public static string FixedParking(string plate, string date, string hour, int minViolationTimeMinute, string imageType, string placeNo, string place)
        {
            string imageName = plate + "#(" + date + ")-(" + hour + "-" +  minViolationTimeMinute.ToString("000") + ")-(" + imageType + ")-" + placeNo + "-" + place + ".jpg";
            return imageName;
        }

        public static string Standing(string plate, string date, string hour,  string imageType, string placeNo, string place)
        {
            string imageName = plate + "#(" + date + ")-(" + hour + "-" + "000" + ")-(" + imageType + ")-" + placeNo + "-" + place + ".jpg";
            //string imageName = plate + "#(" + date + ")-(" + hour + "-" + "000" + ")-(" + imageType + ")-" + placeNo + "-" + sevse.m_prefix + " " + place + ".jpg";
            return imageName;
        }
        //public static string NoVehicles(string plate, string date, string hour, Settings.SingleEntryViolationTwoImages m_ses,
        //EventsMethods.SingleEnteranceViolationTwoImages sevse, string imageType, string placeNo, string place)
        //{
        //    string imageName = plate + "#(" + date + ")-(" + hour + "-" + "000" + ")-(" + imageType + ")-" + placeNo + "-" + place + ".jpg";
        //    return imageName;
        //}
        public static string MobileHighwayShoulder(string plate, string date, string hour,  string imageType, string placeNo, string place)
        {
            string imageName = plate + "#(" + date + ")-(" + hour + "-" + "000" + ")-(" + imageType + ")-" + placeNo + "-"  + place + ".jpg";
            //string imageName = plate + "#(" + date + ")-(" + hour + "-" + "000" + ")-(" + imageType + ")-" + placeNo + "-" + sevse.m_prefix + " " + place + ".jpg";
            return imageName;
        }

    }
}
