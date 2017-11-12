using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Common
{
    [ProtoContract]
    public class Player
    {
        public NetworkUser NetUser;
        [ProtoMember(1)]
        public UInt16 Id;
        [ProtoMember(2)]
        public UInt16 Group;

        public List<Card> Cards = new List<Card>();

        [ProtoMember(3)]
        public string RoomHash;

        // sign, value
        [ProtoMember(4)]
        private UInt16[] _cards;

        [ProtoBeforeSerialization]
        private void Serialize()
        {
            UInt16 i = 0;
            foreach (var card in Cards)
            {
                _cards[i] = card.Sign;
                _cards[i + 1] = card.Value;
                i += 2;
            }
        }

        [ProtoAfterDeserialization]
        private void Deserialize()
        {
            Cards.Clear();
            for (var i = 0; i < _cards.Length; i += 2)
            {
                Cards.Add(new Card(_cards[i], _cards[i + 1]));
            }
        }

        public void setNetworkUser(NetworkUser netUser)
        {
            NetUser = netUser;
        }

        protected Player() { }

        public Player(UInt16 id, UInt16 groupId, string roomHash, List<Card> cards)
        {
            Id = id;
            Group = groupId;
            RoomHash = roomHash;
            Cards = cards;
        }
    }
}
