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

        public ulong SendLatency { get; }
        public ulong ReceiveLatency { get; }
        public ulong SendReceiveLatency { get; }

    }
}
