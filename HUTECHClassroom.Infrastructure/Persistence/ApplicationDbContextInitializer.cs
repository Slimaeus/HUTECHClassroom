using HUTECHClassroom.Domain.Claims;
using HUTECHClassroom.Domain.Constants;
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

        var studentRole = new ApplicationRole(RoleConstants.STUDENT);
        var lecturerRole = new ApplicationRole(RoleConstants.LECTURER);
        var deanRole = new ApplicationRole(RoleConstants.DEAN);
        var trainingOfficeRole = new ApplicationRole(RoleConstants.TRAINING_OFFICE);
        var administratorRole = new ApplicationRole(RoleConstants.ADMIN);
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
        var missionClaimName = ApplicationClaimTypes.MISSION;

        var createMissionClaim = new Claim(missionClaimName, createClaimValue);
        var readMissionClaim = new Claim(missionClaimName, readClaimValue);
        var updateMissionClaim = new Claim(missionClaimName, updateClaimValue);
        var deleteMissionClaim = new Claim(missionClaimName, deleteClaimValue);
        #endregion

        #region Project
        var projectClaimName = ApplicationClaimTypes.PROJECT;

        var createProjectClaim = new Claim(projectClaimName, createClaimValue);
        var readProjectClaim = new Claim(projectClaimName, readClaimValue);
        var updateProjectClaim = new Claim(projectClaimName, updateClaimValue);
        var deleteProjectClaim = new Claim(projectClaimName, deleteClaimValue);
        #endregion

        #region Classroom
        var classroomClaimName = ApplicationClaimTypes.CLASSROOM;

        var createClassroomClaim = new Claim(classroomClaimName, createClaimValue);
        var readClassroomClaim = new Claim(classroomClaimName, readClaimValue);
        var updateClassroomClaim = new Claim(classroomClaimName, updateClaimValue);
        var deleteClassroomClaim = new Claim(classroomClaimName, deleteClaimValue);
        #endregion


        #region Student Role
        await _roleManager.AddClaimAsync(studentRole, readMissionClaim);
        await _roleManager.AddClaimAsync(studentRole, readProjectClaim);
        #endregion

        #region Lecturer Role
        await _roleManager.AddClaimAsync(lecturerRole, createMissionClaim);
        await _roleManager.AddClaimAsync(lecturerRole, readMissionClaim);
        await _roleManager.AddClaimAsync(lecturerRole, updateMissionClaim);
        await _roleManager.AddClaimAsync(lecturerRole, deleteMissionClaim);

        await _roleManager.AddClaimAsync(lecturerRole, createProjectClaim);
        await _roleManager.AddClaimAsync(lecturerRole, readProjectClaim);
        await _roleManager.AddClaimAsync(lecturerRole, updateProjectClaim);
        await _roleManager.AddClaimAsync(lecturerRole, deleteProjectClaim);
        #endregion

        var admin = new ApplicationUser
        {
            UserName = "admin",
            FirstName = "admin",
            LastName = "admin",
            Email = "admin@gmail.com",
        };

        var dean = new ApplicationUser
        {
            UserName = "dean",
            FirstName = "dean",
            LastName = "dean",
            Email = "dean@gmail.com",
        };

        var trainingOffice = new ApplicationUser
        {
            UserName = "trainingOffice",
            FirstName = "trainingOffice",
            LastName = "trainingOffice",
            Email = "trainingOffice@gmail.com",
        };

        var lecturer1 = new ApplicationUser
        {
            UserName = "lecturer1",
            FirstName = "1",
            LastName = "Giảng viên",
            Email = "lecturer1@gmail.com",
            Faculty = faculties[0]
        };

        var lecturer2 = new ApplicationUser
        {
            UserName = "lecturer2",
            FirstName = "2",
            LastName = "Giảng viên",
            Email = "lecturer2@gmail.com",
            Faculty = faculties[0]
        };

        var users = new ApplicationUser[]
        {
            new ApplicationUser
            {
                UserName = "2080600914",
                FirstName = "Thái",
                LastName = "Nguyễn Hồng",
                Email = "thai@gmail.com",
                Faculty = faculties[0]
            },
            new ApplicationUser
            {
                UserName = "2080600803",
                FirstName = "Vân",
                LastName = "Trương Thục",
                Email = "mei@gmail.com",
                Faculty = faculties[0]
            }
        };


        foreach (var user in users)
        {
            await _userManager.CreateAsync(user, "P@ssw0rd").ConfigureAwait(false);
            await _userManager.AddToRoleAsync(user, studentRole.Name);
            await _userManager.AddToRoleAsync(user, lecturerRole.Name);
        }

        await _userManager.CreateAsync(admin, "P@ssw0rd").ConfigureAwait(false);
        await _userManager.AddToRoleAsync(admin, administratorRole.Name);

        await _userManager.CreateAsync(dean, "P@ssw0rd").ConfigureAwait(false);
        await _userManager.AddToRoleAsync(dean, deanRole.Name);

        await _userManager.CreateAsync(trainingOffice, "P@ssw0rd").ConfigureAwait(false);
        await _userManager.AddToRoleAsync(trainingOffice, trainingOfficeRole.Name);

        await _userManager.CreateAsync(lecturer1, "P@ssw0rd").ConfigureAwait(false);
        await _userManager.AddToRoleAsync(lecturer1, lecturerRole.Name);

        await _userManager.CreateAsync(lecturer2, "P@ssw0rd").ConfigureAwait(false);
        await _userManager.AddToRoleAsync(lecturer2, lecturerRole.Name);


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
                Title = "Đại số tuyến tính",
                Description = "Môn đại số",
                Topic = "Toán học",
                Room = "101",
                StudyPeriod = "1/1/2022 - 1/1/2023",
                Class = "20DTHD1",
                StudyGroup = "1",
                SchoolYear = "2022",
                Semester = Semester.I,
                Type = ClassroomType.TheoryRoom,
                Lecturer = lecturer1,
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
                Title = "Tiếng Anh 1",
                Description = "Môn học",
                Topic = "Tiếng Anh",
                StudyPeriod = "1/1/2022 - 1/1/2023",
                Room = "102",
                Class = "20DTHD3",
                StudyGroup = "2",
                SchoolYear = "2021",
                Semester = Semester.II,
                Type = ClassroomType.TheoryRoom,
                Lecturer = lecturer2,
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
                Title = "Giải bài toán",
                Instruction = "62 - 32 = ?",
                Link = "google.com",
                TotalScore = 10,
                Deadline = DateTime.UtcNow.AddDays(1),
                Topic = "Toán học",
                Criteria = "Tốt: 10, Tệ: 5",
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
                Description = "Xin lỗi em không biết làm T_T",
                Link = "a.com",
                Score = 0,
                Exercise = exercises[0],
                User = users[0]
            },
            new Answer
            {
                Description = "Xin lỗi em cũng không biết làm ạ T_T",
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
                Content = "Xin chào thế giới",
                Link = "google.com",
                User = users[0],
                Classroom = classrooms[0]
            },
            new Post
            {
                Content = "Xin chào!",
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
                Content = "Xin chào vũ trụ ._.",
                User = users[0],
                Post = posts[0]
            },
            new Comment
            {
                Content = "Chào",
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
                Description = "Nhóm",
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
                Description = "Nửa hộp",
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
                Description = "Hệ thống quản lý nhóm và dự án",
                Group = groups[0]
            },
            new Project
            {
                Name = "HUTECH Classroom",
                Description = "Hệ thống quản lý lớp học",
                Group = groups[1]
            }
        };

        await _context.AddRangeAsync(projects).ConfigureAwait(false);

        var missions = new Mission[]
        {
            new Mission
            {
                Title = "Hãy đọc",
                Description = "Đọc 1 quyển sách",
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
                Title = "Hãy viết",
                Description = "Viết 1 quyển sách",
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
                Title = "Hãy nghe",
                Description = "Nghe 1 bài hát",
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
                Title = "Hãy hát",
                Description = "Hát 1 bài hát",
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
                Title = "Hãy chạy",
                Description = "Chạy đi!",
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
                Title = "Hãy nghĩ",
                Description = "Nghĩ một lát",
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
