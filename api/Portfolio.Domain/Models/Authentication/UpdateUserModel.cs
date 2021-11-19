using System;

namespace Portfolio.Domain.Models.Authentication
{
    public class UpdateUserModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string NewPassword { get; set; }

        public string OldPassword { get; set; }

        public string Email { get; set; }
    }
}
