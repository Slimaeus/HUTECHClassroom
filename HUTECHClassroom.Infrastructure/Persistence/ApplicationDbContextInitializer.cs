using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace HUTECHClassroom.Infrastructure.Persistence
{
    public class ApplicationDbContextInitializer
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Migration error");
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Seeding error");
            }
        }

        public async Task TrySeedAsync()
        {
            if (_context.Missions.Any()) return;

            var missions = new List<Mission>
            {
                new Mission
                {
                    Title = "Let's read",
                    Description = "Read 1 book"
                },
                new Mission
                {
                    Title = "Let's write",
                    Description = "Write 1 note"
                },
                new Mission
                {
                    Title = "Let's listen",
                    Description = "Listen 1 song"
                },
            };

            await _context.AddRangeAsync(missions);

            await _context.SaveChangesAsync();
        }

    }
}
