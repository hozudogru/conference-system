using ConferenceSystem.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Services
{
    public class SiteSettingsService
    {
        private readonly AppDbContext _context;

        public SiteSettingsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetValueAsync(string key, string defaultValue = "")
        {
            var value = await _context.SiteSettings
                .Where(x => x.Key == key)
                .Select(x => x.Value)
                .FirstOrDefaultAsync();

            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }

        public async Task SetValueAsync(string key, string value)
        {
            var setting = await _context.SiteSettings.FirstOrDefaultAsync(x => x.Key == key);
            if (setting == null)
            {
                _context.SiteSettings.Add(new Entities.SiteSetting { Key = key, Value = value });
            }
            else
            {
                setting.Value = value;
            }

            await _context.SaveChangesAsync();
        }
    }
}
