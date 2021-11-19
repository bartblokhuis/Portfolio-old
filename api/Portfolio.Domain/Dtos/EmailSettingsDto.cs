using Portfolio.Domain.Dtos.Common;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Domain.Dtos
{
    public class EmailSettingsDto
    {
        #region Properties

        [Required]
        public string Email { get; set; }

        [Required]
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
