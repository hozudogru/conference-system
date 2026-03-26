using ConferenceSystem.Web.Data;
using ConferenceSystem.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferenceSystem.Web.Services
{
    public class SmtpSettingsService
    {
        private readonly AppDbContext _context;

        public SmtpSettingsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SmtpSetting?> GetAsync()
        {
            return await _context.SmtpSettings
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<SmtpSetting> GetOrCreateAsync()
        {
            var item = await _context.SmtpSettings
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (item != null)
                return item;

            item = new SmtpSetting
            {
                Host = "",
                Port = 587,
                UserName = "",
                Password = "",
                FromEmail = "",
                FromName = "",
                UseSsl = false,
                SecurityMode = "Auto",
                RequireAuthentication = true,
                TimeoutMilliseconds = 20000,
                UpdatedAt = DateTime.Now
            };

            _context.SmtpSettings.Add(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task SaveAsync(SmtpSetting model)
        {
            var existing = await _context.SmtpSettings
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (existing == null)
            {
                model.UpdatedAt = DateTime.Now;
                _context.SmtpSettings.Add(model);
            }
            else
            {
                existing.Host = model.Host;
                existing.Port = model.Port;
                existing.UserName = model.UserName;
                existing.Password = model.Password;
                existing.FromEmail = model.FromEmail;
                existing.FromName = model.FromName;
                existing.UseSsl = model.UseSsl;
                existing.SecurityMode = model.SecurityMode;
                existing.RequireAuthentication = model.RequireAuthentication;
                existing.TimeoutMilliseconds = model.TimeoutMilliseconds;
                existing.UpdatedAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();
        }
    }
}