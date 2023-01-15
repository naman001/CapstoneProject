using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicAPI.Models;

namespace ClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservedSchedulesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservedSchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReservedSchedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservedSchedule>>> GetReservedSchedules()
        {
            return await _context.ReservedSchedules.ToListAsync();
        }

        // GET: api/ReservedSchedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservedSchedule>> GetReservedSchedule(int id)
        {
            var reservedSchedule = await _context.ReservedSchedules.FindAsync(id);

            if (reservedSchedule == null)
            {
                return NotFound();
            }

            return reservedSchedule;
        }

        // PUT: api/ReservedSchedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservedSchedule(int id, ReservedSchedule reservedSchedule)
        {
            if (id != reservedSchedule.ScheduleId)
            {
                return BadRequest();
            }

            _context.Entry(reservedSchedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservedScheduleExists(id))
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

        // POST: api/ReservedSchedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Check if there is a booked slot in DB system will not create, otherwise it will create new booked slot.
        [HttpPost]
        public IActionResult PostReservedSchedule(ReservedSchedule reservedSchedule)
        {
            var list = GetByDocId(reservedSchedule.DocId);
            var booked = list.Result.Value.FirstOrDefault(x => x.Start == reservedSchedule.Start);
            if (booked != null)
                return NoContent();
            _context.ReservedSchedules.Add(reservedSchedule);
            _context.SaveChangesAsync();
            return CreatedAtAction("GetReservedSchedule", new { id = reservedSchedule.ScheduleId }, reservedSchedule);
        }

        // DELETE: api/ReservedSchedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservedSchedule(int id)
        {
            var reservedSchedule = await _context.ReservedSchedules.FindAsync(id);
            if (reservedSchedule == null)
            {
                return NotFound();
            }

            _context.ReservedSchedules.Remove(reservedSchedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //Get a work schedule of a doctor 
        [HttpGet("DocId/{DocId}")]
        public async Task<ActionResult<IEnumerable<ReservedSchedule>>> GetByDocId(int DocId)
        {
            var reservedSchedule = _context.ReservedSchedules.Where(x=>x.DocId == DocId);
            if (reservedSchedule == null)
            {
                return NotFound();
            }
            return await reservedSchedule.ToListAsync();
        }
        private bool ReservedScheduleExists(int id)
        {
            return _context.ReservedSchedules.Any(e => e.ScheduleId == id);
        }
    }
}
