
    using API_FEB.Data;
    using API_FEB.Models;
    using global::API_FEB.Data;
    using global::API_FEB.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    namespace API_FEB.Services
    {
        public class CampaignService
        {
            private readonly ApplicationDbContext _context;

            public CampaignService(ApplicationDbContext context)
            {
                _context = context;
            }

            // Get all campaigns
            public async Task<IEnumerable<Campaign>> GetCampaignsAsync()
            {
                return await _context.Campaigns.ToListAsync();
            }

            // Get a specific campaign by Id
            public async Task<Campaign> GetCampaignByIdAsync(int id)
            {
                return await _context.Campaigns.FindAsync(id);
            }

            // Create a new campaign
            public async Task<Campaign> CreateCampaignAsync(Campaign campaign)
            {
                _context.Campaigns.Add(campaign);
                await _context.SaveChangesAsync();
                return campaign;
            }

            // Update an existing campaign
            public async Task<Campaign> UpdateCampaignAsync(int id, Campaign campaign)
            {
                var existingCampaign = await _context.Campaigns.FindAsync(id);
                if (existingCampaign == null) return null;

                existingCampaign.Name = campaign.Name;
                existingCampaign.Description = campaign.Description;
                existingCampaign.StartDate = campaign.StartDate;
                existingCampaign.EndDate = campaign.EndDate;
                existingCampaign.IsActive = campaign.IsActive;

                await _context.SaveChangesAsync();
                return existingCampaign;
            }

            // Delete a campaign
            public async Task<bool> DeleteCampaignAsync(int id)
            {
                var campaign = await _context.Campaigns.FindAsync(id);
                if (campaign == null) return false;

                _context.Campaigns.Remove(campaign);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }


