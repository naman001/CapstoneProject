using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class Appointment
    {
        public int appointmentId { get; set; }
        public int scheduleId { get; set; }
    }
}
