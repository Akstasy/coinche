using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using Common;

namespace server
{
    public class Game
    {
        public string RoomHash { get; private set; }
        private List<Player> Players = new List<Player>();
    
        public Game(List<NetworkUser> users)
        { 
            init(users);
        }

        void sendUserDatas()
        {

        }

        private static void sendBet()
        {

        }

        public void start()
        {

            foreach (var player in Players)
            {
                Console.WriteLine(player.NetUser.Ip);
                Console.WriteLine(player.NetUser.Port);
                Console.WriteLine(player.RoomHash);
            }

            foreach (Player player in Players)
            {
                NetworkComms.SendObject("Message", player.NetUser.Ip, player.NetUser.Port, player);
            }
        }

        private void init(List<NetworkUser> users)
        {
            List<Card> deck = createShuffledDeck();
            string roomHash = $"{users[0].Hash}:{users[1].Hash}:{users[2].Hash}:{users[3].Hash}";
            RoomHash = roomHash;
            for (int i = 0; i < 4; ++i)
            {
                int id = i + 1;
                int groupId = i % 2;
                List<Card> userDeck = new List<Card>();
                for (int j = 0; j < 8; ++j)
                {
                    userDeck.Add(deck[0]);
                    deck.RemoveAt(0);
                }
                Player player = new Player((UInt16)id, (UInt16)groupId, roomHash, userDeck);
                player.setNetworkUser(users[i]);
                Players.Add(player);
            }
        }

        private static List<Card> createShuffledDeck()
        {
            List<Card> deck = new List<Card>();

            for (UInt16 i = 0; i < 4; ++i)
            {
                for (UInt16 j = 7; j <= 14; ++j)
                {
                    deck.Add(new Card(i, j));
                }
            }
            Random rng = new Random();
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = deck[k];
                deck[k] = deck[n];
                deck[n] = value;
            }

            return deck;
        }
    }
}
