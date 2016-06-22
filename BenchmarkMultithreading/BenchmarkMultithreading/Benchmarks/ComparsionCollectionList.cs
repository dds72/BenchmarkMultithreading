using System.Collections.Generic;
using System.Threading;

namespace BenchmarkMultithreading.Benchmarks
{
  class ComparsionCollectionList : IBenchmarkable, IComparsion
  {

    #region [.inits]
    const int NEW_ROWS_NUMBER = 5000;
    const int INIT_ROWS_NUMBER = 10000;

    private List<string> unsafeSource = new List<string>();
    private object SyncRoot = new object();
    private ulong changesSum = 0;

    Thread[] threadsLock;

    public ComparsionCollectionList(int threadsCount)
    {
      threadsInit(threadsCount);
      fillSource();
    }
    #endregion

    public void changeSource()
    {
      for (int i = 0; i < NEW_ROWS_NUMBER; i++)
      {
        lock (SyncRoot)
        {
          unsafeSource.Add("new value");
          changesSum++;
        }
      }
    }

    public void threadsInit(int count)
    {
      threadsLock = new Thread[count];
      for (int i = 0; i < count; i++)
      {
        threadsLock[i] = new Thread(new ThreadStart(changeSource));
      }
    }

    public void fillSource()
    {
      for (int i = 0; i < INIT_ROWS_NUMBER; i++)
      {
        var str = "default" + i;
        unsafeSource.Add(str);
      }
    }

    public void Run()
    {
      //Console.WriteLine("Number of elements at collection: {0}", unsafeSource.Count);

      //start threads
      for (int i = 0; i < threadsCount; i++)
      {
        threadsLock[i].Start();
      }
    }
  }
}
