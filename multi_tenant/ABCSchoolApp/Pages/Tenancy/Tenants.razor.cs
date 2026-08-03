using ABCSchoolApp.Components;
using ABCSharedLibrary.Models.Requests.Tenancy;
using ABCSharedLibrary.Models.Responses.Tenancy;
using MudBlazor;

namespace ABCSchoolApp.Pages.Tenancy
{
    public partial class Tenants
    {
        private List<TenantResponse> TenantList { get; set; } = [];
        private bool _isLoading = true;
        protected override async Task OnInitializedAsync()
        {
            await LoadTenantAsync();
            _isLoading = false;
        }
        private async Task LoadTenantAsync()
        {
            var result = await _tenantService.GetAllAsync();
            if (result.IsSuccessful)
            {
                TenantList = result.Data;
            }
            else
            {
                foreach (var message in result.Messages)
                {
                    _snackbar.Add(message, Severity.Error);
                }
            }

        }

        private void ReturClicked()
        {
            _navigation.NavigateTo("/");
        }

        private async Task OnBoardNewTenantAsync()
        {
            var parameters = new DialogParameters();
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, BackdropClick = false };
            var dialog = await _dialogService.ShowAsync<CreateTenant>("Onboard New Tenant", options);

            var result = await dialog.Result;

            if (result != null && !result.Canceled)
            {
                await LoadTenantAsync();
            }
        }

        private async Task UpgradeSubscriptionAsync(TenantResponse tenant)
        {
            var parameters = new DialogParameters
            {
                {nameof(UpgradeSubscription.SubscriptionRequest),
                new UpdateTenantSubscriptionRequest
                {
                    TenantId = tenant.Identifier,
                    NewExpiryDate = tenant.ValidUpTo
                }
                }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true,
                BackdropClick = false
            };

            var dialog = await _dialogService.ShowAsync<UpgradeSubscription>("Upgrade Tenant subscription", parameters, options);
            var result = await dialog.Result;

            if (result != null && !result.Canceled)
            {
                await LoadTenantAsync();
            }

        }

        private async Task ActivateOrDeactivateAsync(TenantResponse tenant)
        {
            if (tenant.IsActive)
            {
                // Deactivate tenant
                var parameters = new DialogParameters
                {
                    {nameof(Confirmation.Title), "Deactivate Tenant"},
                    {nameof(Confirmation.Message), $"Are you sure you want to deactivate the tenant '{tenant.Name}'?"},
                    {nameof(Confirmation.ButtonText), "Deactivate"},
                    {nameof(Confirmation.Color), Color.Error},
                    { nameof(Confirmation.InputIcon), Icons.Material.Filled.CloudOff}
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
                    var response = await _tenantService.DeactivateAsync(tenant.Identifier);
                    if (response.IsSuccessful)
                    {
                        _snackbar.Add(response.Messages[0], Severity.Success);
                        await LoadTenantAsync();
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
                    {nameof(Confirmation.Title), "Activate Tenant"},
                    {nameof(Confirmation.Message), $"Are you sure you want to activate the tenant '{tenant.Name}'?"},
                    {nameof(Confirmation.ButtonText), "Activate"},
                    {nameof(Confirmation.Color), Color.Success},
                    { nameof(Confirmation.InputIcon), Icons.Material.Filled.CloudQueue}
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
                    var response = await _tenantService.ActivateAsync(tenant.Identifier);
                    if (response.IsSuccessful)
                    {
                        _snackbar.Add(response.Messages[0], Severity.Success);
                        await LoadTenantAsync();
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
    }
}
