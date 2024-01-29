using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Messaging;

namespace MSMQReceiveMessage
{
    public class TestMessage
    {
        public double datetime;
        public Int64 counter;
        public String Buffer1KB;
    };

    class Program
    {
        static void Main(string[] args)
        {
            int queueSize;
            int waitTime;

            if (args.Length != 2)
            {
                Console.WriteLine("Invalid arguments need NumberofQueues ReceiveMessagesEveryXMilli");
                return;
            }

            Int32.TryParse(args[0], out queueSize);
            Int32.TryParse(args[1], out waitTime);
            Console.WriteLine("Arguments NumberofQueues " + queueSize + " ReceiveMessagesEveryXMilli" + waitTime);

            while (true) // Receive messages until application is stopped
            {

                for (int i = 1; i <= queueSize; i++)
                {
                    string queuePath = @".\private$\" + "TestQueue" + i;

                    try
                    {
                        if (MessageQueue.Exists(queuePath))
                        {

                            // Connect to a queue on the local computer.
                            MessageQueue myQueue = new MessageQueue(queuePath);

                            // Set the formatter to indicate body contains an Order.
                            myQueue.Formatter = new XmlMessageFormatter(new Type[]
                                {typeof(TestMessage)});

                            Message myMessage = myQueue.Receive(new TimeSpan(0, 0, 0));
                            TestMessage receiveTestMessage = (TestMessage)myMessage.Body;

                            double timeDiff = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds) - receiveTestMessage.datetime;

                            Console.WriteLine("Received " + queuePath + " Counter " + receiveTestMessage.counter + " Timestamp " + receiveTestMessage.datetime + " DelayinMilli " + timeDiff);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception " + queuePath + " " + ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""));
                    }
                }

                Thread.Sleep(waitTime);
            }
        }
    }
}
