using System;
using System.Collections.Specialized;

namespace rm.Config
{
    /// <summary>
    /// Reads config settings from appSettings.
    /// </summary>
    public class AppSettingsConfigSettings : IConfigSettings
    {
        /// <summary>
        /// The nv collection to read from.
        /// </summary>
        private NameValueCollection settings;
        /// <summary>
        /// Prefix used to qualify key name.
        /// </summary>
        public string Prefix { get; private set; }
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="settings">The nv collection to read from.</param>
        /// <param name="prefix">Prefix used to qualify key name.</param>
        internal AppSettingsConfigSettings(NameValueCollection settings, string prefix)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            if (prefix == null)
            {
                throw new ArgumentNullException("prefix");
            }
            if (prefix == "")
            {
                throw new ArgumentException("prefix");
            }
            this.settings = settings;
            this.Prefix = prefix;
        }
        /// <summary>
        /// Get value for key.
        /// </summary>
        public string Get(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (key == "")
            {
                throw new ArgumentException("key");
            }
            return settings[Prefix + "." + key];
        }
        /// <summary>
        /// Get value for key (indexer).
        /// </summary>
        public string this[string key]
        {
            get { return Get(key); }
        }
    }
}
