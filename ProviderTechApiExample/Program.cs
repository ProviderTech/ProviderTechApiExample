using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProviderTechApiExample.Models;
using RestSharp;

namespace ProviderTechApiExample
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //AddPatients();
            //AddAppointments();
            //ForecastAppointmentReminders();
            GetMessagesByDate();

            System.Threading.Thread.Sleep(50000);
        }

        private static void GetMessagesByDate()
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["Url"].ToString();
            string tenantId = System.Configuration.ConfigurationManager.AppSettings["TenantId"].ToString();
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["ApiKey"].ToString();

            var client = new RestClient(url);
            var request = new RestRequest("Messages/GetByDate", Method.POST);

            //add your keys
            request.AddHeader("tenantId", tenantId);
            request.AddHeader("apiKey", apiKey);
            request.AddQueryParameter("startDate", "5/1/2020"); //Protocol Id is appointment reminders
            request.AddQueryParameter("endDate", "5/21/2020"); //Protocol Id is appointment reminders
            request.RequestFormat = DataFormat.Json;

            try
            {
                //make the json call 
                var response = client.Execute(request);
                var messages = JsonConvert.DeserializeObject<List<Message>>(response.Content);
                foreach (var message in messages)
                {
                    Console.WriteLine("MessageId {0}, Direction {1}, Sent date {2}, Message {3}", message.MessageId, message.Direction, message.SentDate, message.MainContent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
        }


        private static void ForecastAppointmentReminders()
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["Url"].ToString();
            string tenantId = System.Configuration.ConfigurationManager.AppSettings["TenantId"].ToString();
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["ApiKey"].ToString();

            var client = new RestClient(url);
            var request = new RestRequest("Forecast/RunByProtocolId", Method.POST);

            //add your keys
            request.AddHeader("tenantId", tenantId);
            request.AddHeader("apiKey", apiKey);
            request.AddQueryParameter("protocolId", "1"); //Protocol Id is appointment reminders
            request.RequestFormat = DataFormat.Json;

            try
            {
                //make the json call 
                var response = client.Execute(request);
                Console.WriteLine(response.Content);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
        }

        private static  void AddPatients()
        {
            //set up your request 
            string url = System.Configuration.ConfigurationManager.AppSettings["Url"].ToString();
            string tenantId = System.Configuration.ConfigurationManager.AppSettings["TenantId"].ToString();
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["ApiKey"].ToString();

            var client = new RestClient(url);
            var request = new RestRequest("Patients/push", Method.POST);

            //add your keys
            request.AddHeader("tenantId", tenantId);
            request.AddHeader("apiKey", apiKey);

            //This will probably be a database call to get the patients
            List<Patient> patients = GetPatientsCollection();

            //add your json parameters
            var jsonBody = JsonConvert.SerializeObject(patients);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            try
            {
                //make the json call 
                var response = client.Execute(request);

                var processedPatients = JsonConvert.DeserializeObject<List<Patient>>(response.Content);
                foreach (var patient in processedPatients)
                {
                    Console.WriteLine("PatientId {0}, Status {1}, Details{2}", patient.Id, patient.MessageStatus, patient.MessageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            
        }

        private static List<Patient> GetPatientsCollection()
        {
            List<Patient> patients = new List<Patient>();


            patients.Add(new Patient
            {
                Id = "25",
                FirstName = "Joe",
                LastName = "Minimum",
                Phone = "1112223333"
            });

            patients.Add(new Patient
            {
                Id = "1",
                FirstName = "Allen",
                LastName = "Crane",
                Phone = "1234567890",
                Birthday = "12/30/1998",
                Language = "English"
            });

            patients.Add(new Patient
            {
                Id = "2",
                FirstName = "Burts",
                LastName = "Reynoldss",
                Phone = "3332224444",
                Language = "Spanish"
            });


            patients.Add(new Patient
            {
                Id = "22",
                FirstName = "Bad",
                LastName = "Bday",
                Phone = "3332224444",
                Birthday = "2/30/1998",
                Language = "Spanish"
            });

            patients.Add(new Patient
            {
                Id = "29",
                FirstName = "Bad",
                LastName = "Phone",
                Phone = "333222444",
                Language = "Spanish"
            });


            return patients;
        }


        private static void AddAppointments()
        {
            Console.WriteLine("Starting to add appointments");
            //create the list of patients you want to send 
            
            //set up your request 
            string url = System.Configuration.ConfigurationManager.AppSettings["Url"].ToString();
            string tenantId = System.Configuration.ConfigurationManager.AppSettings["TenantId"].ToString();
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["ApiKey"].ToString();

            var client = new RestClient(url);
            var request = new RestRequest("Appointment/Push", Method.POST);

            //add your keys
            request.AddHeader("tenantId", tenantId);
            request.AddHeader("apiKey", apiKey);


            //This will probably be a database call to get the appointments
            List<Appointment> appointments = GetAppointmentCollection();

            //add your json parameters
            var jsonBody = JsonConvert.SerializeObject(appointments);
            request.AddParameter("application/json; charset=utf-8", jsonBody, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            
            try
            {
                //Call the APU
                var response = client.Execute(request);

                //Loop through the responses to see how you did
                var processedAppointments = JsonConvert.DeserializeObject<List<Appointment>>(response.Content);
                foreach (var appointment in processedAppointments)
                {
                    Console.WriteLine("AppointmentId {0}, Status {1}, Details{2}", appointment.Id, appointment.MessageStatus, appointment.MessageDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
        
        private static List<Appointment> GetAppointmentCollection()
        {
            List<Appointment> appointments = new List<Appointment>();

            appointments.Add(new Appointment
            {
                Id = "1",
                PatientId = "1",
                Status = "Booked",
                StartDateTime = "5/18/2020 10:00"
            });

            appointments.Add(new Appointment
            {
                Id = "2",
                PatientId = "2",
                Status = "Rescheduled",
                StartDateTime = "5/18/2020 10:00",
                Type = "COVID",
                Provider = "Dr Suess",
                Location = "Austin Metro"
            });

            appointments.Add(new Appointment
            {
                Id = "3",
                PatientId = "2",
                Status = "Rescheduled",
                StartDateTime = "5/21/2020 11:00",
                Type = "COVID",
                Provider = "Dr Suess",
                Location = "Austin Metro"
            });


            appointments.Add(new Appointment
            {
                Id = "99",
                PatientId = "99", //Ths is a bad patient Id
                Status = "Rescheduled",
                StartDateTime = "5/18/2020 10:00",
                Type = "COVID",
                Provider = "Dr Suess",
                Location = "Austin Metro"
            });

            return appointments;
        }
    }
}
