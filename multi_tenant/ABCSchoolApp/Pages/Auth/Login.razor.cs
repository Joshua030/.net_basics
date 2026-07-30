using App.Infrastructure.Models;
using MudBlazor;

namespace ABCSchoolApp.Pages.Auth
{
    public partial class Login
    {
        //[Inject] public ITokenService? TokenService { get; set; }
        private LoginRequest _loginRequest = new();

        private InputType _inputType = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
        private bool _isPasswordVisible;
        private MudForm? _form;

        protected override async Task OnInitializedAsync()
        {
            // check auth state of user
            var state = await _applicationStateProvider.GetAuthenticationStateAsync();

            if (state.User.Identity?.IsAuthenticated ?? false)
            {
                // redirect if logged in
                _navigation.NavigateTo("/");
            }

        }

        private async Task SubmitAsync()
        {
            // Validation
            var result = await _tokenService
                .LoginAsync(tenant: _loginRequest.Tenant, request: _loginRequest);

            if (result.IsSuccessful)
            {
                _navigation.NavigateTo("/");
            }
            else
            {
                // snack bar
                foreach (var message in result.Messages)
                {
                    _snackbar.Add(message, Severity.Error);
                }

            }
        }


        void TogglePasswordVisibility()
        {

            if (_isPasswordVisible)
            {
                _isPasswordVisible = false;
                _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
                _inputType = InputType.Password;
            }
            else
            {
                _isPasswordVisible = true;
                _passwordInputIcon = Icons.Material.Filled.Visibility;
                _inputType = InputType.Text;
            }

        }
    }
}
