using System.Collections.Immutable;

namespace HUTECHClassroom.Infrastructure.Services;

public interface IUserAccessor
{
    string UserName { get; }
    IList<string> Roles { get; }
    IDictionary<string, ImmutableArray<string>> EntityClaims { get; }
}