using BenchmarkMultithreading.TestDomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BenchmarkMultithreading.Benchmarks
{
  internal class Lock : IBenchmarkable
  {
    private Credit credits;
    private readonly int threadCount;
    private readonly int writesCount;

    public Lock(int threadCount, int writesCount)
    {
      this.threadCount = threadCount;
      this.writesCount = writesCount;

      credits = new Credit()
      {
        Security = 810,
        Amount = 1000000
      };
    }

    #region IBenchmarkable
    public void Run()
    {
      for (int i = 0; i < threadCount; i++)
      {
        Thread thread = new Thread(
          new ThreadStart(() =>
          {
            for (int j = 0; j < writesCount; j++)
            {
              lock (credits)
              {
                Client client =
                  new Client()
                  {
                    Name = "Kozlov Ivan Petrovich"
                  };
                credits.clients.Add(client);
              }
            }
          }));
        thread.Start();
      }
    }
    #endregion
  }
}
