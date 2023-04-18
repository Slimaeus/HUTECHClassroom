using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Mission> Missions { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Group> Groups { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ClassroomConfiguration());
        builder.ApplyConfiguration(new ClassroomUserConfiguration());
        builder.ApplyConfiguration(new FacultyConfiguration());
        builder.ApplyConfiguration(new MissionConfiguration());
        builder.ApplyConfiguration(new MissionUserConfiguration());
        builder.ApplyConfiguration(new ProjectConfiguration());
        builder.ApplyConfiguration(new GroupConfiguration());
        builder.ApplyConfiguration(new GroupUserConfiguration());
    }
}
