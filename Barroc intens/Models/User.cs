using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barroc_intens.Models
{
    internal class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public DateTime Created_at { get; set; }
        public Role Role { get; set; }
    }
}
