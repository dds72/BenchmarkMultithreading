using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BenchmarkMultithreadingConsole
{
  class Program
  {
    #region [.consts]
    const int NEW_ROWS_NUMBER = 5000;
    const int THREADS_NUMBER = 10;
    const int INIT_ROWS_NUMBER = 10000;
    #endregion

    #region [.init]
    private static List<string> unsafeSource = new List<string>();
    private static ConcurrentBag<string> safeSource = new ConcurrentBag<string>();
    private static object SyncRoot = new object();
    private static ulong changesSum = 0;
    private static ulong changesSumConcurrent = 0;

    static Thread[] threadsLock = new Thread[THREADS_NUMBER];
    static Thread[] threadsConcurrent = new Thread[THREADS_NUMBER]; 
    #endregion

    static void Main(string[] args)
    {
        fillSources();
        Console.WriteLine("Number of elements at collection: {0}", unsafeSource.Count);

        threadsInit();

        //start threads
        for (int i = 0; i < THREADS_NUMBER; i++)
        {
            threadsLock[i].Start();
            threadsConcurrent[i].Start();
        }

        Console.ReadKey();       
    }

    public static void changeSourceWithLock()
    {
        for (int i = 0; i < NEW_ROWS_NUMBER; i++)
        {
            lock (SyncRoot)
            {
                unsafeSource.Add("with lock");
                changesSum++;
            } 
        }
    }

    public static void changeSource()
    {
        for (int i = 0; i < NEW_ROWS_NUMBER; i++)
        {
            safeSource.Add("with concurrent");
            changesSumConcurrent++; 
        }
    }

    public static void threadsInit()
    {
        for (int i = 0; i < THREADS_NUMBER; i++)
        {
            ThreadStart t1s = new ThreadStart(changeSourceWithLock);
            Thread t1 = new Thread(t1s);
            threadsLock[i] = t1;

            ThreadStart t2s = new ThreadStart(changeSource);
            Thread t2 = new Thread(t2s);
            threadsConcurrent[i] = t2;
        }
    }

    public static void fillSources()
    {
        for (int i = 0; i < INIT_ROWS_NUMBER; i++)
        {
            var str = "default" + i;
            unsafeSource.Add(str);
            safeSource.Add(str);
        }
    }
  }
}
