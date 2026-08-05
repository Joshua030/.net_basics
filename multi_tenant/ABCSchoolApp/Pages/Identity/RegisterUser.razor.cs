using ABCSharedLibrary.Models.Requests.Identity;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ABCSchoolApp.Pages.Identity
{
    public partial class RegisterUser
    {
        private CreateUserRequest CreateUserRequest { get; set; } = new CreateUserRequest();
        [CascadingParameter]
        private IMudDialogInstance _dialogInstance { get; set; } = default!;

        private InputType _inputType = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
        private bool _isPasswordVisible = false;
        private MudForm _form = default;

        private InputType _inputConfirmType = InputType.Password;
        private string _confirmPasswordInputIcon = Icons.Material.Filled.VisibilityOff;
        private bool _isConfirmPasswordVisible = false;

        private async Task SubmitUserRegistrationAsync()
        {
            var result = await _userService.RegisterUserAsync(CreateUserRequest);

            if (result.IsSuccessful)
            {
                _snackbar.Add(result.Messages[0], Severity.Success);
                _dialogInstance.Close(DialogResult.Ok(true));
            }
            else
            {
                foreach (var message in result.Messages)
                {
                    _snackbar.Add(message, Severity.Error);
                }
            }


        }

        private void CancelDialog() => _dialogInstance.Cancel();

        private void TogglePasswordVisibility()
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
        private void ToggleConfirmPasswordVisibility()
        {

            if (_isConfirmPasswordVisible)
            {
                _isConfirmPasswordVisible = false;
                _confirmPasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                _inputConfirmType = InputType.Password;
            }
            else
            {
                _isConfirmPasswordVisible = true;
                _confirmPasswordInputIcon = Icons.Material.Filled.Visibility;
                _inputConfirmType = InputType.Text;
            }

        }

    }
}
