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
            

            await _context.SaveChangesAsync();
        }

    }
}
