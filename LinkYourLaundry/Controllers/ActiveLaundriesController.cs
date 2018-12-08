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
using LinkYourLaundry.ViewModels;

namespace LinkYourLaundry.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ActiveLaundriesController : BaseController
    {
        private readonly LaundryDbContext _context;

        public ActiveLaundriesController(LaundryDbContext context)
        {
            _context = context;
        }

        // GET: api/ActiveLaundries
        [HttpGet]
        public IEnumerable<ActiveLaundry> GetActiveLaundries()
        {
            return _context.ActiveLaundries;
        }

        // GET: api/ActiveLaundries/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActiveLaundry([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activeLaundry = await _context.ActiveLaundries.FindAsync(id);

            if (activeLaundry == null)
            {
                return NotFound();
            }

            return Ok(activeLaundry);
        }

        // PUT: api/ActiveLaundries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActiveLaundry([FromRoute] int id, [FromBody] ActiveLaundry activeLaundry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activeLaundry.Id)
            {
                return BadRequest();
            }

            _context.Entry(activeLaundry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActiveLaundryExists(id))
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

        // POST: api/ActiveLaundries
        [HttpPost]
        public async Task<IActionResult> PostActiveLaundry([FromBody] CreateActiveLaundryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activeLaundry = new ActiveLaundry
            {
                UserId = GetCurrentUserId(),
                LaundryTemplateId = viewModel.LaundryTemplateId,
                WashStartTime = viewModel.WashStartTime,
                Completed = false
            };

            _context.ActiveLaundries.Add(activeLaundry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetActiveLaundry), new { id = activeLaundry.Id }, activeLaundry);
        }

        // DELETE: api/ActiveLaundries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActiveLaundry([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activeLaundry = await _context.ActiveLaundries.FindAsync(id);
            if (activeLaundry == null)
            {
                return NotFound();
            }

            _context.ActiveLaundries.Remove(activeLaundry);
            await _context.SaveChangesAsync();

            return Ok(activeLaundry);
        }

        private bool ActiveLaundryExists(int id)
        {
            return _context.ActiveLaundries.Any(e => e.Id == id);
        }
    }
}