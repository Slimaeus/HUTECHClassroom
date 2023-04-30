using System.Collections.Immutable;

namespace HUTECHClassroom.Domain.Interfaces;

public interface IUserAccessor
{
    Guid Id { get; }
    string UserName { get; }
    IList<string> Roles { get; }
    IDictionary<string, ImmutableArray<string>> EntityClaims { get; }
}