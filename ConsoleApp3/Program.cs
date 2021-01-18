using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            var queue = new ConcurrentLIFOQueue<int>(10);

            for (int i = 0; i < 10; i++)
            {
                Task.Run(() => EnqueueAndTryDequeue(queue));
            }



            Task.Run(() => Dequeue(queue));
            Task.Run(() => Dequeue(queue));

            Thread.Sleep(3000);

            Console.ReadLine();
        }
        private static void EnqueueAndTryDequeue(ConcurrentLIFOQueue<int> queue)
        {
            int result;
            for (int i = 0; i < queue.Length; i++)
            {
                queue.Enqueue(i);
                queue.TryDequeue(out result);
            }
            Console.WriteLine("finish");
        }

        private static void Dequeue(ConcurrentLIFOQueue<int> queue)
        {
            bool result_bool = true;
            int result;
            while (result_bool)
            {
                if (queue.Count() > 0)
                {
                    result_bool = queue.TryDequeue(out result);
                }
                else
                {
                    result_bool = false;
                }
            }

            Console.WriteLine("Done");
        }
    }
}
