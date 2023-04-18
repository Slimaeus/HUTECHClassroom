using HUTECHClassroom.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;

namespace HUTECHClassroom.Infrastructure.Persistence;

public class ApplicationDbContextInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public ApplicationDbContextInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
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
        if (_context.Users.Any() || _context.Classrooms.Any() || _context.Missions.Any() || _context.Projects.Any() || _context.Groups.Any() || _context.Roles.Any()) return;

        var studentRole = new ApplicationRole("Student");
        var roles = new ApplicationRole[6]
        {
            new ApplicationRole("Administrator"),
            new ApplicationRole("TrainingOffice"),
            new ApplicationRole("Dean"),
            new ApplicationRole("Lecturer"),
            new ApplicationRole("Leader"),
            studentRole
        };


        foreach (var role in roles)
        {
            await _roleManager.CreateAsync(role);
        }

        var readMission = new Claim("mission", "read");
        await _roleManager.AddClaimAsync(studentRole, readMission);

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
            },
            new ApplicationUser
            {
                UserName = "lecturer1",
                Email = "lecturer1@gmail.com"
            },
            new ApplicationUser
            {
                UserName = "lecturer2",
                Email = "lecturer2@gmail.com"
            }
        };

        foreach (var user in users)
        {
            await _userManager.CreateAsync(user, "P@ssw0rd").ConfigureAwait(false);
            await _userManager.AddToRoleAsync(user, roles[5].Name);
        }

        var classrooms = new Classroom[]
        {
            new Classroom
            {
                Title = "Linear algebra",
                Description = "A subject",
                Topic = "Mathemetics",
                Room = "101",
                Lecturer = users[2],
            },
            new Classroom
            {
                Title = "English 1",
                Description = "A subject",
                Topic = "English",
                Room = "102",
                Lecturer = users[3],
            }
        };

        var groups = new Group[]
        {
            new Group
            {
                Name = "Owlvernyte",
                Description = "Owls group",
                Leader = users[0],
                GroupUsers = new GroupUser[]
                {
                    new GroupUser
                    {
                        User = users[0]
                    }
                },
                Classroom = classrooms[0]
            },
            new Group
            {
                Name = "Semibox",
                Description = "Half of a box",
                Leader = users[1],
                GroupUsers = new GroupUser[]
                {
                    new GroupUser
                    {
                        User = users[0]
                    },
                    new GroupUser
                    {
                        User = users[1]
                    }
                },
                Classroom = classrooms[1]
            }
        };

        await _context.AddRangeAsync(groups).ConfigureAwait(false);

        var projects = new Project[]
        {
            new Project
            {
                Name = "Plan together",
                Description = "Projects, Groups Management system",
                Group = groups[0]
            },
            new Project
            {
                Name = "HUTECH Classroom",
                Description = "Classroom, Students, Lecturers... Management system",
                Group = groups[1]
            }
        };

        await _context.AddRangeAsync(projects).ConfigureAwait(false);

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
                },
                Project = projects[0]
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
                },
                Project = projects[1]
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
                },
                Project = projects[1]
            },
        };

        await _context.AddRangeAsync(missions).ConfigureAwait(false);

        await _context.SaveChangesAsync();
    }

}
