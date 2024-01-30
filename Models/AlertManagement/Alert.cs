namespace Device_Management.Models.AlertManagement
{
    public partial class Alert
    {
        public int AlertId { get; set; }

        public string AlertName { get; set; } = string.Empty;
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


        // Constructor that takes an AlertTemplate object
        public Alert(int deviceId, AlertTemplate template)
        {
            this.DeviceId = deviceId;
            this.Timestamp = DateTime.UtcNow;


            // Initialize properties from the AlertTemplate
            this.AlertName = template.AlertName;
            this.Severity = template.Severity;
            this.Description = template.Description;
            this.Status = template.Status;
        }

        // Default constructor
        public Alert()
        {
        }
    }
}
