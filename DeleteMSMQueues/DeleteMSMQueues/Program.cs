using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace DeleteMSMQueues
{
    class Program
    {
        static void Main(string[] args)
        {
            int queueSize;

            if (args.Length != 1)
            {
                Console.WriteLine("Invalid arguments need NumberofQueues");
                return;
            }

            Int32.TryParse(args[0], out queueSize);
            Console.WriteLine("Arguments NumberofQueues " + queueSize);

            for (int i = 1; i <= queueSize; i++)
            {
                string queuePath = @".\private$\" + "TestQueue" + i;

                try
                {
                    if (MessageQueue.Exists(queuePath))
                    {

                        // Connect to a queue on the local computer.
                        MessageQueue myQueue = new MessageQueue(queuePath);

                        myQueue.Close();
                        MessageQueue.Delete(queuePath);

                        Console.WriteLine("Delete " + queuePath);
                    }
                    else
                    {
                        Console.WriteLine("Error Queue not found " + queuePath);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception " + queuePath + " " + ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : ""));
                }
            }
        }
    }
}
