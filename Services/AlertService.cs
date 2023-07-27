using Device_Management.Models;


namespace Device_Management.Services
{
    public class AlertService
    {
        private readonly DeviceManagementDbContext _context;

        public AlertService(DeviceManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Alert> CreateAlert(Alert alert)
        {
            _context.Alerts.Add(alert);
            await _context.SaveChangesAsync();

            return alert;
        }

         
    }
}
