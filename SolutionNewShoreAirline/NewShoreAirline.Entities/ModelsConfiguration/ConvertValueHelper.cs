namespace NewShoreAirline.Entities.ModelsConfiguration
{
    public class ConvertValueHelper
    {
        public static TimeSpan ConvertHourValue(object obj)
        {
            try
            {
                if (obj == null)
                    return DateTime.Now.TimeOfDay;

                string value = Convert.ToString(obj);

                if (TimeSpan.TryParse(value, out TimeSpan hour))
                    return DateTime.Now.TimeOfDay;

                return hour;
            }
            catch (Exception)
            {
                return DateTime.Now.TimeOfDay;
            }
        }
        public static DateTime ConvertDateValue(object obj)
        {
            try
            {
                if (obj == null)
                    return DateTime.Now;

                string value = Convert.ToString(obj);

                if (DateTime.TryParse(value, out DateTime date))
                    return DateTime.Now;

                return date;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }
        public static string ConvertStringValue(object obj)
        {
            try
            {
                if (obj == null)
                    return string.Empty;

                string value = Convert.ToString(obj);

                if (string.IsNullOrEmpty(value))
                    return string.Empty;

                return value;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static decimal ConvertDecimalValue(object obj)
        {
            try
            {
                if (obj == null)
                    return 0;

                string value = Convert.ToString(obj);

                if (!decimal.TryParse(value, out decimal num))
                    return 0;

                return num;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int ConvertIntValue(object obj)
        {
            try
            {
                if (obj == null)
                    return 0;

                string value = Convert.ToString(obj);

                if (!int.TryParse(value, out int num))
                    return 0;

                return num;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
