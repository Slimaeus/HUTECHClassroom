using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
    IdentityUserClaim<Guid>,
    ApplicationUserRole,
    IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    //public DbSet<Major> Majors { get; set; }
    //public DbSet<Subject> Subjects { get; set; }
    public DbSet<Mission> Missions { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<GroupRole> GroupRoles { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ClassroomConfiguration())
               .ApplyConfiguration(new ClassroomUserConfiguration())
               .ApplyConfiguration(new ExerciseConfiguration())
               .ApplyConfiguration(new ExerciseUserConfiguration())
               .ApplyConfiguration(new AnswerConfiguration())
               .ApplyConfiguration(new FacultyConfiguration())
               //.ApplyConfiguration(new MajorConfiguration())
               //.ApplyConfiguration(new SubjectConfiguration())
               .ApplyConfiguration(new MissionConfiguration())
               .ApplyConfiguration(new MissionUserConfiguration())
               .ApplyConfiguration(new ProjectConfiguration())
               .ApplyConfiguration(new GroupConfiguration())
               .ApplyConfiguration(new GroupUserConfiguration())
               .ApplyConfiguration(new PostConfiguration())
               .ApplyConfiguration(new CommentConfiguration())
               .ApplyConfiguration(new ApplicationUserRoleConfiguration())
               .ApplyConfiguration(new GroupRoleConfiguration())
               .ApplyConfiguration(new GroupRoleClaimConfiguration());
    }
}
