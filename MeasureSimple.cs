
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace webtest
{
    class MeasureSimple : IMesurement
    {
        protected long CurrentTimeMks()
        {
            return Stopwatch.GetTimestamp() * nanosecondsPerTick / 1000;
        }

        public  void Start()
        {
            startMks = CurrentTimeMks()
        }

        void Sent(UInt32 sent)
        {
            long currentTime = CurrentTimeMks();

            if (!startMks)
                startMks = currentTime;

            if (!sentFirstMks)
                sentFirstMks = currentTime;

            sentBytes += sent;
        }

        void Received(UInt32 received)
        {
            long currentTime = CurrentTimeMks();

            if (!startMks)
                startMks = currentTime;

            if (!receivedFirstMks)
                receivedFirstMks = currentTime;

            receivedLastMks = currentTime;
            receivedBytes += received;
        }



        public long SentLatency {
            get {
                if (startMks && sentFirstMks)
                    return sentFirstMks - startMks;
            }
        }

        public long ReceiveStartLatency 
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


        
        private long sentFirstMks = 0;
        private long sentLastMks = 0;
        private long receivedFirstMks = 0;
        private long receivedLastMks = 0;
        private long sentBytes = 0;
        private long receivedBytes = 0;
        
        private long nanosecondsPerTick = (1000L*1000L*1000L) / Stopwatch.Frequency;
    }
}
