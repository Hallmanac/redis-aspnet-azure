using System;


namespace RedisOnAzure.Web.App_Common
{
    public static class StringExtensionMethods
    {
        /// <summary>
        ///     Parses a string for a boolean value (True or False). The parse is wrapped in a try/catch and if an exception is
        ///     thrown
        ///     false is returned.
        /// </summary>
        public static bool ParseBool(this string str)
        {
            try
            {
                if (string.Equals("1", str))
                {
                    return true;
                }
                if (string.Equals("0", str))
                {
                    return false;
                }
                var value = bool.Parse(str);
                return value;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}