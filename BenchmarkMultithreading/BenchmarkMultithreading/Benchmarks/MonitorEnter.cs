using BenchmarkMultithreading.TestDomainObjects;
using System.Threading;

namespace BenchmarkMultithreading.Benchmarks
{
    class MonitorEnter : IBenchmarkable
  {
    #region private fields
    private Credit credits;

    public int ThreadCount { get { return 4; } }

    public int WritesCount { get { return 10; } }
    #endregion

    #region IBenchmarkable
    public void Run()
    {
      for (int i = 0; i < ThreadCount; i++)
      {
        Thread thread = new Thread(
          new ThreadStart(() =>
          {
            for (int j = 0; j < WritesCount; j++)
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

    public void Setup()
    {
      credits = new Credit()
      {
        Security = 810,
        Amount = 1000000
      };
    }
    #endregion

  }
}
