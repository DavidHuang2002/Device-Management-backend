using Device_Management.Models;
using Device_Management.Models.AlertManagement;
using Device_Management.Services.Email;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Device_Management.Services
{

    public class AlertService
    {
        private readonly DeviceManagementDbContext _context;
        private readonly IEmailService _emailService;

        public AlertService(DeviceManagementDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<Alert> CreateAlert(Alert alert)
        {
            _context.Alerts.Add(alert);
            await _context.SaveChangesAsync();

            return alert;
        }

         // TODO methods for alerting
         public async Task<Alert?> CheckAlertRules(int deviceId, JObject jsonMessage)
        {
            // get the device's rule
            var device = await _context.Devices.FirstOrDefaultAsync(d => d.Id == deviceId) ?? throw new ArgumentException($"device with Id {deviceId} not found");
            var rules = device.AlertRules?.ToList();
            if( rules != null )
            {
                // check it against message
                foreach (var rule in rules)
                {
                    var alertInfoTemplate = AlertRuleEvaluator.Evaluate(rule, jsonMessage);
                    if (alertInfoTemplate != null)
                    {
                        Alert alert = new(deviceId, alertInfoTemplate);
                        _ = _emailService.SendAlertEmailAsync(alert);
                        return await CreateAlert(alert);
                    }
                }
            }
            return null;
        }

        
    }
}
