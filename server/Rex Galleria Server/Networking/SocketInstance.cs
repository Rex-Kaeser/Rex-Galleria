using System.Text;

using System.Net;      //required
using System.Net.Sockets;    //required
using WebSocketSharp;
using WebSocketSharp.Server;
using System;

namespace Rex_Galleria_Server.Networking
{
    class SocketInstance : WebSocketBehavior
    {
        bool unlocked = false;
        public void SendMessage(string message)
        {
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            if (!unlocked)
            {
                Console.WriteLine(e.Data);
                string msg = Encryption.AESDecrypt(e.RawData);
                Console.WriteLine(msg);
            }
        }

        protected override void OnClose(CloseEventArgs e)
        {
        }

        protected override void OnOpen()
        {
            Console.WriteLine("Connceted");
        }

        protected override void OnError(ErrorEventArgs e)
        {
            Console.WriteLine("Error");
        }
    }
}
