using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class TimeSlot
    {
        public int slotId { get; set; }
        public string slotDesc { get; set; }
    }
}
