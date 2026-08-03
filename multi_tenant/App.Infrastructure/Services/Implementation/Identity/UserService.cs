using ABCSharedLibrary.Models.Requests.Identity;
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

    }
}
