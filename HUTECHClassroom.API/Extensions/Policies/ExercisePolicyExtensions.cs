﻿using HUTECHClassroom.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class ExercisePolicyExtensions
{
    public static void AddExercisePolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateExercisePolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER);
        });
        options.AddPolicy(ReadExercisePolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateExercisePolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER);
        });
        options.AddPolicy(DeleteExercisePolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER);
        });
    }
}
