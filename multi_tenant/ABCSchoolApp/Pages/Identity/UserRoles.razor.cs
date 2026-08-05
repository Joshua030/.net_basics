using ABCSharedLibrary.Constants;
using ABCSharedLibrary.Models.Requests.Identity;
using ABCSharedLibrary.Models.Responses.Identity;
using App.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace ABCSchoolApp.Pages.Identity
{
    public partial class UserRoles
    {
        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; } = default!;

        [Inject]
        protected IAuthorizationService AuthorizationService { get; set; } = default!;

        [Parameter]
        public string UserId { get; set; } = string.Empty;
        private List<UserRoleResponse> _userRoleList = new List<UserRoleResponse>();
        private UserResponse _userResponse = new UserResponse();

        private bool _canUpdateUserRoles;
        private bool _isLoading = true;
        private string? _title;
        private string? _description;

        protected override async Task OnInitializedAsync()
        {
            // Get User By Id Provided
            var user = (await AuthState).User;
            _canUpdateUserRoles = await AuthorizationService.HasPermissionAsync(user, SchoolFeature.UserRoles, SchoolAction.Update);
            await GetUserByIdAsync();
            // Get User Roles By User Id Provided
            await GetUserRolesAsync();

            _isLoading = false;
        }

        private async Task GetUserByIdAsync()
        {
            var result = await _userService.GetUserByIdAsync(UserId);
            if (result.IsSuccessful)
            {
                _userResponse = result.Data;
                _title = $"User Roles for {_userResponse.FirstName} {_userResponse.LastName}";
                _description = $"Manage roles for {_userResponse.FirstName} {_userResponse.LastName}";
            }
            else
            {
                foreach (var message in result.Messages)
                {
                    _snackbar.Add(message, Severity.Error);
                }
            }
        }


        private async Task GetUserRolesAsync()
        {
            var result = await _userService.GetUserRolesByIdAsync(UserId);
            if (result.IsSuccessful)
            {
                _userRoleList = result.Data;
            }
            else
            {
                foreach (var message in result.Messages)
                {
                    _snackbar.Add(message, Severity.Error);
                }
            }
        }

        private async Task UpdateUserRolesAsync()
        {
            var request = new UserRolesRequest
            {
                UserRoles = [.. _userRoleList.Select(r => new UserRoleRequest
                {
                    RoleId = r.RoleId,
                    Name = r.Name,
                    IsAssigned = r.IsAssigned
                })]
            };

            var result = await _userService.UpdateUserRolesAsync(UserId, request);

            if (result.IsSuccessful)
            {
                _snackbar.Add(result.Messages[0], Severity.Success);
                _navigation.NavigateTo("/users");
            }
            else
            {
                foreach (var message in result.Messages)
                {
                    _snackbar.Add(message, Severity.Error);
                }
            }
        }

        private void Cancel()
        {
            _navigation.NavigateTo("/users");
        }
    }
}
