using System;

namespace rm.Config
{
    /// <summary>
    /// Defines methods to access config setting.
    /// </summary>
    public interface IConfigSettings
    {
        /// <summary>
        /// Get value for key.
        /// </summary>
        string Get(string key);
        /// <summary>
        /// Get value for key (indexer).
        /// </summary>
        string this[string key] { get; }
    }
}
