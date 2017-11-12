using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;

namespace client
{
    class Client
    {
        public Player Player;
        private NetworkUser NetUser;
        private Game Game;


        public Client()
        {
            requireConnection();
            initCallbacks();
        }

        void requireConnection()
        {
            Console.WriteLine("Please enter your coinche favorite IP and Port in the format 100.100.100.100:100 and press enter:");
            string serverInfo = Console.ReadLine();
            //string serverIp = serverInfo.Substring(0, serverInfo.LastIndexOf(":"));
            //UInt16 serverPort = UInt16.Parse(serverInfo.Split(':').Last());
            string serverIp = "192.168.152.1";

            UInt16 serverPort = 2000;
            NetUser = new NetworkUser(serverIp, serverPort);

            NetworkComms.SendObject("Connection", serverIp, serverPort, "Asking for connection");
        }

        void initCallbacks()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("200", confirmationHandler);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("500", errorHandler);
            NetworkComms.AppendGlobalIncomingPacketHandler<Player>("Start", startGame);
        }

        private static void confirmationHandler(PacketHeader header, Connection connection, string message)
        {
            Console.WriteLine("OK");
        }

        private static void errorHandler(PacketHeader header, Connection connection, string message)
        {
            Console.WriteLine("KO");
            NetworkComms.Shutdown();
            System.Environment.Exit(84);
        }

        public void startGame(PacketHeader header, Connection connection, Player player)
        {
            Console.WriteLine("GameStart");
            Player = player;
            Player.setNetworkUser(NetUser);
            Game = new Game();
            Game.start();
        }
       

        public void join()
        {
            while (true)
            {
                //Console.ReadKey(true);
            }
        }
    }
}
