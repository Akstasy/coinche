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
    public class Game
    {
        public Bet Bet { get; private set; }

        public Game()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<Component<Bet>>("Bet", setBet);
            Component<Bet> comp = new Component<Bet>("trolololo");
            NetworkComms.SendObject("Bet", "127.0.0.1", 2000, comp);
        }

       private void setBet(PacketHeader header, Connection connection, Component<Bet> bet)
        {
            Bet = bet.Object;
            askBet();
        }

        public void askBet()
        {
            // readline
        }

        public void start()
        {

        }
    }
}
