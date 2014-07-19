using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace rm.Config
{
    /// <summary>
    /// Wrapper class.
    /// </summary>
    public static class ConfigSettings
    {
        /// <summary>
        /// Empty nvc.
        /// </summary>
        private static readonly NameValueCollection emptyNvc = new NameValueCollection();
        /// <summary>
        /// Get config-settings by configSection name.
        /// </summary>
        public static IConfigSettings GetSection(string sectionName)
        {
            if (sectionName == null)
            {
                throw new ArgumentNullException("sectionName");
            }
            if (sectionName == "")
            {
                throw new ArgumentException("sectionName");
            }
            return new ConfigSectionConfigSettings(
                (NameValueCollection)ConfigurationManager.GetSection(sectionName) ?? emptyNvc
                );
        }
        /// <summary>
        /// Get config-settings by prefix.
        /// </summary>
        public static IConfigSettings GetByPrefix(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException("prefix");
            }
            if (prefix == "")
            {
                throw new ArgumentException("prefix");
            }
            return new AppSettingsConfigSettings(ConfigurationManager.AppSettings, prefix);
        }
    }
}
