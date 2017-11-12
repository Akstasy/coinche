using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Common
{
    [ProtoContract]
    public class Bet
    {
        [ProtoMember(1)]
        public bool Coinche;

        [ProtoMember(2)]
        public UInt16 BetValue { get; private set; }
        
        [ProtoMember(3)]
        public UInt16 AtoutSign { get; private set; }

        [ProtoMember(4)]
        public UInt16 PlayerId { get; private set; }

        [ProtoMember(5)]
        public UInt16 CurrentPlayerId { get; private set; }

        /// <summary>
        /// Parameterless constructor required for protobuf
        /// </summary>
        protected Bet() { }

        protected Bet(UInt16 currentPlayerId) {
            Coinche = false;
            BetValue = 0;
            AtoutSign = 0;
            PlayerId = 0;
            CurrentPlayerId = currentPlayerId;
        }
    }
}
