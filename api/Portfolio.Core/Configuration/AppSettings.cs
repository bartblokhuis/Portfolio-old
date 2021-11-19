using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Configuration
{
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets hosting configuration parameters
        /// </summary>
        public HostingConfig HostingConfig { get; set; } = new HostingConfig();
    }
}
