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

        long SendLatency { get; }
        long ReceiveLatency { get; }
        long SendReceiveLatency { get; }

        long ReceivedBytes { get; }
        long SendBytes { get; }


    }
}
