using System.Collections.Concurrent;
using System.Threading;

namespace BenchmarkMultithreading.Benchmarks
{
  class ComparsionCollectionConcurrent : IBenchmarkable, IComparsion
  {
    #region [.inits]
    private ConcurrentBag<string> safeSource = new ConcurrentBag<string>();
    private readonly int threadsCount;
    private readonly int newRowsCount;
    private readonly int initRowsCount;

    private Thread[] threadsConcurrent;

    public ComparsionCollectionConcurrent(
      int ThreadsCount,
      int NewRowsCount = 5000,
      int InitRowsCount = 10000)
    {
      this.threadsCount = ThreadsCount;
      this.newRowsCount = NewRowsCount;
      this.initRowsCount = InitRowsCount;
      this.threadsConcurrent = new Thread[threadsCount];

      threadsInit();
      fillSource();
    }
    #endregion

    public void changeSource()
    {
      for (int i = 0; i < newRowsCount; i++)
      {
        safeSource.Add("new value");
      }
    }

    public void threadsInit()
    {
      for (int i = 0; i < threadsCount; i++)
      {
        threadsConcurrent[i] =
          new Thread(
            new ThreadStart(changeSource));
      }
    }

    public void fillSource()
    {
      for (int i = 0; i < initRowsCount; i++)
      {
          var str = "default";
          safeSource.Add(str);
      }
    }

    public void Run()
    {
      for (int i = 0; i < threadsCount; i++)
      {
        threadsConcurrent[i].Start();
      }
    }
  }
}