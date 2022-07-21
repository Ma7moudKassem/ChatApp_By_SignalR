namespace ChatApp.Client.Pages
{
    public partial class Chat 
    {
        [Inject] NavigationManager navigationManager { get; set; }
        private HubConnection? hubConnection;

        private string? userName;
        private string? newMessage;
        private bool isConnected = false;
        private List<Message> messages = new List<Message>();
        public async Task Connect()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(navigationManager.ToAbsoluteUri("/chathub")).Build();
            hubConnection.On<string, string>("ReceiveMessage", Send);
            await hubConnection.StartAsync();
            isConnected = true;
            messages.Clear();
        }
        private async Task SendAsync(string message)
        {
            if (isConnected && !string.IsNullOrWhiteSpace(message))
            {
                await hubConnection.SendAsync("SendMessage", userName, message);
            }
        }
        private void Send(string name, string message)
        {
            List<string> receiverIds = new List<string> { Guid.NewGuid().ToString() };
            bool isMine = name.Equals(userName, StringComparison.OrdinalIgnoreCase);
            messages.Add(new Message() { UserName = name, Subject = message, Mine = isMine, SenderId = Guid.NewGuid().ToString(), ReceiverIds = receiverIds, DateTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") }); ;
            newMessage = string.Empty;
            StateHasChanged();
        }
    }
}
