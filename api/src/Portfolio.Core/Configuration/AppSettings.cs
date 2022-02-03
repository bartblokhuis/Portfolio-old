namespace Portfolio.Core.Configuration;

public class AppSettings
{
    /// <summary>
    /// Gets or sets hosting configuration parameters
    /// </summary>
    public HostingConfig HostingConfig { get; set; } = new HostingConfig();

    public bool IsDemo { get; set; }
}

