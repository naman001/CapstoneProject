using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class ReservedSchedule
    {
        public int scheduleId { get; set; }
        public int slotId { get; set; }
        public int docId { get; set; }
        public int patientId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string start { get; set; }
        public string end { get; set; }
    }
}
