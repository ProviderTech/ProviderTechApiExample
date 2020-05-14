using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderTechApiExample.Models
{
    public class Appointment
    {
        public string Id { get; set; }
        public string StartDateTime { get; set; }
        public string PatientId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Provider { get; set; }
        public string Location { get; set; }
        public string MessageStatus { get; set; }
        public string MessageDetail { get; set; }
    }
}
