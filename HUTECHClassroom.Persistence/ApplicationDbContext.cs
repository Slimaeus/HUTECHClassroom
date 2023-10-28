using HUTECHClassroom.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HUTECHClassroom.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
    IdentityUserClaim<Guid>,
    ApplicationUserRole,
    IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Class> Classes { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Major> Majors { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Mission> Missions { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<GroupRole> GroupRoles { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<ScoreType> ScoreTypes { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
