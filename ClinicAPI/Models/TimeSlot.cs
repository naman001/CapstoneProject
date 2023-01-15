using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicAPI.Models
{
    public class TimeSlot
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SlotId { get; set; }
        [Required]
        public string SlotDesc { get; set; }
        public ICollection<ReservedSchedule> ReservedSchedules { get; set; }
    }
}
