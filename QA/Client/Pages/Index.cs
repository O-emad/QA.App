using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using QA.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Client.Pages
{
    public partial class Index : IAsyncDisposable
    {
        private HubConnection hubConnection;
        private Question ReceivedQuestion = null;
        private Answer SelectedAnswer = null;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/qaHub"))
                .Build();

            hubConnection.On<Question>("ReceiveQuestion", (question) =>
            {
                ReceivedQuestion = question;
                StateHasChanged();
            });

            hubConnection.On("TerminateQuestion", () =>
            {
                ReceivedQuestion = null;
                StateHasChanged();
            });


            await hubConnection.StartAsync();
        }

        async Task SendAnswer() =>
            await hubConnection.SendAsync("SendAnswer", SelectedAnswer);

        public async ValueTask DisposeAsync()
        {
            if (hubConnection is not null)
            {
                await hubConnection.DisposeAsync();
            }
        }
    }
}
