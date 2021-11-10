using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace QA.Api.Hubs
{
    public class QAHub : Hub
    {
        

        //public async Task GetUpdateForQuestion()
        //{
        //    Thread.Sleep(1000);
        //    await Clients.Caller.SendAsync("", new { });
        //}

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

    }
}
