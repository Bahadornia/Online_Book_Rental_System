using Refit;
using Website.Dtos;

namespace Website.Externals.Refit
{
    public interface IUserService
    {
        [Put("/api/Users/{userId}/SetUserConfigs")]
        Task SetUserConfig([Body] UserDto userDto, string userId, CancellationToken ct);

        [Get("/api/Users/{userId}/UserConfigs")]
        Task<ApiResponse<UserDto>> GetUserConfigs(string userId, CancellationToken ct);
    }
}
