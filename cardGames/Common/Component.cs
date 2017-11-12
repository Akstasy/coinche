using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Common
{
    
    [ProtoContract]
    public class Component<T>
    {
        [ProtoMember(1)]
        public string RoomHash;

        [ProtoMember(2)]
        public T Object;

        protected Component()
        {

        }

        public Component(string hash)
        {
            RoomHash = hash;
        }
    }
}
