using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Linq;
using System.Windows.Forms;



namespace Achievement_API
{

    class Server
    {


        private static void HandleRequest(TcpClient Client, NetworkStream Stream, string RemoteIP)
        {

            try
            {
                Client.Client.Close();
                return;
            }
            catch (Exception ex)
            {
                Tools.AppendText(ex.Message, ConsoleColor.Red);
                //Tools.AppendText($"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()} >> Failed to Read File >> ", ConsoleColor.Red);
                Client.Client.Close();
                return;
            }
        }

        private static void AcceptClient(TcpListener Listener)
        {
            try
            {

                while (true)
                {
                    while (!Listener.Pending())
                    {
                        Thread.Sleep(1);
                    }
                    TcpClient Client = Listener.AcceptTcpClient();
                    NetworkStream Stream = Client.GetStream();
                    string RemoteIP = Client.Client.RemoteEndPoint.ToString().Split(':')[0x00];

                    HandleRequest(Client, Stream, RemoteIP);
                }
            }
            catch
            {
                Console.WriteLine("API - Unable to listen for incoming requests\n");
                Listener.Server.Close();
            }
        }
        static int ServerPort;
        public enum CONFIG_VALUES : int
        {
            PORT,
            IP,
        }
        public static void Initialize()
        {
            if (!File.Exists("bin/serverconfig.ini"))
            {
                File.WriteAllText("bin/serverconfig.ini", "" + 6000);
            }
            if (!File.Exists("bin/serverip.ini"))
            {
                File.WriteAllText("bin/serverip.ini", "127.0.0.1");
            }
            string[] config = File.ReadAllLines("bin/serverconfig.ini");
            ServerPort = int.Parse(config[(int)CONFIG_VALUES.PORT]);
            string ServerIP = File.ReadAllText("bin/serverip.ini");
            try
            {
                TcpListener Listener = new TcpListener(IPAddress.Parse(ServerIP), ServerPort);
                Listener.Start();
                Console.WriteLine("API started & listening on Port: {0}  IP: {1}", ServerPort, ServerIP);
                new Thread(new ThreadStart(() => AcceptClient(Listener))).Start();
            }
            catch
            {
                Console.WriteLine("API - Failed to open socket\n");
                return;
            }
        }
    }
}
