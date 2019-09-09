using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace webtest
{
    class SimplePageTest : IRunTest
    {
        public SimplePageTest(string url)
        {
            Url = url;
        }
        public string Url { get; set; }

        public int StartTest(IMeasurement measurement)
        {
            this.measurement = measurement;
            SimplePageTest test = this;
            new Thread(() =>
            {
                test.RunTest();                
            }).Start();
            return 0;
        }

        void RunTest()
        {
            measurement.Start();
            measurement.Sent(100);
            measurement.DoneSend();
            Completed = false;
            Progress = 0;
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            System.IO.Stream data = client.OpenRead(Url);
            StreamReader reader = new StreamReader(data);
            Progress = 0;
            string s = reader.ReadToEnd();
            Progress = 100;
            measurement.Received((uint) s.Length);
            Completed = true;
        }

        public int Progress { get; private set; }
        public bool Completed { get; private set; }
        public IMeasurement GetMeasurement() { return measurement; }

        private IMeasurement measurement;

    }
}
