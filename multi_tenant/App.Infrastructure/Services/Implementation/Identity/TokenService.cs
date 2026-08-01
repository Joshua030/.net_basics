using ABCSharedLibrary.Models.Requests.Token;
using ABCSharedLibrary.Models.Responses.Token;
using ABCSharedLibrary.Wrappers;
using App.Infrastructure.Constants;
using App.Infrastructure.Extensions;
using App.Infrastructure.Services.Auth;
using App.Infrastructure.Services.Identity;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace App.Infrastructure.Services.Implementation.Identity
{
    public class TokenService(
        HttpClient httpClient,
        ILocalStorageService localStorageService,
        AuthenticationStateProvider authenticationStateProvider,
        ApiSettings apiSettings
        ) : ITokenService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILocalStorageService _localStorageService = localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;
        private readonly ApiSettings _apiSettings = apiSettings;


        public async Task<IResponseWrapper> LoginAsync(string tenant, TokenRequest request)
        {
            // Attach Tenant to tenant header of the request
            using var httpRequest = new HttpRequestMessage(
                HttpMethod.Post,
                _apiSettings.TokenEndpoints.Login)
            {
                Content = JsonContent.Create(request)
            };

            AddTenantHeader(httpRequest, headerName: "tenant", value: tenant);
            //Send a login request to the api
            var response = await _httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead);
            var result = await response.WrapToResponse<TokenResponse>();
            //if successful -> save token to local storage
            if (result.IsSuccessful)
            {
                var token = result.Data.Jwt;
                var refreshToken = result.Data.RefreshToken;

                await _localStorageService.SetItemAsync(StorageConstants.AuthToken, token);
                await _localStorageService.SetItemAsync(StorageConstants.RefreshToken, refreshToken);

                //update auth state
                ((ApplicationStateProvider)_authenticationStateProvider).MarkUserAuthenticated(request.Username);

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return await ResponseWrapper.SuccessAsync();
            }
            else
            {
                return await ResponseWrapper.FailAsync(messages: result.Messages);
            }

        }

        public async Task<IResponseWrapper> LogoutAsync()
        {
            // Clear local storage
            await _localStorageService.RemoveItemAsync(StorageConstants.AuthToken);
            await _localStorageService.RemoveItemAsync(StorageConstants.RefreshToken);
            // notify that the user is logged out
            ((ApplicationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
            return await ResponseWrapper.SuccessAsync(message: "Successfully logged out");
        }

        #region Helpers
        private static void AddTenantHeader(HttpRequestMessage request, string headerName, string value)
        {
            if (string.IsNullOrEmpty(value) || request.Headers.Contains(headerName)) return;
            request.Headers.TryAddWithoutValidation(headerName, value);
        }

        #endregion
    }
}
