using System.Collections.Generic;
using System.Threading.Tasks;
using API_FEB.Data;
using API_FEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_FEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CampaignsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Get all campaigns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campaign>>> GetCampaigns()
        {
            return await _context.Campaigns.ToListAsync();
        }

        // ✅ Get a single campaign by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Campaign>> GetCampaign(int id)
        {
            var campaign = await _context.Campaigns.FindAsync(id);

            if (campaign == null)
            {
                return NotFound();
            }

            return campaign;
        }

        // ✅ Create a new campaign
        [HttpPost]
        public async Task<ActionResult<Campaign>> CreateCampaign(Campaign campaign)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Campaigns.Add(campaign);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCampaign), new { id = campaign.Id }, campaign);
        }

        // ✅ Update an existing campaign
        [HttpPut("{id}")]
        public IActionResult UpdateCampaign(int id, [FromBody] Campaign updatedCampaign)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCampaign = _context.Campaigns.Find(id);
            if (existingCampaign == null)
            {
                return NotFound();
            }

            // Update properties
            existingCampaign.Name = updatedCampaign.Name;
            existingCampaign.Description = updatedCampaign.Description;
            existingCampaign.StartDate = updatedCampaign.StartDate;
            existingCampaign.EndDate = updatedCampaign.EndDate;
            existingCampaign.IsActive = updatedCampaign.IsActive;

            _context.SaveChanges();

            return Ok(existingCampaign);
        }

        // ✅ Delete a campaign
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampaign(int id)
        {
            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }

            _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
