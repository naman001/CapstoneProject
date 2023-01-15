using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicAPI.Models
{
    public class Doctor
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocId { get; set; }
        [Required]
        public string DocName { get; set; }
        public ICollection<ReservedSchedule> ReservedSchedules { get; set; }
    }
}
