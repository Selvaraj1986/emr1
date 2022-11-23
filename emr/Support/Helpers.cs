using System.ComponentModel.DataAnnotations;
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
    }

}
