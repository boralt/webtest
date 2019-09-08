using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webtest
{
    interface IMeasurement
    {
        void Start();
        void Sent(UInt32 sent);
        void Received(UInt32 received);
        void DoneSend();
        void DoneReceived();

        public UInt64 SendLatency { get; }
        public UInt64 ReceiveLatency { get; }
        public UInt64 SendReceiveLatency { get; }

    }
}
