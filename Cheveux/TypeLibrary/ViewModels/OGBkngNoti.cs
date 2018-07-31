using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModels
{
    public class OGBkngNoti
    {
        public string BookingID { get; set; }
        public string SlotNo { get; set; }
            public DateTime StartTime { get; set; }
        public string CustomerID { get; set; }
            public string FirstName { get; set; }
            public string lastName { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string ContactNo { get; set; }
            public char PreferredCommunication { get; set; }
        public string StylistID { get; set; }
            public string stylistFirstName { get; set; }
        public string ServiceID { get; set; }
            public string serviceName { get; set; }
            public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public bool NotificationReminder { get; set; }
    }
}
