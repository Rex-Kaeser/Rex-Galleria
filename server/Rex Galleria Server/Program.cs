using Rex_Galleria_Server.Networking;
using System;
using System.Text;

namespace Rex_Galleria_Server
{
    class Program
    {

        public static string key = "password";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(Encryption.GetComplexKey(key));
            Console.WriteLine(Encryption.GetIV(key));
            Console.WriteLine(Convert.ToBase64String(Encryption.AESEncrypt("test")));
            Console.WriteLine(Encryption.AESDecrypt(Encryption.AESEncrypt("test")));
            new WebsocketServer();

            while (true)
            {

            }
        }
    }
}
