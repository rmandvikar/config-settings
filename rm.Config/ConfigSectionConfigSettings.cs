using System;
using System.Collections.Specialized;

namespace rm.Config
{
    /// <summary>
    /// Reads config settings from configSection.
    /// </summary>
    public class ConfigSectionConfigSettings : IConfigSettings
    {
        /// <summary>
        /// The nv collection to read from.
        /// </summary>
        private NameValueCollection settings;
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="settings">The nv collection to read from.</param>
        internal ConfigSectionConfigSettings(NameValueCollection settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            this.settings = settings;
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
            return settings[key];
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
