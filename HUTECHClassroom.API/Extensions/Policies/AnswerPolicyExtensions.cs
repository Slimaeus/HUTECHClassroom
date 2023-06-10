﻿using HUTECHClassroom.API.Authorization.Roles;
using HUTECHClassroom.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class AnswerPolicyExtensions
{
    public static void AddAnswerPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateAnswerPolicy, policy =>
        {
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.STUDENT, RoleConstants.LECTURER));

        });
        options.AddPolicy(ReadAnswerPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateAnswerPolicy, policy =>
        {
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.STUDENT, RoleConstants.LECTURER));

        });
        options.AddPolicy(DeleteAnswerPolicy, policy =>
        {
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.STUDENT, RoleConstants.LECTURER));

        });
    }
}
