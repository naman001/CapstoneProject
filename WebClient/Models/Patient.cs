using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class Patient
    {
        public int patientId { get; set; }
        public string patientName { get; set; }
    }
}
