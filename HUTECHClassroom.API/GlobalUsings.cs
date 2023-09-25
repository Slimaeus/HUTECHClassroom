// Account
global using HUTECHClassroom.Application.Account.Commands.AddAvatar;
global using HUTECHClassroom.Application.Account.Commands.ChangePassword;
global using HUTECHClassroom.Application.Account.Commands.ForgotPassword;
global using HUTECHClassroom.Application.Account.Commands.Login;
global using HUTECHClassroom.Application.Account.Commands.Register;
global using HUTECHClassroom.Application.Account.Commands.RemoveAvatar;
global using HUTECHClassroom.Application.Account.Commands.ResetPassword;
global using HUTECHClassroom.Application.Account.DTOs;
// Answers
global using HUTECHClassroom.Application.Answers;
global using HUTECHClassroom.Application.Answers.Commands.CreateAnswer;
global using HUTECHClassroom.Application.Answers.Commands.DeleteAnswer;
global using HUTECHClassroom.Application.Answers.Commands.DeleteRangeAnswer;
global using HUTECHClassroom.Application.Answers.Commands.UpdateAnswer;
global using HUTECHClassroom.Application.Answers.DTOs;
global using HUTECHClassroom.Application.Answers.Queries.GetAnswer;
global using HUTECHClassroom.Application.Answers.Queries.GetAnswersWithPagination;
// Classrooms
global using HUTECHClassroom.Application.Classrooms;
global using HUTECHClassroom.Application.Classrooms.Commands.AddClassroomUser;
global using HUTECHClassroom.Application.Classrooms.Commands.CreateClassroom;
global using HUTECHClassroom.Application.Classrooms.Commands.DeleteClassroom;
global using HUTECHClassroom.Application.Classrooms.Commands.DeleteRangeClassroom;
global using HUTECHClassroom.Application.Classrooms.Commands.RemoveClassroomUser;
global using HUTECHClassroom.Application.Classrooms.Commands.UpdateClassroom;
global using HUTECHClassroom.Application.Classrooms.DTOs;
global using HUTECHClassroom.Application.Classrooms.Queries.GetClassroom;
global using HUTECHClassroom.Application.Classrooms.Queries.GetClassroomExercisesWithPagination;
global using HUTECHClassroom.Application.Classrooms.Queries.GetClassroomGroupsWithPagination;
global using HUTECHClassroom.Application.Classrooms.Queries.GetClassroomPostsWithPagination;
global using HUTECHClassroom.Application.Classrooms.Queries.GetClassroomsWithPagination;
global using HUTECHClassroom.Application.Classrooms.Queries.GetClassroomUsersWithPagination;
// Comments
global using HUTECHClassroom.Application.Comments;
global using HUTECHClassroom.Application.Comments.Commands.CreateComment;
global using HUTECHClassroom.Application.Comments.Commands.DeleteComment;
global using HUTECHClassroom.Application.Comments.Commands.DeleteRangeComment;
global using HUTECHClassroom.Application.Comments.Commands.UpdateComment;
global using HUTECHClassroom.Application.Comments.DTOs;
global using HUTECHClassroom.Application.Comments.Queries.GetComment;
global using HUTECHClassroom.Application.Comments.Queries.GetCommentsWithPagination;
// Missions
global using HUTECHClassroom.Application.Common.DTOs;
global using HUTECHClassroom.Application.Common.Models;
// Exercises
global using HUTECHClassroom.Application.Exercises;
global using HUTECHClassroom.Application.Exercises.Commands.AddExerciseUser;
global using HUTECHClassroom.Application.Exercises.Commands.CreateExercise;
global using HUTECHClassroom.Application.Exercises.Commands.DeleteExercise;
global using HUTECHClassroom.Application.Exercises.Commands.DeleteRangeExercise;
global using HUTECHClassroom.Application.Exercises.Commands.RemoveExerciseUser;
global using HUTECHClassroom.Application.Exercises.Commands.UpdateGroup;
global using HUTECHClassroom.Application.Exercises.DTOs;
global using HUTECHClassroom.Application.Exercises.Queries.GetExercise;
global using HUTECHClassroom.Application.Exercises.Queries.GetExerciseAnswersWithPagination;
global using HUTECHClassroom.Application.Exercises.Queries.GetExercisesWithPagination;
global using HUTECHClassroom.Application.Exercises.Queries.GetExerciseUsersWithPagination;
// Faculties
global using HUTECHClassroom.Application.Faculties;
global using HUTECHClassroom.Application.Faculties.Commands.CreateFaculty;
global using HUTECHClassroom.Application.Faculties.Commands.DeleteFaculty;
global using HUTECHClassroom.Application.Faculties.Commands.DeleteRangeFaculty;
global using HUTECHClassroom.Application.Faculties.Commands.UpdateFaculty;
global using HUTECHClassroom.Application.Faculties.DTOs;
global using HUTECHClassroom.Application.Faculties.Queries.GetFacultiesWithPagination;
global using HUTECHClassroom.Application.Faculties.Queries.GetFaculty;
// GroupRoles
global using HUTECHClassroom.Application.GroupRoles.DTOs;
global using HUTECHClassroom.Application.GroupRoles.Queries;
// Groups
global using HUTECHClassroom.Application.Groups;
global using HUTECHClassroom.Application.Groups.Commands.AddGroupLeader;
global using HUTECHClassroom.Application.Groups.Commands.AddGroupUser;
global using HUTECHClassroom.Application.Groups.Commands.AddRangeGroupUser;
global using HUTECHClassroom.Application.Groups.Commands.CreateGroup;
global using HUTECHClassroom.Application.Groups.Commands.DeleteGroup;
global using HUTECHClassroom.Application.Groups.Commands.DeleteRangeGroup;
global using HUTECHClassroom.Application.Groups.Commands.RemoveGroupLeader;
global using HUTECHClassroom.Application.Groups.Commands.RemoveGroupUser;
global using HUTECHClassroom.Application.Groups.Commands.RemoveRangeGroupUser;
global using HUTECHClassroom.Application.Groups.Commands.UpdateGroup;
global using HUTECHClassroom.Application.Groups.DTOs;
global using HUTECHClassroom.Application.Groups.Queries.GetGroup;
global using HUTECHClassroom.Application.Groups.Queries.GetGroupProjectsWithPagination;
global using HUTECHClassroom.Application.Groups.Queries.GetGroupsWithPagination;
global using HUTECHClassroom.Application.Groups.Queries.GetGroupUsersWithPagination;
// Majors
global using HUTECHClassroom.Application.Majors;
global using HUTECHClassroom.Application.Majors.Commands.CreateMajor;
global using HUTECHClassroom.Application.Majors.Commands.DeleteMajor;
global using HUTECHClassroom.Application.Majors.Commands.DeleteRangeMajor;
global using HUTECHClassroom.Application.Majors.Commands.UpdateMajor;
global using HUTECHClassroom.Application.Majors.DTOs;
global using HUTECHClassroom.Application.Majors.Queries.GetMajor;
global using HUTECHClassroom.Application.Majors.Queries.GetMajorsWithPagination;
global using HUTECHClassroom.Application.Missions;
global using HUTECHClassroom.Application.Missions.Commands.AddMissionUser;
global using HUTECHClassroom.Application.Missions.Commands.AddRangeMissionUser;
global using HUTECHClassroom.Application.Missions.Commands.CreateMission;
global using HUTECHClassroom.Application.Missions.Commands.DeleteMission;
global using HUTECHClassroom.Application.Missions.Commands.DeleteRangeMission;
global using HUTECHClassroom.Application.Missions.Commands.RemoveMissionUser;
global using HUTECHClassroom.Application.Missions.Commands.RemoveRangeMissionUser;
global using HUTECHClassroom.Application.Missions.Commands.UpdateMission;
global using HUTECHClassroom.Application.Missions.DTOs;
global using HUTECHClassroom.Application.Missions.Queries.GetMission;
global using HUTECHClassroom.Application.Missions.Queries.GetMissionsWithPagination;
global using HUTECHClassroom.Application.Missions.Queries.GetMissionUsersWithPagination;
// Posts
global using HUTECHClassroom.Application.Posts;
global using HUTECHClassroom.Application.Posts.Commands.CreatePost;
global using HUTECHClassroom.Application.Posts.Commands.DeletePost;
global using HUTECHClassroom.Application.Posts.Commands.DeleteRangePost;
global using HUTECHClassroom.Application.Posts.Commands.UpdatePost;
global using HUTECHClassroom.Application.Posts.DTOs;
global using HUTECHClassroom.Application.Posts.Queries.GetPost;
global using HUTECHClassroom.Application.Posts.Queries.GetPostCommentsWithPagination;
global using HUTECHClassroom.Application.Posts.Queries.GetPostsWithPagination;
// Projects
global using HUTECHClassroom.Application.Projects;
global using HUTECHClassroom.Application.Projects.Commands.AddMission;
global using HUTECHClassroom.Application.Projects.Commands.CreateProject;
global using HUTECHClassroom.Application.Projects.Commands.DeleteProject;
global using HUTECHClassroom.Application.Projects.Commands.DeleteRangeProject;
global using HUTECHClassroom.Application.Projects.Commands.RemoveMission;
global using HUTECHClassroom.Application.Projects.Commands.UpdateProject;
global using HUTECHClassroom.Application.Projects.DTOs;
global using HUTECHClassroom.Application.Projects.Queries.GetProject;
global using HUTECHClassroom.Application.Projects.Queries.GetProjectMissionsWithPagination;
global using HUTECHClassroom.Application.Projects.Queries.GetProjectsWithPagination;
// Roles
global using HUTECHClassroom.Application.Roles.DTOs;
// Subjects
global using HUTECHClassroom.Application.Subjects;
global using HUTECHClassroom.Application.Subjects.Commands.CreateSubject;
global using HUTECHClassroom.Application.Subjects.Commands.DeleteRangeSubject;
global using HUTECHClassroom.Application.Subjects.Commands.DeleteSubject;
global using HUTECHClassroom.Application.Subjects.Commands.UpdateSubject;
global using HUTECHClassroom.Application.Subjects.DTOs;
global using HUTECHClassroom.Application.Subjects.Queries.GetSubject;
global using HUTECHClassroom.Application.Subjects.Queries.GetSubjectsWithPagination;
// Users
global using HUTECHClassroom.Application.Users.DTOs;
global using HUTECHClassroom.Application.Users.Queries.GetCurrentUser;
global using HUTECHClassroom.Application.Users.Queries.GetUser;
global using HUTECHClassroom.Application.Users.Queries.GetUserAnswersWithPagination;
global using HUTECHClassroom.Application.Users.Queries.GetUserClassroomsWithPagination;
global using HUTECHClassroom.Application.Users.Queries.GetUserCommentsWithPagination;
global using HUTECHClassroom.Application.Users.Queries.GetUserExercisesWithPagination;
global using HUTECHClassroom.Application.Users.Queries.GetUserGroupsWithPagination;
global using HUTECHClassroom.Application.Users.Queries.GetUserMissionsWithPagination;
global using HUTECHClassroom.Application.Users.Queries.GetUserPostsWithPagination;
global using HUTECHClassroom.Application.Users.Queries.GetUserProjectsWithPagination;
global using HUTECHClassroom.Application.Users.Queries.GetUserRolesWithPagination;
// AspNet
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
