using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Exist { get; set; }
        public DateTime? Activedate { get; set; }
        public string Access_level { get; set; }
        public string Payment_mode { get; set; }
        public string Lockedby { get; set; }
        public DateTime? Lockeddate { get; set; }
        public string Status { get; set; }
    }
}
