using System;
using System.Collections.Generic;

namespace robot_controller_api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Passwordhash { get; set; } = null!;
        public string? Description { get; set; }
        public string? Role { get; set; }
        public DateTime Createddate { get; set; }
        public DateTime Modifieddate { get; set; }
    }
}
