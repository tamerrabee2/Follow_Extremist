using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow.Core
{
    public class UsersRoles
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public bool Value { get; set; }

        // Navigation 
        public int UserId { get; set; }
        public Users Users { get; set; }
    }
}
