﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Accessibility_backend.Hubs
{
	public class ChatHub : Hub
	{
		private readonly string _botUser;
		public ChatHub()
		{
			_botUser = "MyChat Bot";
		}
		public async Task JoinRoom(UserConnection userConnection)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);
			await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", _botUser, $"{userConnection.Email} is er");
		}
	}
}
