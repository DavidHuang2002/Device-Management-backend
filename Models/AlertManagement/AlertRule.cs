using System.ComponentModel.DataAnnotations;


namespace Device_Management.Models.AlertManagement
{
    public enum Operator
    {
        Equals,
        NotEquals,
        LessThan,
        GreaterThan,
        LessThanOrEqualTo,
        GreaterThanOrEqualTo
        // and so on
    }

    public enum DataType
    {
        Int,
        Float,
        String,
    }

    public class AlertRule
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        [Required]
        public int DeviceId { get; set; }

        public string Attribute { get; set; } // the name of the JSON attribute to monitor
        public DataType AttributeDataType { get; set; }
        public Operator Operator { get; set; } // the condition's operator
        public string Value { get; set; } // the value to compare against
        public AlertTemplate AlertTemplate { get; set; } // the alert info to raise when the rule is met

    }
}
