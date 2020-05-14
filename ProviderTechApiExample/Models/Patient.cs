using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderTechApiExample.Models
{
    public class Patient
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthday { get; set; }
        public string Phone { get; set; }
        public string Language { get; set; }
        public string MessageStatus { get; set; }
        public string MessageDetail { get; set; }
    }
}
