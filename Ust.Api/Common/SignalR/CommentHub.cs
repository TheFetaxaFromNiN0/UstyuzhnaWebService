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
    }
}
