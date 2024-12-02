using System;

namespace Barroc_intens.Models
{
    internal class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public DateTime Created_at { get; set; }
        public Role Role { get; set; }
    }
}
