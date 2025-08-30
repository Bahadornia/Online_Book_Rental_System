using Inventory.API.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace Inventory.API.Policies;

internal class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        throw new NotImplementedException();
    }
}
