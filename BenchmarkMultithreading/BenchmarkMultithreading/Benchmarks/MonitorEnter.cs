using BenchmarkMultithreading.TestDomainObjects;
using System.Threading;

namespace BenchmarkMultithreading.Benchmarks
{
  internal class MonitorEnter : IBenchmarkable
  {
    #region private fields
    private Credit credits;
    private readonly int threadCount;
    private readonly int writesCount;
    #endregion

    #region IBenchmarkable
    public MonitorEnter(int threadCount, int writesCount)
    {
      this.threadCount = threadCount;
      this.writesCount = writesCount;

      credits = new Credit()
      {
        Security = 810,
        Amount = 1000000
      };
    }

    public void Run()
    {
      for (int i = 0; i < threadCount; i++)
      {
        Thread thread = new Thread(
          new ThreadStart(() =>
          {
            for (int j = 0; j < writesCount; j++)
            {

            }
            Monitor.Enter(credits);
            try
            {
              Client client =
                new Client()
                {
                  Name = "Kozlov Ivan Petrovich"
                };
              credits.clients.Add(client);
            }
            finally
            {
              Monitor.Exit(credits);
            }
          }));
        thread.Start();
      }
    }
    #endregion

  }
}
