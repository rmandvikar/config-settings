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
        private static readonly object monitor = new object();
        /// <summary>
        /// Dictionary for appSettings config-settings by prefix.
        /// </summary>
        internal static readonly IDictionary<string, AppSettingsConfigSettings> AppSettingsCache =
            new Dictionary<string, AppSettingsConfigSettings>();
        /// <summary>
        /// Dictionary for configSection config-settings by sectionName.
        /// </summary>
        internal static readonly IDictionary<string, ConfigSectionConfigSettings> ConfigSectionCache =
            new Dictionary<string, ConfigSectionConfigSettings>();
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
            ConfigSectionConfigSettings configsettings;
            if (!ConfigSectionCache.TryGetValue(sectionName, out configsettings))
            {
                lock (monitor)
                {
                    if (!ConfigSectionCache.TryGetValue(sectionName, out configsettings))
                    {
                        configsettings = new ConfigSectionConfigSettings(
                            (NameValueCollection)ConfigurationManager.GetSection(sectionName) ?? emptyNvc
                            );
                        ConfigSectionCache.Add(sectionName, configsettings);
                    }
                }
            }
            return configsettings;
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
            AppSettingsConfigSettings configsettings;
            if (!AppSettingsCache.TryGetValue(prefix, out configsettings))
            {
                lock (monitor)
                {
                    if (!AppSettingsCache.TryGetValue(prefix, out configsettings))
                    {
                        configsettings = new AppSettingsConfigSettings(ConfigurationManager.AppSettings, prefix);
                        AppSettingsCache.Add(prefix, configsettings);
                    }
                }
            }
            return configsettings;
        }
    }
}
