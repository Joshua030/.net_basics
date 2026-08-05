using ABCSharedLibrary.Models.Requests.Identity;
using ABCSharedLibrary.Models.Responses.Identity;
using ABCSharedLibrary.Wrappers;
using App.Infrastructure.Extensions;
using App.Infrastructure.Services.Identity;
using System.Net.Http.Json;

namespace App.Infrastructure.Services.Implementation.Identity
{
    internal class UserService(
        HttpClient httpClient,
        ApiSettings apiSettings
        ) : IUserService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ApiSettings _settings = apiSettings;

        public async Task<IResponseWrapper<string>> UpdateUserAsync(UpdateUserRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(_settings.UserEndpoints.Update, request);
            return await response.WrapToResponse<string>();
        }

        public async Task<IResponseWrapper> ChangeUserPasswordAsync(ChangePasswordRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(_settings.UserEndpoints.ResetPassword, request);
            return await response.WrapToResponse<string>();
        }

        public async Task<IResponseWrapper<List<UserResponse>>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync(_settings.UserEndpoints.All);
            return await response.WrapToResponse<List<UserResponse>>();
        }

        public async Task<IResponseWrapper<UserResponse>> GetUserByIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync(_settings.UserEndpoints.GetById(userId));
            return await response.WrapToResponse<UserResponse>();
        }

        public async Task<IResponseWrapper<string>> RegisterUserAsync(CreateUserRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(_settings.UserEndpoints.Register, request);
            return await response.WrapToResponse<string>();
        }

        public async Task<IResponseWrapper<List<UserRoleResponse>>> GetUserRolesByIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync(_settings.UserEndpoints.GetRolesById(userId));
            return await response.WrapToResponse<List<UserRoleResponse>>();
        }

        public async Task<IResponseWrapper<string>> UpdateUserRolesAsync(string userId, UserRolesRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(_settings.UserEndpoints.UpdateRolesById(userId), request);
            return await response.WrapToResponse<string>();
        }

        public async Task<IResponseWrapper<string>> ChangeUserStatusAsync(ChangeUserStatusRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(_settings.UserEndpoints.UpdateStatus, request);
            return await response.WrapToResponse<string>();
        }
    }
}
