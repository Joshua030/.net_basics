using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Services.Auth
{
    public class ApplicationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient) : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService = localStorageService;
        private readonly HttpClient _httpClient = httpClient;
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
