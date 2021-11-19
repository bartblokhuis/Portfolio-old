using System;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Models.Common;

namespace Portfolio.Domain.Models
{
    public class Message : BaseEntity, IFullyAudited, ISoftDelete
    {
        #region Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string MessageContent { get; set; }

        public MessageStatus MessageStatus { get; set; }

        public string IpAddress { get; set; }

        public bool HasSentNotification { get; set; }

        public DateTime CreatedAtUTC { get; set; }
        public DateTime UpdatedAtUtc { get; set; }

        public bool IsDeleted { get; set; }

        #endregion
    }
}
