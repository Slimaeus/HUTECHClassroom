﻿using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Exercises.DTOs;

public record ExerciseClassroomDTO : BaseEntityDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Room { get; set; }
    public string? StudyPeriod { get; set; }
    public string? Topic { get; set; }
    public string? Class { get; set; }
    public string? SchoolYear { get; set; }
    public string? StudyGroup { get; set; }
    public string? PracticalStudyGroup { get; set; }
    public Semester? Semester { get; set; }
    public ClassroomType? Type { get; set; }

    public ClassroomType? Faculty { get; set; }
    public ClassroomType? Subject { get; set; }


    public MemberDTO? Lecturer { get; set; }
}
