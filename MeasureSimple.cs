
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace webtest
{
    class MeasureSimple : IMeasurement
    {
        protected long CurrentTimeMks()
        {
            return Stopwatch.GetTimestamp() * nanosecondsPerTick / 1000;
        }

        public void Start()
        {
            startMks = CurrentTimeMks();
        }

        public void Sent(UInt32 sent)
        {
            long currentTime = CurrentTimeMks();

            if (startMks == 0)
                startMks = currentTime;

            if (sentFirstMks == 0)
                sentFirstMks = currentTime;

            sentLastMks = currentTime;

            sentBytes += sent;
        }

        public void DoneSend()
        {
            sentLastMks = CurrentTimeMks();
        }

        public void DoneReceived()
        {
            receivedLastMks = CurrentTimeMks();
        }

        public void Received(UInt32 received)
        {
            long currentTime = CurrentTimeMks();

            if (startMks == 0)
                startMks = currentTime;

            if (receivedFirstMks == 0)
                receivedFirstMks = currentTime;

            receivedLastMks = currentTime;
            receivedBytes += received;
        }



        public long SendLatency {
            get {
                if (startMks != 0 && sentFirstMks != 0)
                    return sentFirstMks - startMks;
                return 0;
            }
        }

        public long ReceiveLatency
        {
            get {
                if (receivedFirstMks == 0)
                    return 0;

                if (sentFirstMks != 0)
                    return receivedFirstMks - sentFirstMks;
                else if (startMks != 0)
                    return receivedFirstMks - startMks;
                else
                    return 0;
            }
        }

        public long SendReceiveLatency
        {
            get
            {
                if (receivedFirstMks == 0)
                    return 0;

                if (startMks != 0)
                    return receivedFirstMks - startMks;
                else
                if (sentFirstMks != 0)
                    return receivedFirstMks - sentFirstMks;
                else
                    return 0;
            }
        }


        public long ReceiveCompletionTime
        {
            get
            {
                if (receivedFirstMks == 0)
                {
                    return 0;
                }

                return receivedLastMks - receivedLastMks;
            }

        }

        public long SendRate()
        {
            if (sentFirstMks != 0 && sentLastMks != 0 && sentLastMks != sentFirstMks)
                return sentBytes * 1000L * 1000L / (sentLastMks - sentFirstMks);
            return 0;
        }

        public long ReceivedRate()
        {
            if (receivedFirstMks != 0 && receivedLastMks != 0 && receivedLastMks != receivedFirstMks)
            {
                return receivedBytes * 1000L * 1000L / (receivedLastMks - receivedFirstMks);
            }
            return 0;

        }

        public long ReceivedBytes { get { return receivedBytes; } }
        public long SendBytes { get { return receivedBytes; } }

        private long startMks;
        private long sentFirstMks = 0;
        private long sentLastMks = 0;
        private long receivedFirstMks = 0;
        private long receivedLastMks = 0;
        private long sentBytes = 0;
        private long receivedBytes = 0;
        
        private long nanosecondsPerTick = (1000L*1000L*1000L) / Stopwatch.Frequency;
    }
}
