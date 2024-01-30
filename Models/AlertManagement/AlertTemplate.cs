using System.ComponentModel.DataAnnotations;

namespace Device_Management.Models.AlertManagement
{
    public class AlertTemplate
    {
        [Key]
        public int AlertTemplateId { get; set; }
        public string? AlertTemplateName { get; set; }

        [Required]
        public string AlertName { get; set; }

        // TODO add enum for severity
        [Required]
        public string Severity { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        // Default constructor
        public AlertTemplate()
        {
        }

        // Copy constructor
        public AlertTemplate(AlertTemplate other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other), "The input AlertTemplate object is null.");
            }
            // not setting AlertId since we want the copy to be a different entry in DB
            this.AlertTemplateName = other.AlertTemplateName;
            this.AlertName = other.AlertName;
            this.Severity = other.Severity;
            this.Description = other.Description;
            this.Status = other.Status;
        }
    }
}
