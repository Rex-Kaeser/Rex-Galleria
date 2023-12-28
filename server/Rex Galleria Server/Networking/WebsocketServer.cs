using Rex_Galleria_Server.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace Rex_Galleria_Server
{
    class WebsocketServer
    {
        public WebsocketServer()
        {
            Thread recieveThread = new Thread(this.Recieve);
            recieveThread.Start();
        }
        public void Recieve()
        {
            WebSocketServer wssv = new WebSocketServer(System.Net.IPAddress.Any, 8070);
            Console.WriteLine("Listening on port 8070.");
            wssv.AddWebSocketService<SocketInstance>("/websocket");
            wssv.Start();
        }
    }
}
