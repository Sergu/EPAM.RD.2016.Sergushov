using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WcfClient.UserStorageServ;

namespace WcfClient
{
    class Program
    {
        private static Random random = new Random();
        static void Main(string[] args)
        {
            Mutex mutex;
            while (!Mutex.TryOpenExisting("mutex", out mutex))
                Thread.Sleep(100);
            mutex.WaitOne();

            var service = new UserStorageServiceClient();
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var start = new ManualResetEventSlim(false);

            var exampleUser = new UserBll { LastName = "foligno", FirstName = "nick", VisaRecords = new List<VisaRecord>().ToArray()};

            WaitCallback callService = (object state) =>
            {
                start.Wait();
                while (true)
                {
                    if (token.IsCancellationRequested)
                        break;

                    service.Add(exampleUser);
                    Console.WriteLine("add: Name - {0}", exampleUser.FirstName);
                    var searchResult = service.Search(new FirstNameCriteria { Name = "nick" });
                    Console.WriteLine("search: {0}", searchResult.Count());
                    var numb = random.Next(1, 4);
                    if(numb == 1)
                    {
                        if (searchResult.Count() > 0)
                        {
                            var removedUserIdInCollection = random.Next(0, searchResult.Count() - 1);
                            var removedUser = searchResult[removedUserIdInCollection];
                            service.Delete(removedUser.Id);
                            Console.WriteLine("delete: id - {0}", removedUser.Id);
                        }
                    }

                    Thread.Sleep(5000);
                }
            };

            for (int i = 0; i < 1; i++)
            {
                ThreadPool.QueueUserWorkItem(callService);
            }

            Console.WriteLine("Threads will started.");
            Console.WriteLine("Press any key to stop");
            start.Set();
            Console.ReadLine();
            cts.Cancel();
            Console.WriteLine("Threads stoped");
            Console.ReadLine();
        }
    }
}
