using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using QA.Shared;

namespace QA.Server.Hubs
{
    public class QAHub : Hub
    {
        

        public async Task SendQuestion(Question question)
        {
            await Clients.All.SendAsync("ReceiveQuestion",question);
            
        }
        public async Task SendAnswer(Answer answer)
        {
            await Clients.All.SendAsync("ReceiveAnswer", answer);
        }

        public async Task EndQuestion()
        {
            await Clients.All.SendAsync("TerminateQuestion");
        }
    }
}
