namespace HUTECHClassroom.Application.Groups.DTOs;
public record GroupUserDTO
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string GroupRole { get; set; }

    public GroupUserDTO(string userName, string email, string firstName, string lastName, string groupRole)
    {
        UserName = userName;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        GroupRole = groupRole;
    }
}
