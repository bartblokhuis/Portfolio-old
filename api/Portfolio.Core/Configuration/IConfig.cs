using Newtonsoft.Json;

namespace Portfolio.Core.Configuration
{
    /// <summary>
    /// Represents a configuration from app settings
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// Gets a section name to load configuration
        /// </summary>
        [JsonIgnore]
        string Name => GetType().Name;
    }
}
