using HUTECHClassroom.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace HUTECHClassroom.Infrastructure.Persistence
{
    public class ApplicationDbContextInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationDbContextInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            if (_context.Users.Any() || _context.Missions.Any()) return;

            var users = new ApplicationUser[]
            {
                new ApplicationUser
                {
                    UserName = "2080600914",
                    Email = "thai@gmail.com"
                },
                new ApplicationUser
                {
                    UserName = "2080600803",
                    Email = "mei@gmail.com"
                }
            };

            foreach (var user in users)
            {
                await _userManager.CreateAsync(user, "P@ssw0rd");
            }

            var missions = new Mission[]
            {
                new Mission
                {
                    Title = "Let's read",
                    Description = "Read 1 book",
                    MissionUsers = new MissionUser[]
                    {
                        new MissionUser
                        {
                            User = users[0],
                        }
                    }
                },
                new Mission
                {
                    Title = "Let's write",
                    Description = "Write 1 note",
                    MissionUsers = new MissionUser[]
                    {
                        new MissionUser
                        {
                            User = users[1],
                        }
                    }
                },
                new Mission
                {
                    Title = "Let's listen",
                    Description = "Listen 1 song",
                    MissionUsers = new MissionUser[]
                    {
                        new MissionUser
                        {
                            User = users[0],
                        },
                        new MissionUser
                        {
                            User = users[1],
                        }
                    }
                },
            };

            await _context.AddRangeAsync(missions);

            await _context.SaveChangesAsync();
        }

    }
}
