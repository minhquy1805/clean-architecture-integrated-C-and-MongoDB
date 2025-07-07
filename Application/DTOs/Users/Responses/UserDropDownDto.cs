using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Users.Responses
{
    public class UserDropDownDto
    {
        public string? UserId { get; set; }
        public string FullName { get; set; } = default!;
    }
}
