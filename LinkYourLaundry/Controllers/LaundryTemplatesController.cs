using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinkYourLaundry;
using LinkYourLaundry.Models;
using Microsoft.AspNetCore.Authorization;

namespace LinkYourLaundry.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LaundryTemplatesController : BaseController
    {
        private readonly LaundryDbContext _context;

        public LaundryTemplatesController(LaundryDbContext context)
        {
            _context = context;
        }

        // GET: api/LaundryTemplates
        [HttpGet]
        public IEnumerable<LaundryTemplate> GetLaundryTemplates()
        {
            return _context.LaundryTemplates;
        }

        // GET: api/LaundryTemplates/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLaundryTemplate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var laundryTemplate = await _context.LaundryTemplates.FindAsync(id);

            if (laundryTemplate == null)
            {
                return NotFound();
            }

            return Ok(laundryTemplate);
        }

        // PUT: api/LaundryTemplates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLaundryTemplate([FromRoute] int id, [FromBody] LaundryTemplate laundryTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != laundryTemplate.Id)
            {
                return BadRequest();
            }

            _context.Entry(laundryTemplate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LaundryTemplateExists(id))
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

        // POST: api/LaundryTemplates
        [HttpPost]
        public async Task<IActionResult> PostLaundryTemplate([FromBody] LaundryTemplate laundryTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.LaundryTemplates.Add(laundryTemplate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLaundryTemplate", new { id = laundryTemplate.Id }, laundryTemplate);
        }

        // DELETE: api/LaundryTemplates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLaundryTemplate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var laundryTemplate = await _context.LaundryTemplates.FindAsync(id);
            if (laundryTemplate == null)
            {
                return NotFound();
            }

            _context.LaundryTemplates.Remove(laundryTemplate);
            await _context.SaveChangesAsync();

            return Ok(laundryTemplate);
        }

        private bool LaundryTemplateExists(int id)
        {
            return _context.LaundryTemplates.Any(e => e.Id == id);
        }
    }
}