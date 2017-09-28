namespace IA.Weather.API.Configuration
{
    public class LogConfiguration
    {
    }

    public class LogConfigurationBuilder
    {
        public LogConfiguration Configuration => new LogConfiguration();

        /// <summary>
        /// Create a logging config from the app settings
        /// </summary>
        /// <param name="appNameKey"></param>
        /// <param name="seqPortKey"></param>
        /// <param name="levelKey"></param>
        /// <returns></returns>
        public LogConfigurationBuilder FromAppSettings(
            string appNameKey = "logging-appname",
            string seqPortKey = "logging-seqport",
            string levelKey = "logging-level")
        {
            return this;
        }

    }

}
