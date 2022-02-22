using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory
{
    class StringFormatOperation
    {
        public static string Hour(string hour)
        {
            try
            {
                string de = "";

                if (hour.IndexOf("-") != -1)
                    de = hour.Replace("-", ":");
                else if (hour.IndexOf(":") != -1)
                    de = hour.Replace(":", "-");

                return de;
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.ConvertToHourExceptionMessage, ex);
            }
        }

        public static string Date(string date)
        {
            try
            {
                string result = "";

                if (date.IndexOf("-") != -1)
                {
                    string[] splitDay = date.Split('-');
                    result = string.Format("{0}-{1}-{2}", splitDay[2], splitDay[1], splitDay[0]).Replace("-", ".");
                }
                else if (date.IndexOf(".") != -1)
                {
                    string[] splitDay = date.Split('.');
                    result = string.Format("{0}-{1}-{2}", splitDay[2], splitDay[1], splitDay[0]);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.ConvertToDayExceptionMessage, ex);
            }
        }

    }
}
