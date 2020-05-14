using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderTechApiExample.Models
{
    public class Message
    {
        public string LegacyAppointmentId { get; set; }
        public string AppointmentStart { get; set; }
        public string Location { get; set; }
        public string Provider { get; set; }
        public string MessageId { get; set; }
        public string Direction { get; set; }
        public string Protocol { get; set; }
        public string ProtocolRule { get; set; }
        public string MainContent { get; set; }
        public string SentDate { get; set; }
        public string ScheduledDate { get; set; }
        public string LegacyPatientId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
    }
}
