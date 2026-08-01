using ABCSharedLibrary.Models.Requests.Identity;
using App.Infrastructure.Extensions;
using MudBlazor;

namespace ABCSchoolApp.Pages.Identity
{
    public partial class Profile
    {
        private UpdateUserRequest? UpdateUserRequest { get; set; }
        private string? FirstName { get; set; }
        private string? LastName { get; set; }

        private string? FirstLetterOfFirstName { get; set; }
        private string? Email { get; set; }
        public string? UserId { get; set; }

        private bool _isLoading = true;

        private MudForm _form;

        protected override async Task OnInitializedAsync()
        {
            await SetCurrentUserDetails();
            _isLoading = false;
        }

        private async Task SetCurrentUserDetails()
        {
            var state = await _applicationStateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            if (user != null)
            {
                FirstName = user.GetFirstName();
                FirstLetterOfFirstName = FirstName?.Substring(0, 1).ToUpper();
                LastName = user.GetLastName();
                Email = user.GetEmail();
                UserId = user.GetUserId();
            }
        }
    }

}
