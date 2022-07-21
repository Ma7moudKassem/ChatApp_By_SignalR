

namespace ChatApp.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string username, string message) => 
            await Clients.All.SendAsync("ReceiveMessage", username, message);

        public override async Task OnConnectedAsync()=>await base.OnConnectedAsync();

        public override async Task OnDisconnectedAsync(Exception e)=> await base.OnDisconnectedAsync(e);
    }
}
