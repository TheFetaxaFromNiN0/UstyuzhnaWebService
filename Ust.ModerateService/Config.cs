using System;
using System.Configuration;

namespace Ust.ModerateService
{
    public class Config
    {
        public static string GetString(string key, bool strict = true)
        {
            try
            {
                var result = ConfigurationManager.AppSettings[key];

                if (result == null && strict)
                    throw GetConfigException(key, null);

                return result;
            }
            catch (Exception exc)
            {
                throw GetConfigException(key, exc);
            }
        }

        public static TimeSpan GetTimeSpan(string key, TimeSpan defaultValue)
        {
            try
            {
                var stringResult = ConfigurationManager.AppSettings[key];

                if (stringResult == null)
                    return defaultValue;

                var result = TimeSpan.Parse(stringResult);
                return result;
            }
            catch (Exception exc)
            {
                throw GetConfigException(key, exc);
            }
        }

        public static void Reload()
        {
            ConfigurationManager.RefreshSection("appSettings");
        }

        private static Exception GetConfigException(string key, Exception innerException)
        {
            return new ConfigurationErrorsException(string.Format("Not found or cannot parse {0} key from appSettings or connectionStrings of config", key), innerException);
        }
    }
}
