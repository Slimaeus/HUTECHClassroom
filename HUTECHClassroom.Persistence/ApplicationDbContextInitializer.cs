using HUTECHClassroom.Domain.Claims;
using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;

namespace HUTECHClassroom.Persistence;

public sealed class ApplicationDbContextInitializer
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
            || _context.Roles.Any()
            || _context.ScoreTypes.Any()) return;

        var faculties = new List<Faculty>
        {
            new Faculty
            {
                Name = "Công nghệ thông tin"
            },
            new Faculty
            {
                Name = "Marketing - Kinh doanh Quốc tế"
            }
        };

        await _context.AddRangeAsync(faculties);

        var leader = new GroupRole("Leader");
        var member = new GroupRole("Member");

        var groupRoles = new List<GroupRole>
        {
            leader,
            member
        };

        await _context.AddRangeAsync(groupRoles);

        var studentRole = new ApplicationRole(RoleConstants.Student);
        var lecturerRole = new ApplicationRole(RoleConstants.Lecturer);
        var deanRole = new ApplicationRole(RoleConstants.Dean);
        var trainingOfficeRole = new ApplicationRole(RoleConstants.TrainingOffice);
        var administratorRole = new ApplicationRole(RoleConstants.Administrator);
        var departmentSecretaryRole = new ApplicationRole(RoleConstants.DepartmentSecretary);
        var roles = new List<ApplicationRole>
        {
            studentRole,
            lecturerRole,
            deanRole,
            trainingOfficeRole,
            administratorRole,
            departmentSecretaryRole
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
        //var classroomClaimName = ApplicationClaimTypes.CLASSROOM;

        //var createClassroomClaim = new Claim(classroomClaimName, createClaimValue);
        //var readClassroomClaim = new Claim(classroomClaimName, readClaimValue);
        //var updateClassroomClaim = new Claim(classroomClaimName, updateClaimValue);
        //var deleteClassroomClaim = new Claim(classroomClaimName, deleteClaimValue);
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

        var classes = new List<Class>
        {
            new Class
            {
                Name = "20DTHD1"
            },
            new Class
            {
                Name = "20DTHD3"
            },
            new Class
            {
                Name = "20DTHD4"
            }
        };

        await _context.AddRangeAsync(classes);
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

        var departmentSecretary = new ApplicationUser
        {
            UserName = "departmentSecretary",
            FirstName = "departmentSecretary",
            LastName = "departmentSecretary",
            Email = "departmentSecretary@gmail.com",
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

        var users = new List<ApplicationUser>
        {
            new() {
                UserName = "2080600914",
                FirstName = "Thái",
                LastName = "Nguyễn Hồng",
                Email = "thai@gmail.com",
                Faculty = faculties[0],
                Class = classes[1]
            },
            new() {
                UserName = "2080600803",
                FirstName = "Vân",
                LastName = "Trương Thục",
                Email = "mei@gmail.com",
                Faculty = faculties[0],
                Class = classes[1]
            },
            new() {
                UserName = "2080600413",
                FirstName = "Kiên",
                LastName = "Phan Thị",
                Email = "2080600413@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[2]
            },
            new() {
                UserName = "2080600425",
                FirstName = "Lâm",
                LastName = "Nguyễn Bá Gia",
                Email = "2080600425@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[1]
            },
            new() {
                UserName = "2080600885",
                FirstName = "Loan",
                LastName = "Nguyễn Thị",
                Email = "2080600885@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[2]
            },
            new() {
                UserName = "2080600442",
                FirstName = "Long",
                LastName = "Lâm Hoàng",
                Email = "2080600442@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[1]
            },
            new() {
                UserName = "2080600446",
                FirstName = "Long",
                LastName = "Nguyễn Phước",
                Email = "2080600446@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[1]
            },
            new() {
                UserName = "2011061661",
                FirstName = "Lộc",
                LastName = "Mông Văn",
                Email = "2011061661@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[1]
            },
            new() {
                UserName = "2080600453",
                FirstName = "Lộc",
                LastName = "Nguyễn Văn",
                Email = "2080600453@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[2]
            },
            new() {
                UserName = "2080600456",
                FirstName = "Lỗi",
                LastName = "Tạ Thạch",
                Email = "2080600456@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[2]
            },
            new() {
                UserName = "2080600462",
                FirstName = "Lực",
                LastName = "Lê Trần Tấn",
                Email = "2080600462@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[1]
            },
            new() {
                UserName = "2080600476",
                FirstName = "Minh",
                LastName = "Nguyễn Thái Quang",
                Email = "2080600476@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[1]
            },
            new() {
                UserName = "2080600477",
                FirstName = "Minh",
                LastName = "Nguyễn Thế",
                Email = "2080600477@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[1]
            },
            new() {
                UserName = "2011063219",
                FirstName = "Nam",
                LastName = "Vũ Đức Hoài",
                Email = "2011063219@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[1]
            },
            new() {
                UserName = "2080600518",
                FirstName = "Nguyên",
                LastName = "Hoàng Nguyễn Quốc",
                Email = "2080600518@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[1]
            },
            new() {
                UserName = "2080600532",
                FirstName = "Nhân",
                LastName = "Phan Thành",
                Email = "2080600532@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[1]
            },
            new() {
                UserName = "2011060750",
                FirstName = "Nhi",
                LastName = "Nguyễn Hoàng",
                Email = "2011060750@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[1]
            },
            new() {
                UserName = "2080600539",
                FirstName = "Nhi",
                LastName = "Phạm Võ Linh",
                Email = "2080600539@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[2]
            },
            new() {
                UserName = "2080600542",
                FirstName = "Nhơn",
                LastName = "Võ Thương Trường",
                Email = "2080600542@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[2]
            },
            new() {
                UserName = "2080600546",
                FirstName = "Nhựt",
                LastName = "Trần Minh",
                Email = "2080600546@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[2]
            },
            new() {
                UserName = "2080600099",
                FirstName = "Phát",
                LastName = "Nguyễn Tấn",
                Email = "2080600099@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[2]
            },
            new() {
                UserName = "2080600577",
                FirstName = "Phúc",
                LastName = "Phạm Bảo",
                Email = "2080600577@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[1]
            },
            new() {
                UserName = "2011064979",
                FirstName = "Phương",
                LastName = "Nguyễn Duy",
                Email = "2011064979@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[2]
            },
            new() {
                UserName = "2080600907",
                FirstName = "Sang",
                LastName = "Trần Thanh",
                Email = "2080600907@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[2]
            },
            new() {
                UserName = "2080600636",
                FirstName = "Tài",
                LastName = "Hồ Ngọc",
                Email = "2080600636@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[2]
            },
            new() {
                UserName = "2080600639",
                FirstName = "Tài",
                LastName = "Phạm Đức",
                Email = "2080600639@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[2]
            },
            new() {
                UserName = "2080600648",
                FirstName = "Tân",
                LastName = "Đỗ Duy",
                Email = "2080600648@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[2]
            },
            new() {
                UserName = "2080600652",
                FirstName = "Thái",
                LastName = "Dương Hoàng",
                Email = "2080600652@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[1]
            },
            new() {
                UserName = "2011064299",
                FirstName = "Thái",
                LastName = "Hoàng Quốc",
                Email = "2011064299@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[1]
            },
            new() {
                UserName = "2011065005",
                FirstName = "Thái",
                LastName = "Phạm Hông",
                Email = "2011065005@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[2]
            },
            new() {
                UserName = "2011069022",
                FirstName = "Thanh",
                LastName = "Lê Chế",
                Email = "2011069022@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[1]
            },
            new() {
                UserName = "2080600663",
                FirstName = "Thanh",
                LastName = "Nguyễn Ngọc",
                Email = "2080600663@hutech.edu.vn",
                Faculty = faculties[0],
                Class = classes[2]
            },
        };

        var password = "P@ssw0rd";

        foreach (var user in users)
        {
            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, studentRole.Name!);
            await _userManager.AddToRoleAsync(user, lecturerRole.Name!);
        }

        await _userManager.CreateAsync(admin, password);
        await _userManager.AddToRoleAsync(admin, administratorRole.Name!);

        await _userManager.CreateAsync(dean, password);
        await _userManager.AddToRoleAsync(dean, deanRole.Name!);

        await _userManager.CreateAsync(trainingOffice, password);
        await _userManager.AddToRoleAsync(trainingOffice, trainingOfficeRole.Name!);

        await _userManager.CreateAsync(departmentSecretary, password);
        await _userManager.AddToRoleAsync(departmentSecretary, departmentSecretaryRole.Name!);

        await _userManager.CreateAsync(lecturer1, password);
        await _userManager.AddToRoleAsync(lecturer1, lecturerRole.Name!);

        await _userManager.CreateAsync(lecturer2, password);
        await _userManager.AddToRoleAsync(lecturer2, lecturerRole.Name!);


        var majors = new List<Major>
        {
            new Major
            {
                Code = "TH",
                Title = "Công nghệ thông tin",
                TotalCredits = 152,
                NonComulativeCredits = 5
            },
            new Major
            {
                Code = "MARQT",
                Title = "Marketing - Kinh doanh Quốc tế",
                TotalCredits = 137,
                NonComulativeCredits = 5
            }
        };

        await _context.AddRangeAsync(majors);

        var subjects = new List<Subject>
        {
            new Subject
            {
                Code = "CMP101",
                Title = "Subject CMP101",
                TotalCredits = 3,
                Major = majors[0]
            },
            new Subject
            {
                Code = "CMP1024",
                Title = "Lập trình ứng dụng với Java",
                TotalCredits = 3,
                Major = majors[0]
            }
        };

        await _context.AddRangeAsync(subjects);

        var scoreTypes = new List<ScoreType>
        {
            new ScoreType
            {
                Id = ScoreTypeConstants.MidtermScoreId,
                Name = "Điểm quá trình"
            },
            new ScoreType
            {
                Id = ScoreTypeConstants.FinalScoreId,
                Name = "Điểm cuối kỳ"
            }
        };

        await _context.AddRangeAsync(scoreTypes);

        var class2StudentResults = users
            .Select((x, i) => new StudentResult { Student = x, ScoreType = scoreTypes[0], OrdinalNumber = i + 1, Score = new Random().Next(1, 11) }).ToList();

        class2StudentResults.AddRange(users.Select((x, i) => new StudentResult { Student = x, ScoreType = scoreTypes[1], OrdinalNumber = i + 1, Score = new Random().Next(1, 11) }).ToList());

        var classrooms = new List<Classroom>
        {
            new Classroom
            {
                Title = "Đại số tuyến tính",
                Description = "Môn đại số",
                Topic = "Toán học",
                Room = "101",
                StudyPeriod = "1/1/2022 - 1/1/2023",
                Class = classes[0],
                StudyGroup = "1",
                SchoolYear = "2022",
                Semester = Semester.I,
                Type = ClassroomType.TheoryRoom,
                Lecturer = lecturer1,
                Subject = subjects[0],
                Faculty = faculties[0],
                ClassroomUsers = new List<ClassroomUser>
                {
                    new ClassroomUser
                    {
                        User = users[0]
                    }
                },
                StudentResults = new List<StudentResult>
                {
                    new StudentResult
                    {
                        Student = users[0],
                        ScoreType = scoreTypes[0],
                        Score = 8
                    },
                    new StudentResult
                    {
                        Student = users[0],
                        ScoreType = scoreTypes[1],
                        Score = 9
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
                Class = classes[1],
                StudyGroup = "2",
                SchoolYear = "2021",
                Semester = Semester.II,
                Type = ClassroomType.TheoryRoom,
                Lecturer = lecturer2,
                Faculty = faculties[0],
                ClassroomUsers = new List<ClassroomUser>
                {
                    new ClassroomUser
                    {
                        User = users[0]
                    },
                    new ClassroomUser
                    {
                        User = users[1]
                    }
                },
                StudentResults = new List<StudentResult>
                {
                    new StudentResult
                    {
                        Student = users[0],
                        ScoreType = scoreTypes[0],
                        OrdinalNumber = 1,
                        Score = 9
                    },
                    new StudentResult
                    {
                        Student = users[0],
                        ScoreType = scoreTypes[1],
                        OrdinalNumber = 1,
                        Score = 10
                    },
                    new StudentResult
                    {
                        Student = users[1],
                        ScoreType = scoreTypes[0],
                        OrdinalNumber = 2,
                        Score = 10
                    },
                    new StudentResult
                    {
                        Student = users[1],
                        ScoreType = scoreTypes[1],
                        OrdinalNumber = 2,
                        Score = 10
                    }
                }
            },
            new Classroom
            {
                Title = subjects[1].Title,
                Description = "Môn học",
                Topic = "Lập trình",
                StudyPeriod = "1/1/2022 - 1/1/2023",
                Room = "102",
                Class = classes[1],
                StudyGroup = "20",
                SchoolYear = "2023",
                Semester = Semester.II,
                Type = ClassroomType.TheoryRoom,
                Subject = subjects[1],
                Lecturer = lecturer2,
                Faculty = faculties[0],
                ClassroomUsers = users.Select(x => new ClassroomUser { User = x }).ToList(),
                StudentResults = class2StudentResults,

            }
        };

        await _context.AddRangeAsync(classrooms);

        var exercises = new List<Exercise>
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
                ExerciseUsers = new List<ExerciseUser>
                {
                    //new ExerciseUser
                    //{
                    //    User = users[0]
                    //},
                    new ExerciseUser
                    {
                        User = users[1]
                    }
                }
            }
        };

        await _context.AddRangeAsync(exercises);

        var answers = new List<Answer>
        {
            new Answer
            {
                Description = "Xin lỗi thầy em không biết làm T_T",
                Link = "a.com",
                Score = 0,
                Exercise = exercises[0],
                User = users[0]
            },
            new Answer
            {
                Description = "Xin lỗi thầy em cũng không biết làm ạ T_T",
                Link = "b.com",
                Score = 0,
                Exercise = exercises[0],
                User = users[0]
            }
        };

        await _context.AddRangeAsync(answers);

        var posts = new List<Post>
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

        var comments = new List<Comment>
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

        var groups = new List<Group>
        {
            new Group
            {
                Name = "Owlvernyte",
                Description = "Nhóm",
                Leader = users[0],
                GroupUsers = new List<GroupUser>
                {
                    //new GroupUser
                    //{
                    //    User = users[0],
                    //    GroupRole = leader
                    //}
                },
                Classroom = classrooms[0]
            },
            new Group
            {
                Name = "Semibox",
                Description = "Nửa hộp",
                Leader = users[1],
                GroupUsers = new List<GroupUser>
                {
                    new GroupUser
                    {
                        User = users[0],
                        GroupRole = leader
                    },
                    //new GroupUser
                    //{
                    //    User = users[1],
                    //    GroupRole = member
                    //}
                },
                Classroom = classrooms[1]
            }
        };

        await _context.AddRangeAsync(groups);

        var projects = new List<Project>
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

        await _context.AddRangeAsync(projects);

        var missions = new List<Mission>
        {
            new Mission
            {
                Title = "Hãy đọc",
                Description = "Đọc 1 quyển sách",
                MissionUsers = new List<MissionUser>
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
                MissionUsers = new List<MissionUser>
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
                MissionUsers = new List<MissionUser>
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
                MissionUsers = new List<MissionUser>
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
                MissionUsers = new List<MissionUser>
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
                MissionUsers = new List<MissionUser>
                {
                    new MissionUser
                    {
                        User = users[1],
                    }
                },
                Project = projects[1]
            }
        };

        await _context.AddRangeAsync(missions);

        await _context.SaveChangesAsync();
    }

}
