﻿using HUTECHClassroom.Domain.Entities;
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
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Mission> Missions { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ClassroomConfiguration());
        builder.ApplyConfiguration(new ClassroomUserConfiguration());
        builder.ApplyConfiguration(new ExerciseConfiguration());
        builder.ApplyConfiguration(new ExerciseUserConfiguration());
        builder.ApplyConfiguration(new AnswerConfiguration());
        builder.ApplyConfiguration(new FacultyConfiguration());
        builder.ApplyConfiguration(new MissionConfiguration());
        builder.ApplyConfiguration(new MissionUserConfiguration());
        builder.ApplyConfiguration(new ProjectConfiguration());
        builder.ApplyConfiguration(new GroupConfiguration());
        builder.ApplyConfiguration(new GroupUserConfiguration());
        builder.ApplyConfiguration(new PostConfiguration());
        builder.ApplyConfiguration(new CommentConfiguration());
        builder.ApplyConfiguration(new ApplicationUserRoleConfiguration());
    }
}
