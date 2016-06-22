using System;
using System.Collections.Concurrent;
using System.Threading;

namespace BenchmarkMultithreading.Benchmarks
{
  class ComparsionCollectionConcurrent : IBenchmarkable, IComparsion
  {
    #region [.inits]
    const int NEW_ROWS_NUMBER = 5000;
    const int INIT_ROWS_NUMBER = 10000;

    private readonly ConcurrentBag<string> safeSource = new ConcurrentBag<string>();
    private ulong changesSumConcurrent = 0;
    private readonly int threadsCount;

    private Thread[] threadsConcurrent;

    public ComparsionCollectionConcurrent(int threadsCount)
    {
      this.threadsCount = threadsCount;
      threadsConcurrent = new Thread[threadsCount];
      threadsInit(threadsCount);
      fillSource();
      //Console.WriteLine("Number of elements at collection: {0}", safeSource.Count);
    }
    #endregion

    public void changeSource()
    {
      for (int i = 0; i < NEW_ROWS_NUMBER; i++)
      {
        safeSource.Add("new value");
        changesSumConcurrent++;
      }
    }

    public void threadsInit(int count)
    {
      threadsConcurrent = new Thread[count];
      for (int i = 0; i < count; i++)
      {
        threadsConcurrent[i] = new Thread(new ThreadStart(changeSource));
      }
    }

    public void fillSource()
    {
      for (int i = 0; i < INIT_ROWS_NUMBER; i++)
      {
        var str = "default" + i;
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