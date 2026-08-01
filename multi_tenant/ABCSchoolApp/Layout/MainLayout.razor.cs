using ABCSchoolApp.Components;
using MudBlazor;

namespace ABCSchoolApp.Layout
{
    public partial class MainLayout
    {
        private bool _drawerOpen = true;
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private void toggleDrawer()
        {
            _drawerOpen = !_drawerOpen;
        }

        private async Task LogoutDialog()
        {
            var parameters = new DialogParameters
            {
                { nameof(Logout.Title), "Logout Confirmation" },
                { nameof(Logout.ConfirmationMessage), "Are you sure you want to logout?" },
                { nameof(Logout.ButtonText), "Logout" },
                { nameof(Logout.Color), Color.Error }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, FullWidth = true, BackdropClick = true };
            await _dialogService.ShowAsync<Logout>(null, parameters, options);
        }
    }
}
