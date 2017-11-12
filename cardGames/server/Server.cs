using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;

namespace server
{
    class Server
    {
        private UInt16 MaxClient;
        private List<NetworkUser> NetworkUsers = new List<NetworkUser>();
        private Dictionary<string, Game> Games = new Dictionary<string, Game>();

        public Server(UInt16 port, UInt16 maxClient = 40)
        {
            MaxClient = maxClient;
            NetworkComms.AppendGlobalConnectionEstablishHandler(connectionHandler);
            NetworkComms.AppendGlobalConnectionCloseHandler(disconnectionHandler);

            NetworkComms.AppendGlobalIncomingPacketHandler<Component<Bet>>("Bet", receiveBet);

            Connection.StartListening(ConnectionType.TCP, new System.Net.IPEndPoint(System.Net.IPAddress.Any, port));

            foreach (System.Net.IPEndPoint localEndPoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
                Console.WriteLine("{0}:{1}", localEndPoint.Address, localEndPoint.Port);
        }

        public void start()
        {
            while (true)
            {
                Console.ReadKey(true);
            }
        }

        private void connectionHandler(Connection connection)
        {
            Console.WriteLine("Incoming Co {0}", NetworkComms.TotalNumConnections());
            if (NetworkComms.TotalNumConnections() > MaxClient)
            {
                connection.CloseConnection(false);
            }
            else
            {
                NetworkUsers.Add(getNetworkUser(connection));

                if (NetworkUsers.Count >= 4)
                {
                    List<NetworkUser> localUsers = new List<NetworkUser>();

                    for (int i = 0; i < 4; i++)
                    {
                        localUsers.Add(NetworkUsers[0]);
                        NetworkUsers.RemoveAt(0);
                    }
                    var game = new Game(localUsers);
                    Games[game.RoomHash] = game;
                    game.start();
                }
            }
        }

        private void disconnectionHandler(Connection connection)
        {

        }

        private static NetworkUser getNetworkUser(Connection connection)
        { 
            var endPoint = connection.ConnectionInfo.RemoteEndPoint.ToString();
            string ip = endPoint.Substring(0, endPoint.LastIndexOf(":"));
            UInt16 port = UInt16.Parse(endPoint.Split(':').Last());
            string hash = connection.ConnectionInfo.NetworkIdentifier;

            return new NetworkUser(ip, port, hash);
        }
        private static void receiveBet(PacketHeader header, Connection connection, Component<Bet> bet)
        {
            Console.WriteLine(bet.RoomHash);
            Console.WriteLine(bet.Object.Coinche);
        }
    }
}
