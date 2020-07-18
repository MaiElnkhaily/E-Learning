using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ELearningManagementSystem.signalr.hubs
{
    public class chatHub : Hub
    {
        public void send(string UserName,string photo,string message)
        {
            Clients.All.addNewMessageToPage(UserName,photo,message);
        }
    }
}