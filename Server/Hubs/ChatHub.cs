using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;


namespace Server.Hubs
{


    public class ChatHub : Hub
    {

        private static int _connectedUsers = 0;
 
        public async Task SendMessage(string encryptedMessage, string hmac, string messageId)
        {
            await Clients.All.SendAsync("RecieveMessage", encryptedMessage, hmac, messageId);
        }

        public async Task ExchangePublicKey(string user, string publicKey)
        {
            await Clients.Others.SendAsync("RecievePublicKey", user, publicKey);
        }

        public async Task SendAESKey(string user, string encryptedAESKey)
        {
            await Clients.All.SendAsync("RecieveAESKey", user, encryptedAESKey);
        }


        public override async Task OnConnectedAsync()
        {
            _connectedUsers++;
            await Clients.All.SendAsync("UserCountUpdate", _connectedUsers);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _connectedUsers--;
            await Clients.All.SendAsync("UserCountUpdate", _connectedUsers);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
