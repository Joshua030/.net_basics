using ABCSharedLibrary.Models.Requests.Tenancy;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ABCSchoolApp.Pages.Tenancy
{
    public partial class UpgradeSubscription
    {
        [CascadingParameter] private IMudDialogInstance _dialogInstance { get; set; }
        [Parameter]

        public required UpdateTenantSubscriptionRequest SubscriptionRequest { get; set; }
        private MudForm? _form;

        private DateTime? NewExpiryDatePicker
        {
            get => SubscriptionRequest.NewExpiryDate == default
                ? null : SubscriptionRequest.NewExpiryDate;
            set
            {
                if (value.HasValue)
                {
                    SubscriptionRequest.NewExpiryDate = value.Value;
                }

            }
        }

        private async Task UpgradeSubscriptionAsync()
        {
            var result = await _tenantService.UpdateSubscriptionAsync(SubscriptionRequest);
            if (result.IsSuccessful)
            {
                // Handle successful creation
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

        private void CancelDialog()
        {
            _dialogInstance.Close();
        }
    }
}
