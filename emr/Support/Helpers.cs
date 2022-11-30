using Microsoft.Data.SqlClient.Server;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Xml.Linq;

namespace emr.Support
{
    public static class Helpers
    {
        public enum Activities
        {
            Independent = 1,
            [Display(Name = "With Assistance")]
            WithAssistance = 2,
        }
        public enum Status
        {
            No = 0,
            Yes = 1,
        }
        public static T GetEnumValue<T>(int intValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            T val = ((T[])Enum.GetValues(typeof(T)))[0];

            foreach (T enumValue in (T[])Enum.GetValues(typeof(T)))
            {
                if (Convert.ToInt32(enumValue).Equals(intValue))
                {
                    val = enumValue;
                    break;
                }
            }
            return val;
        }
        public static string ToDateString(this DateTime date)
        {
            string dateValue = string.Empty;
            if (date != null)
                dateValue = date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            else
                dateValue = string.Empty;
            return dateValue;
        }
        public static DateTime ToDateFormat(this string date)
        {
            var dateValue = new DateTime();
            if (date != null)
                dateValue = Convert.ToDateTime(date);
            else
                dateValue = DateTime.Now;
            return dateValue;
        }
        public static string GetTodayDate()
        {
            string cleanString = string.Empty;
            cleanString = DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

            return cleanString;
        }
        public static string ToDateTimeFormat(this DateTime date)
        {
            string cleanString = string.Empty;
            cleanString = date.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            return cleanString;
        }
    }

}
