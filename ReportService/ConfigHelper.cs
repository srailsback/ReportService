public static class ConfigHelper
{
    /// <summary>
    /// Get App Settings
    /// </summary>
    /// <param name="key">appSetting key</param>
    /// <returns></returns>
    public static string GetAppSetting(string key)
    {
        // this is obsolete, ConfigurationManager is better, need to fix
        return System.Configuration.ConfigurationSettings.AppSettings[key];
    }
}
