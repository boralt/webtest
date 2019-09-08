
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webtest
{
    class MeasureSimple : IMesurement
    {
        protected UInt64 CurrentTime()
        {
            return 0;
        }

        public  void Start()
        {
            startMks = CurrentTime();
        }

        void Sent(UInt32 sent)
        {
            UInt64 currentTime = CurrentTime();

            if (!startMks)
                startMks = currentTime;

            if (!sentFirstMks)
                sentFirstMks = currentTime;

            sentBytes += sent;
        }

        void Received(UInt32 received)
        {
            UInt64 currentTime = CurrentTime();

            if (!startMks)
                startMks = currentTime;

            if (!receivedFirstMks)
                receivedFirstMks = currentTime;

            receivedLastMks = currentTime;
            receivedBytes += received;
        }



        public Uint64 SentLatency {
            get {
                if (startMks && sentFirstMks)
                    return sentFirstMks - startMks;
            }
        }

        public Uint64 ReceiveStartLatency 
        {
            get {
                if (!receivedFirst)
                    return 0;

                if (sentFirstMks)
                    return receivedFirstMks - SentLatency;
                else if (startMks)
                    return receivedFirstMks - startMks;
                else
                    return 0;
            }
        }

        public Uint64 ReceiveCompletionTime 
        {
            get
            {

            }

        }


        private Uint64 startMks = 0;
        private Uint64 sentFirstMks = 0;
        private Uint64 sentLastMks = 0;
        private Uint64 receivedFirstMks = 0;
        private Uint64 receivedLastMks = 0;
        private Uint64 sentBytes = 0;
        private Uint64 receivedBytes = 0;
    }
}
