using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicAPI.Models
{
    public class Appointment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }
        [Required]
        public int ScheduleId { get; set; }
        [ForeignKey("ScheduleId")]
        public virtual ReservedSchedule ReservedSchedule { get; set; }
    }
}
