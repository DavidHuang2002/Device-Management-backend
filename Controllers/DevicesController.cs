using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Device_Management.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Device_Management.Models.AlertManagement;

namespace Device_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly DeviceManagementDbContext _context;

        public DevicesController(DeviceManagementDbContext context)
        {
            _context = context;
        }

        // GET: api/Devices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
          if (_context.Devices == null)
          {
              return NotFound();
          }
            return await _context.Devices.ToListAsync();
        }

        // TODO add a controller to get with alertRule included

        // GET: api/Devices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(int id)
        {
          if (_context.Devices == null)
          {
              return NotFound();
          }
            var device = await _context.Devices.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            return device;
        }

        // GET: api/Devices/5/alertRules
        [HttpGet("{id}/alertRules")]
        public async Task<ActionResult<IEnumerable<AlertRule>>> GetAlertRulesForDevice(int id)
        {
            if (_context.Devices == null)
            {
                Console.WriteLine("Devices not found");
                return NotFound();
            }
            var rules = await _context.AlertRules.Where(ar => ar.DeviceId == id).Include(ar => ar.AlertTemplate).ToListAsync();

            return rules;
        }

        // TODO: bad implementation. Better way is to have a Device/State/{id} that can return the state of any type
        [HttpGet("raspberryPi/{id}")]
        public async Task<ActionResult<Device>> GetRaspberryPi(int id)
        {
            if (_context.Devices == null)
            {
                return NotFound();
            }
            var device = await _context.RaspberryPi.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            return device;
        }

        // GET: api/Devices/search?name={name}&type={type}&status={status}&startDate={startDate}&endDate={endDate}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Device>>> SearchDevices(string? name, string? type, string? status, DateTime? startDate, DateTime? endDate)
        {
            if (_context.Devices == null)
            {
                return NotFound();
            }

            var devices = _context.Devices.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                
                devices = devices.Where(d => d.Name != null && d.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(type))
            {
                devices = devices.Where(d => d.Type.Contains(type));
            }

            if (!string.IsNullOrEmpty(status))
            {
                devices = devices.Where(d => d.Status.Contains(status));
            }

            if (startDate.HasValue)
            {
                devices = devices.Where(d => d.LastCheckInTime >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                devices = devices.Where(d => d.LastCheckInTime <= endDate.Value);
            }

            var result = await devices.ToListAsync();

            //if (!result.Any())
            //{
            //    return NotFound();
            //}

            return result;
        }

        // PUT: api/Devices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice(int id, Device device)
        {
            if (id != device.Id)
            {
                return BadRequest();
            }

            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Devices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(Device device)
        {
          if (_context.Devices == null)
          {
              return Problem("Entity set 'DeviceManagementDbContext.Devices'  is null.");
          }
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDevice", new { id = device.Id }, device);
        }

        // DELETE: api/Devices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            if (_context.Devices == null)
            {
                return NotFound();
            }
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeviceExists(int id)
        {
            return (_context.Devices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
