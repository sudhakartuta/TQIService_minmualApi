using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class EntryData
    {
        public int Id { get; set; }
        public string Formnumber { get; set; }
        public string Details { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public DateTime Entrydate { get; set; }
        public string Pay_mode { get; set; }
        public string Pay_sent { get; set; }
        public string Pay_approvedby { get; set; }
        public DateTime Approved_date { get; set; }
        public string Status { get; set; }
    }
}
