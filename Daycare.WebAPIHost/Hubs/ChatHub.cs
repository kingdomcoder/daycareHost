using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Daycare.WebAPIHost.Hubs {
    // M3: Real-time chat hub integrated into the WebAPIHost (the only API container deployed
    // to production). Functionally identical to the original Daycare.WebSocket.SignalR.Hubs.ChatHub:
    // a client invokes SendMessage(user, message) and every connected client receives
    // "ReceiveMessage". The Flutter client (signalr_helper.dart) persists the message via the REST
    // API first, then calls SendMessage purely as a broadcast "ping"; on ReceiveMessage the client
    // reloads its message list from REST. The hub therefore carries no sensitive payload itself.
    //
    // Authentication: intentionally anonymous (no [Authorize]) to match the existing front-end,
    // which connects with HubConnectionBuilder().withUrl(url) WITHOUT a bearer token or
    // access_token query param. Message content/authorization is enforced on the REST endpoints
    // (ChatController is [Authorize]); the hub only signals "something changed, reload".
    public class ChatHub : Hub {
        public override Task OnConnectedAsync() {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception) {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message) {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
