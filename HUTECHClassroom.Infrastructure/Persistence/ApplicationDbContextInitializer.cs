using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Domain.Enums;
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
        if (_context.Faculties.Any()
            || _context.Users.Any()
            || _context.Classrooms.Any()
            || _context.Exercises.Any()
            || _context.Answers.Any()
            || _context.Posts.Any()
            || _context.Comments.Any()
            || _context.Missions.Any()
            || _context.Projects.Any()
            || _context.Groups.Any()
            || _context.Roles.Any()) return;

        var faculties = new Faculty[]
        {
            new Faculty
            {
                Name = "Information Technology"
            },
            new Faculty
            {
                Name = "Marketing"
            },
            new Faculty
            {
                Name = "Study of Language"
            }
        };

        await _context.AddRangeAsync(faculties);

        var leader = new GroupRole("Leader");
        var member = new GroupRole("Member");

        var groupRoles = new GroupRole[]
        {
            leader,
            member
        };

        await _context.AddRangeAsync(groupRoles);

        var studentRole = new ApplicationRole("Student");
        var lecturerRole = new ApplicationRole("Lecturer");
        var deanRole = new ApplicationRole("Dean");
        var trainingOfficeRole = new ApplicationRole("TrainingOffice");
        var administratorRole = new ApplicationRole("Administrator");
        var roles = new ApplicationRole[]
        {
            studentRole,
            lecturerRole,
            deanRole,
            trainingOfficeRole,
            administratorRole
        };

        foreach (var role in roles)
        {
            await _roleManager.CreateAsync(role);
        }

        var createClaimValue = "create";
        var readClaimValue = "read";
        var updateClaimValue = "update";
        var deleteClaimValue = "delete";

        #region Mission
        var missionClaimName = "mission";

        var createMissionClaim = new Claim(missionClaimName, createClaimValue);
        var readMissionClaim = new Claim(missionClaimName, readClaimValue);
        var updateMissionClaim = new Claim(missionClaimName, updateClaimValue);
        var deleteMissionClaim = new Claim(missionClaimName, deleteClaimValue);
        #endregion

        #region Project
        var projectClaimName = "project";

        var createProjectClaim = new Claim(projectClaimName, createClaimValue);
        var readProjectClaim = new Claim(projectClaimName, readClaimValue);
        var updateProjectClaim = new Claim(projectClaimName, updateClaimValue);
        var deleteProjectClaim = new Claim(projectClaimName, deleteClaimValue);
        #endregion



        await _roleManager.AddClaimAsync(studentRole, readMissionClaim);
        await _roleManager.AddClaimAsync(studentRole, readProjectClaim);

        await _roleManager.AddClaimAsync(lecturerRole, createMissionClaim);
        await _roleManager.AddClaimAsync(lecturerRole, readMissionClaim);
        await _roleManager.AddClaimAsync(lecturerRole, updateMissionClaim);
        await _roleManager.AddClaimAsync(lecturerRole, deleteMissionClaim);

        await _roleManager.AddClaimAsync(lecturerRole, createProjectClaim);
        await _roleManager.AddClaimAsync(lecturerRole, readProjectClaim);
        await _roleManager.AddClaimAsync(lecturerRole, updateProjectClaim);
        await _roleManager.AddClaimAsync(lecturerRole, deleteProjectClaim);

        var admin = new ApplicationUser
        {
            UserName = "admin",
            FirstName = "admin",
            LastName = "admin",
            Email = "admin@gmail.com",
        };

        var users = new ApplicationUser[]
        {
            new ApplicationUser
            {
                UserName = "2080600914",
                FirstName = "Nguyễn Hồng",
                LastName = "Thái",
                Email = "thai@gmail.com",
                Faculty = faculties[0]
            },
            new ApplicationUser
            {
                UserName = "2080600803",
                FirstName = "Trương Thục",
                LastName = "Vân",
                Email = "mei@gmail.com",
                Faculty = faculties[0]
            },
            new ApplicationUser
            {
                UserName = "lecturer1",
                FirstName = "Lecturer",
                LastName = "1",
                Email = "lecturer1@gmail.com",
                Faculty = faculties[0]
            },
            new ApplicationUser
            {
                UserName = "lecturer2",
                FirstName = "Lecturer",
                LastName = "2",
                Email = "lecturer2@gmail.com",
                Faculty = faculties[0]
            },
            admin
        };


        foreach (var user in users)
        {
            await _userManager.CreateAsync(user, "P@ssw0rd").ConfigureAwait(false);
            await _userManager.AddToRoleAsync(user, studentRole.Name);
            await _userManager.AddToRoleAsync(user, lecturerRole.Name);
        }

        await _userManager.AddToRoleAsync(admin, administratorRole.Name);

        var majors = new Major[]
        {
            new Major
            {
                Code = "TH",
                Title = "Information Technology",
                TotalCredits = 152,
                NonComulativeCredits = 10
            },
            new Major
            {
                Code = "MK",
                Title = "Maketing",
                TotalCredits = 150,
                NonComulativeCredits = 10
            }
        };

        await _context.AddRangeAsync(majors);

        var subjects = new Subject[]
        {
            new Subject
            {
                Code = "CMP101",
                Title = "Subject CMP101",
                TotalCredits = 3,
                Major = majors[0]
            }
        };

        await _context.AddRangeAsync(subjects);

        var classrooms = new Classroom[]
        {
            new Classroom
            {
                Title = "Linear algebra",
                Description = "A subject",
                Topic = "Mathemetics",
                Room = "101",
                Class = "20DTHD1",
                StudyGroup = "1",
                SchoolYear = "2022",
                Semester = Semester.I,
                Type = ClassroomType.TheoryRoom,
                Lecturer = users[2],
                Subject = subjects[0],
                Faculty = faculties[0],
                ClassroomUsers = new ClassroomUser[]
                {
                    new ClassroomUser
                    {
                        User = users[0]
                    }
                }
            },
            new Classroom
            {
                Title = "English 1",
                Description = "A subject",
                Topic = "English",
                Room = "102",
                Class = "20DTHD3",
                StudyGroup = "2",
                SchoolYear = "2021",
                Semester = Semester.II,
                Type = ClassroomType.TheoryRoom,
                Lecturer = users[3],
                Faculty = faculties[0],
                ClassroomUsers = new ClassroomUser[]
                {
                    new ClassroomUser
                    {
                        User = users[0]
                    },
                    new ClassroomUser
                    {
                        User = users[1]
                    }
                }
            }
        };

        await _context.AddRangeAsync(classrooms);

        var exercises = new Exercise[]
        {
            new Exercise
            {
                Title = "Solve the problem",
                Instruction = "Suppose that L1 and L2 are lines in the plane, that the x-intercepts of L1 and L2 are 5\r\nand −1, respectively, and that the respective y-intercepts are 5 and 1. Then L1 and L2\r\nintersect at the point ( , ) .",
                Link = "google.com",
                TotalScore = 10,
                Deadline = DateTime.UtcNow.AddDays(1),
                Topic = "Mathemetics",
                Criteria = "Good: 10, Bad: 5",
                Classroom = classrooms[0],
                ExerciseUsers = new ExerciseUser[]
                {
                    new ExerciseUser
                    {
                        User = users[0]
                    },
                    new ExerciseUser
                    {
                        User = users[1]
                    }
                }
            }
        };

        await _context.AddRangeAsync(exercises);

        var answers = new Answer[]
        {
            new Answer
            {
                Description = "Sorry, I don't know T_T",
                Link = "a.com",
                Score = 0,
                Exercise = exercises[0],
                User = users[0]
            },
            new Answer
            {
                Description = "Sorry, I don't know, too T_T",
                Link = "b.com",
                Score = 0,
                Exercise = exercises[0],
                User = users[0]
            }
        };

        await _context.AddRangeAsync(answers);

        var posts = new Post[]
        {
            new Post
            {
                Content = "Hello world",
                Link = "google.com",
                User = users[0],
                Classroom = classrooms[0]
            },
            new Post
            {
                Content = "Hello!",
                Link = "yahoo.com",
                User = users[1],
                Classroom = classrooms[1]
            }
        };

        await _context.AddRangeAsync(posts);

        var comments = new Comment[]
        {
            new Comment
            {
                Content = "Hello universe ._.",
                User = users[0],
                Post = posts[0]
            },
            new Comment
            {
                Content = "Hi",
                User = users[0],
                Post = posts[1]
            }
        };

        await _context.AddRangeAsync(comments);

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
                        User = users[0],
                        GroupRole = leader
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
                        User = users[0],
                        GroupRole = leader
                    },
                    new GroupUser
                    {
                        User = users[1],
                        GroupRole = member
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
            new Mission
            {
                Title = "Let's sing",
                Description = "Sing 1 song",
                MissionUsers = new MissionUser[]
                {
                    new MissionUser
                    {
                        User = users[0],
                    }
                },
                Project = projects[1]
            },
            new Mission
            {
                Title = "Let's run",
                Description = "Run away",
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
                Project = projects[0]
            },
            new Mission
            {
                Title = "Let's think",
                Description = "Think a while",
                MissionUsers = new MissionUser[]
                {
                    new MissionUser
                    {
                        User = users[1],
                    }
                },
                Project = projects[1]
            }
        };

        await _context.AddRangeAsync(missions).ConfigureAwait(false);

        await _context.SaveChangesAsync();
    }

}
