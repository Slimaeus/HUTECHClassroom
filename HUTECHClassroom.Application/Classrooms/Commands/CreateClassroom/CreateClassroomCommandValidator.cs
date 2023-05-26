using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Classrooms.Commands.CreateClassroom;

public class CreateClassroomCommandValidator : AbstractValidator<CreateClassroomCommand>
{
    public CreateClassroomCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().NotNull().MaximumLength(ClassroomConstants.TITLE_MAX_LENGTH);
        RuleFor(x => x.Description).MaximumLength(ClassroomConstants.DESCRIPTION_MAX_LENGTH);
        RuleFor(x => x.Topic).MaximumLength(ClassroomConstants.TOPIC_MAX_LENGTH);
        RuleFor(x => x.Room).MaximumLength(ClassroomConstants.ROOM_MAX_LENGTH);
        RuleFor(x => x.SchoolYear).MaximumLength(ClassroomConstants.SCHOOL_YEAR_MAX_LENGTH);
        RuleFor(x => x.Topic).MaximumLength(ClassroomConstants.TOPIC_MAX_LENGTH);
        RuleFor(x => x.StudyGroup).MaximumLength(ClassroomConstants.STUDY_GROUP_MAX_LENGTH);
        RuleFor(x => x.PracticalStudyGroup).MaximumLength(ClassroomConstants.PRACTIAL_STUDY_GROUP_MAX_LENGTH);
        RuleFor(x => x.Semester).IsInEnum();
        RuleFor(x => x.Type).IsInEnum();

        RuleFor(x => x.LecturerName).NotEmpty().NotNull();
    }
}
