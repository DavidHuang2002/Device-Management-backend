namespace Device_Management.Models
{
    public partial class Alert
    {
        public int AlertId { get; set; }
        public int DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Severity { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string? AcknowledgedBy { get; set; }
        public DateTime? AcknowledgedTimestamp { get; set; }
        public string? ResolvedBy { get; set; }
        public DateTime? ResolvedTimestamp { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
