using EntityFrameworkCore.Triggered;
using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Persistence.Triggers;
public sealed class AfterCreateGroupTrigger : IAfterSaveTrigger<Group>
{
    private readonly ApplicationDbContext _dbContext;

    public AfterCreateGroupTrigger(ApplicationDbContext dbContext)
        => _dbContext = dbContext;
    public async Task AfterSave(ITriggerContext<Group> context, CancellationToken cancellationToken)
    {
        if (context.ChangeType != ChangeType.Added) return;

        var group = context.Entity;

        if (group.LeaderId is null) return;

        var groupRole = await _dbContext
            .GroupRoles
            .FirstOrDefaultAsync(
                x => x.Name == GroupRoleConstants.LEADER,
                cancellationToken: cancellationToken);

        group.GroupUsers
            .Add(new GroupUser
            {
                UserId = group.LeaderId.Value,
                GroupRole = groupRole
            });

        _dbContext.Update(group);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
