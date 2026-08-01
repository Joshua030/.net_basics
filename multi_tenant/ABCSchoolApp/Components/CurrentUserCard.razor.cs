using App.Infrastructure.Extensions;

namespace ABCSchoolApp.Components
{
    public partial class CurrentUserCard
    {
        private string? FirstName { get; set; }
        private string? LastName { get; set; }

        private string? FirstLetterOfFirstName { get; set; }
        private string? Email { get; set; }

        private bool _isLoading = true;

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
            }
        }
    }
}
