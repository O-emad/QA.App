using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using QA.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QA.Client.Pages
{
    public partial class Admin
    {
        private HubConnection hubConnection;
        private Question QuestionInput = null;
        public List<Question> Questions { get; set; } = new();

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/qaHub"))
                .Build();
            await hubConnection.StartAsync();

            //get available questions
            Questions = new List<Question>
            {
                new Question
                {
                    Text = "Where Are We?",
                    Answers = new List<Answer>
                    {
                        new Answer
                        {
                            Text = "Cairo Festival City",
                            IsCorrect = true
                        },
                        new Answer
                        {
                            Text = "City Center Almaza",
                            IsCorrect = false
                        },
                        new Answer
                        {
                            Text = "City Stars",
                            IsCorrect = false
                        },
                        new Answer
                        {
                            Text = "Mall Of Arabia",
                            IsCorrect = false
                        }
                    }
                },
                new Question
                {
                    Text = "What color is the sky?",
                    Answers = new List<Answer>
                    {
                        new Answer
                        {
                            Text = "Red",
                            IsCorrect = false
                        },
                        new Answer
                        {
                            Text = "Green",
                            IsCorrect = false
                        },
                        new Answer
                        {
                            Text = "Yellow",
                            IsCorrect = false
                        },
                        new Answer
                        {
                            Text = "Blue",
                            IsCorrect = true
                        }
                    }
                },
                new Question
                {
                    Text = "Blazor is?",
                    Answers = new List<Answer>
                    {
                        new Answer
                        {
                            Text = "Back-end framework",
                            IsCorrect = false
                        },
                        new Answer
                        {
                            Text = "UI framework",
                            IsCorrect = true
                        },
                        new Answer
                        {
                            Text = "RTE",
                            IsCorrect = false
                        },
                        new Answer
                        {
                            Text = "Data repository",
                            IsCorrect = false
                        }
                    }
                }
            };

            hubConnection.On<Answer>("ReceiveAnswer", async (answer) =>
            {
                if (answer.IsCorrect)
                    await Terminate();
            });

        }

        async Task Send() =>
            await hubConnection.SendAsync("SendQuestion", QuestionInput);

        async Task Terminate() =>
            await hubConnection.SendAsync("EndQuestion");

        public bool IsConnected =>
            hubConnection.State == HubConnectionState.Connected;

        public async ValueTask DisposeAsync()
        {
            if (hubConnection is not null)
            {
                await hubConnection.DisposeAsync();
            }
        }

    }
}
