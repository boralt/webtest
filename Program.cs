using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace webtest
{
    class Program
    {
        static void Main(string[] args)
        {
            string sUrl = "http://www.cnn.com";

            if (args.Length > 0)
                sUrl = args[0];

            Console.WriteLine("Proceed with {0}", sUrl);

            IMeasurement measurement = new MeasureSimple();
            IRunTest test = new SimplePageTest(sUrl);
            test.StartTest(measurement);

            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(10);
                if (test.Completed)
                {
                    break;
                }
            }
            if (test.Completed)
            {
                Console.WriteLine("Test completed. Latency {0} mks Bytes {1}", measurement.ReceiveLatency, measurement.ReceivedBytes);
            }
        }
    }
}
