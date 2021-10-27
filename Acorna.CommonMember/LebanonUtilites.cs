using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Acorna.CommonMember
{
    public static class LebanonUtilites
    {
        public static bool IsStringNumber(string calledNumber)
        {
            try
            {
                Int64 number = 0;
                if (Int64.TryParse(calledNumber, out number))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetOperatorKeyFromNumber(string phoneNumber)
        {
            try
            {
                int lengthPhoneNumber = phoneNumber.Length;

                if (lengthPhoneNumber == 12)
                {
                    phoneNumber = phoneNumber.Substring(1, 3);
                }

                return phoneNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime GetDateLastDayMonth(DateTime dateTime)
        {
            try
            {
                return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetNumberWithoutCountryKey(string phoneNumber)
        {
            try
            {
                int lengthPhoneNumber = phoneNumber.Length;
                string countryKey = string.Empty;

                if (lengthPhoneNumber == 11)
                {
                    countryKey = phoneNumber.Substring(0, 3);
                    if (countryKey == "961")
                    {
                        phoneNumber = phoneNumber.Substring(3, lengthPhoneNumber - 3);
                        phoneNumber = string.Format("0{0}", phoneNumber);
                    }
                }
                else if (lengthPhoneNumber == 8)
                {
                    phoneNumber = string.Format("0{0}", phoneNumber);
                }

                return phoneNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetMinutesFromDateTime(string durationDate)
        {
            try
            {
                DateTime dateTime = Convert.ToDateTime(durationDate); 
                int hours = dateTime.Hour;
                int minutes = dateTime.Minute;
                int seconds = dateTime.Second;


                if (seconds > 0)
                {
                    minutes = minutes + 1;
                }

                return Convert.ToString(minutes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime GetCallDateTime(string callDate, string callTime)
        {
            try
            {
                string date = Convert.ToDateTime(callDate).ToString("dd/MM/yyyy");
                string time = Convert.ToDateTime(callTime).ToString("H:mm:ss t");

                DateTime callDateTime = Convert.ToDateTime(date + " " + time);

                return callDateTime;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetTimeFromDateTime(string timeDate)
        {
            try
            {
                DateTime dateTime = Convert.ToDateTime(timeDate);
                return dateTime.ToString("H:mm:ss tt");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
