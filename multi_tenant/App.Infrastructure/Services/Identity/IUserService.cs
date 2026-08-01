using ABCSharedLibrary.Models.Requests.Identity;
using ABCSharedLibrary.Wrappers;

namespace App.Infrastructure.Services.Identity
{
    public interface IUserService
    {
        Task<IResponseWrapper<string>> UpdateUserAsync(UpdateUserRequest request);
    }
}
