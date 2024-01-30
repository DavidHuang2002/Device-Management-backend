using Device_Management.Models.AlertManagement;
using Device_Management.Models;
using Newtonsoft.Json.Linq;

namespace Device_Management.Services
{
    public class AlertRuleEvaluator
    {
        // return null when condition is not met
        static public AlertTemplate? Evaluate(AlertRule rule, JObject message)
        {
            // TODO how to handle when the attribute doesn't exist. Maybe dont do anything? Maybe sign off a warning?
            var attributeValue = (message[rule.Attribute]?.ToString()) ?? throw new ArgumentException($"The message {message} doesn't have attribute for rule{rule.Id} to check");
            
            if (CheckCondition(attributeValue, rule.AttributeDataType, rule.Operator, rule.Value))
            {
                var template = rule.AlertTemplate;

                template.Description = CompileAlertDescriptionFromTemplate(template.Description, rule.Attribute, attributeValue);

                return template;
            }

            return null;
        }
            
       static public String CompileAlertDescriptionFromTemplate(String templateDescription, string attributeName, string attributeValStr)
        {
            return templateDescription.Replace($"${{{attributeName}}}", attributeValStr);
        }

        static private bool CheckCondition(string attributeValue, DataType dataType, Operator op, string value)
        {
            // Delegating the comparison to methods based on the DataType.
            switch (dataType)
            {
                case DataType.Int:
                    return CheckConditionInt(int.Parse(attributeValue), op, int.Parse(value));
                case DataType.Float:
                    return CheckConditionFloat(float.Parse(attributeValue), op, float.Parse(value));
                case DataType.String:
                    return CheckConditionString(attributeValue, op, value);
                default:
                    throw new NotImplementedException($"DataType {dataType} is not implemented");
            }
        }

        static private bool CheckConditionInt(int attributeValue, Operator op, int value)
        {
            return op switch
            {
                Operator.Equals => attributeValue == value,
                Operator.NotEquals => attributeValue != value,
                Operator.LessThan => attributeValue < value,
                Operator.GreaterThan => attributeValue > value,
                Operator.LessThanOrEqualTo => attributeValue <= value,
                Operator.GreaterThanOrEqualTo => attributeValue >= value,
                _ => throw new NotImplementedException($"Operator {op} is not implemented"),
            };
        }

        static private bool CheckConditionFloat(float attributeValue, Operator op, float value)
        {
            return op switch
            {
                Operator.Equals => attributeValue == value,
                Operator.NotEquals => attributeValue != value,
                Operator.LessThan => attributeValue < value,
                Operator.GreaterThan => attributeValue > value,
                Operator.LessThanOrEqualTo => attributeValue <= value,
                Operator.GreaterThanOrEqualTo => attributeValue >= value,
                _ => throw new NotImplementedException($"Operator {op} is not implemented"),
            };
        }

        static private bool CheckConditionString(string attributeValue, Operator op, string value)
        {
            return op switch
            {
                Operator.Equals => attributeValue == value,
                Operator.NotEquals => attributeValue != value,
                _ => throw new NotImplementedException($"Operator {op} is not implemented"),
            };
        }
    }

}
