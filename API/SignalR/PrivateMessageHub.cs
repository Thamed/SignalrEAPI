using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using API.Controllers;
using API.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace API.SignalR
{
    [Authorize]
    [HubName("privateMessageHub")]
    public class PrivateMessageHub : Hub
    {
        private readonly static UserMapping<string> _connections =
            new UserMapping<string>();
        MessageModelController ctr = new MessageModelController();

        public async Task SendMessageAsync(string me, string reciver, string message)
        {
            
            MessageModel archiveMessage = new MessageModel
            {
                Text = message,
                Sender = me,
                Reciver = reciver,
                Time = DateTime.Now.ToString(),
            };
            ctr.PostMessageModel(archiveMessage);            
            foreach (var connectionId in _connections.GetConnections(reciver))
            {
                await Clients.Client(connectionId).SendAsync(message);
            }
        }

        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;
            _connections.Add(name, Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;
            
            _connections.Remove(name, Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string name = Context.User.Identity.Name;
            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }

            return base.OnReconnected();
        }
    }
}