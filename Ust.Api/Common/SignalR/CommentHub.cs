using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Ust.Api.Models.Response;

namespace Ust.Api.Common.SignalR
{
    public class CommentHub : Hub
    {
        //public async Task Enter(string username, string groupName)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        //}

        //public async Task Send(string message, string username, string groupName)
        //{
        //    var comment = new Comment
        //    {
        //        Message = message,
        //        CreatedDate = DateTimeOffset.Now.ToString("dd.MM.yyyy HH:mm"),
        //        CreatedBy = username,
        //    };
        //    await Clients.Group(groupName).SendAsync("Receive", comment);
        //}
        string groupname = "cats";
        public async Task Enter(string username)
        {
            if (String.IsNullOrEmpty(username))
            {
                await Clients.Caller.SendAsync("Notify", "Для входа в чат введите логин");
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
                await Clients.Group(groupname).SendAsync("Notify", $"{username} вошел в чат");
            }
        }
        public async Task Send(string message, string username)
        {
            await Clients.Group(groupname).SendAsync("Receive", message, username);
        }
    }
}
