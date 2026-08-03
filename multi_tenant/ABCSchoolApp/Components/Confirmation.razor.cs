using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ABCSchoolApp.Components
{
    public partial class Confirmation
    {
        [CascadingParameter] IMudDialogInstance MudDialog { get; set; }
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Message { get; set; }
        [Parameter]
        public string ButtonText { get; set; }
        [Parameter]
        public Color Color { get; set; }

        [Parameter]
        public string InputIcon { get; set; }

        private async Task Confirmed()
        {
            MudDialog.Close(DialogResult.Ok(true));
        }

        private void OnCancel() => MudDialog.Cancel();

    }
}
