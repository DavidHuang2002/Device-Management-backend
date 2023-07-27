using System.ComponentModel.DataAnnotations;

namespace Device_Management.Models.DeviceUpdate
{
    // TODO make the class abstract
    public class DeviceUpdate
    {
        [Key]
        public int UpdateId { get; set; }

        [Required]
        public int DeviceId { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}
