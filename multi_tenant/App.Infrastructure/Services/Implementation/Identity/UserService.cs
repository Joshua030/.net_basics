using ABCSharedLibrary.Models.Requests.Identity;
using ABCSharedLibrary.Wrappers;
using App.Infrastructure.Extensions;
using App.Infrastructure.Services.Identity;
using System.Net.Http.Json;

namespace App.Infrastructure.Services.Implementation.Identity
{
    internal class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _settings;
        public async Task<IResponseWrapper<string>> UpdateUserAsync(UpdateUserRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(_settings.UserEnpoints.Update, request);
            return await response.WrapToResponse<string>();
        }
    }
}
