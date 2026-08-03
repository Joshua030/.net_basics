using ABCSharedLibrary.Models.Requests.Tenancy;
using ABCSharedLibrary.Models.Responses.Tenancy;
using ABCSharedLibrary.Wrappers;

namespace App.Infrastructure.Services.Tenancy
{
    public interface ITenantService
    {
        Task<IResponseWrapper<List<TenantResponse>>> GetAllAsync();
        Task<IResponseWrapper<TenantResponse>> GetByIdAsync(string tenantId);
        Task<IResponseWrapper<string>> CreateAsync(CreateTenantRequest request);
        Task<IResponseWrapper<string>> UpdateSubscriptionAsync(UpdateTenantSubscriptionRequest request);
        Task<IResponseWrapper<string>> ActivateAsync(string tenantId);
        Task<IResponseWrapper<string>> DeactivateAsync(string tenantId);
    }
}
