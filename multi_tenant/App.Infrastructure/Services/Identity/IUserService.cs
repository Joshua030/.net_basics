using ABCSharedLibrary.Models.Requests.Identity;
using ABCSharedLibrary.Models.Responses.Identity;
using ABCSharedLibrary.Wrappers;

namespace App.Infrastructure.Services.Identity
{
    public interface IUserService
    {
        Task<IResponseWrapper<string>> UpdateUserAsync(UpdateUserRequest request);
        Task<IResponseWrapper> ChangeUserPasswordAsync(ChangePasswordRequest request);
        Task<IResponseWrapper<List<UserResponse>>> GetUsersAsync();
        Task<IResponseWrapper<UserResponse>> GetUserByIdAsync(string userId);
        Task<IResponseWrapper<string>> RegisterUserAsync(CreateUserRequest request);
        Task<IResponseWrapper<List<UserRoleResponse>>> GetUserRolesByIdAsync(string userId);
        Task<IResponseWrapper<string>> UpdateUserRolesAsync(string userId, UserRolesRequest request);
        Task<IResponseWrapper<string>> ChangeUserStatusAsync(ChangeUserStatusRequest request);

    }
}
