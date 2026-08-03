using ABCSharedLibrary.Models.Requests.Tenancy;
using ABCSharedLibrary.Models.Responses.Tenancy;
using ABCSharedLibrary.Wrappers;
using App.Infrastructure.Extensions;
using App.Infrastructure.Services.Tenancy;
using System.Net.Http.Json;

namespace App.Infrastructure.Services.Implementation.Tenancy
{
    public class TenantService(HttpClient httpClient, ApiSettings apiSettings) : ITenantService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ApiSettings _apiSettings = apiSettings;
        public async Task<IResponseWrapper<string>> ActivateAsync(string tenantId)
        {
            var response = await _httpClient.PutAsJsonAsync(_apiSettings.TenantEndpoints.FullActivate(tenantId), tenantId);
            return await response.WrapToResponse<string>();
        }

        public async Task<IResponseWrapper<string>> CreateAsync(CreateTenantRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(_apiSettings.TenantEndpoints.Create, request);
            return await response.WrapToResponse<string>();
        }

        public async Task<IResponseWrapper<string>> DeactivateAsync(string tenantId)
        {
            var response = await _httpClient.PutAsJsonAsync(_apiSettings.TenantEndpoints.FullDeactivate(tenantId), tenantId);
            return await response.WrapToResponse<string>();
        }

        public async Task<IResponseWrapper<List<TenantResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(_apiSettings.TenantEndpoints.GetAll);
            return await response.WrapToResponse<List<TenantResponse>>();
        }

        public async Task<IResponseWrapper<TenantResponse>> GetByIdAsync(string tenantId)
        {
            var response = await _httpClient.GetAsync(_apiSettings.TenantEndpoints.GetById(tenantId));
            return await response.WrapToResponse<TenantResponse>();
        }

        public async Task<IResponseWrapper<string>> UpdateSubscriptionAsync(UpdateTenantSubscriptionRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(_apiSettings.TenantEndpoints.Upgrade, request);
            return await response.WrapToResponse<string>();
        }
    }
}
