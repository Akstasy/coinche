using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Card
    {
        public UInt16 Sign { get; private set; }
        public UInt16 Value { get; private set; }

        public void setSign(UInt16 sign) { Sign = sign; }
        public void setValue(UInt16 value) { Value = value; }

        public Card(UInt16 sign, UInt16 value)
        {
            this.Sign = sign;
            this.Value = value;
        }
    }
}