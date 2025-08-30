using Microsoft.AspNetCore.Authorization;

namespace Inventory.API.Policies.Requirements;

internal class MinimumAgeRequirement : IAuthorizationRequirement
{
    public int Age { get; set; }
    public MinimumAgeRequirement(int age) => Age = age;
}
