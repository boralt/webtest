
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webtest
{
    class MeasureSimple : public IMesurement
    {

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


        }


        private Uint64 startMks;
        private Uint64 sentFirstMks;
        private Uint64 receivedFirstMks;
        private Uint64 receivedLastMks;
        private Uint64 sentBytes;
        private Uint64 receivedBytes;
    }
}
