using Acorna.Core.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Acorna.Core.Models.Chat;

namespace Acorna.Hubs
{
    public static class HubConnections
    {
        public static Dictionary<string, List<string>> ChatUsers = new Dictionary<string, List<string>>();

        public static List<string> GetChatUserId(string name)
        {
            return ChatUsers[name];
        }
    }

    public class ChatHub : Hub
    {
        public ChatHub()
        {

        }

        public async Task SendNewMessageRefresh(ChatMessageModel chatMessageModel)
        {
            await Clients.User(chatMessageModel.RecipientId.ToString()).SendAsync("SendNewMessageRefresh", chatMessageModel);
        }

        public async Task UnreadchattingMessages(int recipientId)
        {
            await Clients.User(recipientId.ToString()).SendAsync("UnreadchattingMessages");
        }

        public async Task ApprovalsCycleNumbersAndBills(List<string> usersId)
        {
            await Clients.Users(usersId).SendAsync("ApprovalsCycleNumbersAndBills");
        }

        public async Task AddedNewNumbersAndBills()
        {
            await Clients.Groups("SuperAdmin").SendAsync("AddedNewNumbersAndBills");
        }

        public override Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated
                && HubConnections.ChatUsers.ContainsKey(Context.User.Identity.Name)
                && !HubConnections.ChatUsers[Context.User.Identity.Name].Contains(Context.ConnectionId))
                HubConnections.ChatUsers[Context.User.Identity.Name].Add(Context.ConnectionId);
            else if (!string.IsNullOrEmpty(Context.User.Identity.Name))
                HubConnections.ChatUsers.Add(Context.User.Identity.Name, new List<string> { Context.ConnectionId });

            if (Context.User.IsInRole("SuperAdmin"))
            {
                Groups.AddToGroupAsync(Context.ConnectionId, "SuperAdmin");
            }

            if (Context.User.IsInRole("Admin"))
            {
                Groups.AddToGroupAsync(Context.ConnectionId, "Admin");
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (!string.IsNullOrEmpty(Context.User.Identity.Name))
                HubConnections.ChatUsers.Remove(Context.User.Identity.Name);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendNewMessage(ChatMessageModel chatMessageModel)
        {
            await Clients.User(chatMessageModel.RecipientId.ToString()).SendAsync("MessageReceived", chatMessageModel);
            await Clients.User(chatMessageModel.CreatedBy.ToString()).SendAsync("MessageReceived", chatMessageModel);
        }
        public async Task<string> GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}