namespace SDN.Utilities
{
    internal class Utils
    {
        public static T ConvertValue<T>(string value)
        {
            return (T)System.Convert.ChangeType(value, typeof(T));
        }

        public static int BoolToInt(bool p_bool)
        {
            return System.Convert.ToInt16( p_bool );
        }
    }
}