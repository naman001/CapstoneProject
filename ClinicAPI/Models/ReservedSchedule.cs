using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicAPI.Models
{
    public class ReservedSchedule
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScheduleId { get; set; }
        [Required]
        public int SlotId { get; set; }
        [Required]
        public int DocId { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public string Title { get; set;}
        [Required]
        public string Description { get; set;}
        [Required]
        public string Start { get; set; }
        [Required]
        public string End { get; set; }
        [ForeignKey("DocId")]
        public virtual Doctor Doctor { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }
        [ForeignKey("SlotId")]
        public virtual TimeSlot TimeSlot { get; set; } 
        public ICollection<Appointment> Appointments { get; set; }
    }
}
