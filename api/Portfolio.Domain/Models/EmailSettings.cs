using Portfolio.Domain.Models.Common;

namespace Portfolio.Domain.Models
{
    public class EmailSettings : BaseEntity, ISetting
    {
        #region Properties

        public string Email { get; set; }

        public string DisplayName { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool EnableSsl { get; set; }

        public bool UseDefaultCredentials { get; set; }

        public string SendTestEmailTo { get; set; }

        #endregion
    }
}
