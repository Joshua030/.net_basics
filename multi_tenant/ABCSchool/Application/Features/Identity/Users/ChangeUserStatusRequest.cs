using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Identity.Users
{
    public class ChangeUserStatusRequest
    {
        public string UserId { get; set; }
        public bool Activation { get; set; }
    }
}
