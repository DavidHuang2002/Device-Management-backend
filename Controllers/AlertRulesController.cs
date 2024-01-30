using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Device_Management.Models;
using Device_Management.Models.AlertManagement;

namespace Device_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertRulesController : ControllerBase
    {
        private readonly DeviceManagementDbContext _context;

        public AlertRulesController(DeviceManagementDbContext context)
        {
            _context = context;
        }

        // GET: api/AlertRules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertRule>>> GetAlertRules()
        {
          //if (_context.AlertRules == null)
          //{
          //    return NotFound();
          //}
            return await _context.AlertRules.ToListAsync();
        }

        // GET: api/AlertRules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlertRule>> GetAlertRule(int id)
        {
          if (_context.AlertRules == null)
          {
              return NotFound();
          }
            var alertRule = await _context.AlertRules.Include(ar => ar.AlertTemplate).SingleOrDefaultAsync(ar => ar.Id == id);

            if (alertRule == null)
            {
                return NotFound();
            }

            return alertRule;
        }

        // PUT: api/AlertRules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlertRule(int id, AlertRule alertRule)
        {
            if (id != alertRule.Id)
            {
                return BadRequest();
            }

            _context.Entry(alertRule).State = EntityState.Modified;
            _context.Entry(alertRule.AlertTemplate).State = EntityState.Modified;
            var templateId = alertRule.AlertTemplate.AlertTemplateId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertRuleExists(id) || !AlertTemplateExists(templateId))
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

        // POST: api/AlertRules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlertRule>> PostAlertRule(AlertRule alertRule)
        {
          if (_context.AlertRules == null)
          {
              return Problem("Entity set 'DeviceManagementDbContext.AlertRules'  is null.");
          }

          // TODO: functionality improvement -- make it possible to choose existing alertTemplate when creating alertRule
            _context.AlertRules.Add(alertRule);
            _context.AlertTemplates.Add(alertRule.AlertTemplate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlertRule", new { id = alertRule.Id }, alertRule);
        }

        // DELETE: api/AlertRules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlertRule(int id)
        {
            if (_context.AlertRules == null)
            {
                return NotFound();
            }
            var alertRule = await _context.AlertRules.FindAsync(id);
            if (alertRule == null)
            {
                return NotFound();
            }

            _context.AlertRules.Remove(alertRule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlertRuleExists(int id)
        {
            return (_context.AlertRules?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool AlertTemplateExists(int id)
        {
            return (_context.AlertTemplates?.Any(e => e.AlertTemplateId == id)).GetValueOrDefault();
        }
    }
}
