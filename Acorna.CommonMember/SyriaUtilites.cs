using System;
using System.Linq;

namespace Acorna.CommonMember
{
    public static class SyriaUtilites
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

        public static string GetNumberWithoutCountryKey(string phoneNumber)
        {
            try
            {
                int lengthPhoneNumber = phoneNumber.Length;
                string countryKey = string.Empty;

                if (lengthPhoneNumber == 12)
                {
                    countryKey = phoneNumber.Substring(0,3);
                    if(countryKey == "963")
                    {
                        phoneNumber = phoneNumber.Substring(3, lengthPhoneNumber - 3);
                        phoneNumber = string.Format("0{0}", phoneNumber);
                    }
                }
                else if (lengthPhoneNumber == 8)
                {
                    phoneNumber = string.Format("09{0}", phoneNumber);
                }

                return phoneNumber;
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
                return  new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetRandomPassword(int passLength)
        {
            var result = string.Empty;
            try
            {
                var chars = "abcdefghijklmnopqrstuvwxyz@#$&ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                result = new string(
                    Enumerable.Repeat(chars, passLength)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
