using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace client
{
    [ProtoContract]
    class Bet
    {
        [ProtoMember(1)]
        private UInt16 CardSign;

        [ProtoMember(2)]
        private UInt16 CardValue;

        [ProtoMember(3)]
        public int BetValue { get; private set; }

        public Card Card { get; private set; }

        /// <summary>
        /// Parameterless constructor required for protobuf
        /// </summary>
        protected Bet() { }

        public Bet(Card card, int betValue)
        {
            this.Card = card;
            this.BetValue = betValue;
        }

        /// <summary>
        /// Before serialising this object convert the card into two values
        /// </summary>
        [ProtoBeforeSerialization]
        private void Serialize()
        {
            if (Card != null)
            {
                CardSign = Card.Sign;
                CardValue = Card.Value;
            }
        }

        /// <summary>
        /// When deserialising the object convert two values into one Card
        /// </summary>
        [ProtoAfterDeserialization]
        private void Deserialize()
        {
            Card.setValue(CardValue);
            Card.setSign(CardSign);
        }
    }
}
