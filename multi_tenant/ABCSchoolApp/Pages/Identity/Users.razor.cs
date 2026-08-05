using ABCSchoolApp.Components;
using ABCSharedLibrary.Models.Requests.Identity;
using ABCSharedLibrary.Models.Responses.Identity;
using ABCSharedLibrary.Models.Responses.Tenancy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace ABCSchoolApp.Pages.Identity
{
    public partial class Users
    {
        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; } = default!;
        [Inject]
        protected IAuthorizationService AuthorizationService { get; set; } = default!;
        private List<UserResponse> _userList = [];
        private bool _isLoading = true;
        private bool _canCreateUsers;
        private bool _canViewRoles;

        protected override async Task OnInitializedAsync()
        {
            await LoadUsers();
            _isLoading = false;
        }

        private async Task LoadUsers()
        {
            var result = await _userService.GetUsersAsync();
            if (result.IsSuccessful)
            {
                _userList = result.Data;
            }
            else
            {
                foreach (var message in result.Messages)
                {
                    _snackbar.Add(message, Severity.Error);
                }
            }
        }

        private async Task InvokeUserRegistrationDialog()
        {
            var parameters = new DialogParameters();
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, BackdropClick = false };
            var dialog = await _dialogService.ShowAsync<RegisterUser>(null, options: options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                await LoadUsers();
            }
        }

        private async Task ActivateOrDeactivateAsync(UserResponse user)
        {
            if (user.IsActive)
            {
                // Deactivate tenant
                var parameters = new DialogParameters
                {
                    {nameof(Confirmation.Title), "Deactivate User"},
                    {nameof(Confirmation.Message), $"Are you sure you want to deactivate the user '{user.FirstName} {user.LastName}'?"},
                    {nameof(Confirmation.ButtonText), "Deactivate"},
                    {nameof(Confirmation.Color), Color.Error},
                    { nameof(Confirmation.InputIcon), Icons.Material.Filled.PersonOff}
                };

                var options = new DialogOptions
                {
                    CloseButton = true,
                    MaxWidth = MaxWidth.Small,
                    FullWidth = true,
                    BackdropClick = true
                };

                var dialog = await _dialogService.ShowAsync<Confirmation>(null, parameters, options);
                var result = await dialog.Result;
                if (result != null && !result.Canceled)
                {
                    var response = await _userService.ChangeUserStatusAsync(new ChangeUserStatusRequest { UserId = user.Id, Activation = false });
                    if (response.IsSuccessful)
                    {
                        _snackbar.Add(response.Messages[0], Severity.Success);
                        await LoadUsers();
                    }
                    else
                    {
                        foreach (var message in response.Messages)
                        {
                            _snackbar.Add(message, Severity.Error);
                        }
                    }
                }
            }
            else
            {
                // Activate tenant
                var parameters = new DialogParameters
                {
                    {nameof(Confirmation.Title), "Activate User"},
                    {nameof(Confirmation.Message), $"Are you sure you want to activate the user '{user.FirstName} {user.LastName}'?"},
                    {nameof(Confirmation.ButtonText), "Activate"},
                    {nameof(Confirmation.Color), Color.Success},
                    { nameof(Confirmation.InputIcon), Icons.Material.Filled.PersonAdd}
                };

                var options = new DialogOptions
                {
                    CloseButton = true,
                    MaxWidth = MaxWidth.Small,
                    FullWidth = true,
                    BackdropClick = true
                };

                var dialog = await _dialogService.ShowAsync<Confirmation>(null, parameters, options);
                var result = await dialog.Result;
                if (result != null && !result.Canceled)
                {
                    var response = await _userService.ChangeUserStatusAsync(new ChangeUserStatusRequest { UserId = user.Id, Activation = true });
                    if (response.IsSuccessful)
                    {
                        _snackbar.Add(response.Messages[0], Severity.Success);
                        await LoadUsers();
                    }
                    else
                    {
                        foreach (var message in response.Messages)
                        {
                            _snackbar.Add(message, Severity.Error);
                        }
                    }
                }


            }
        }

        private void Cancel()
        {
            _navigation.NavigateTo("/");
        }

        private void GoToRoles(string userId)
        {
            _navigation.NavigateTo($"/user-roles/{userId}");
        }
    }
}
