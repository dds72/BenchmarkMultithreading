using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace BenchmarkMultithreading.Benchmarks
{
    class ComparsionCollection : IBenchmarkable
    {

        #region [.inits]
        const int NEW_ROWS_NUMBER = 5000;
        const int THREADS_NUMBER = 10;
        const int INIT_ROWS_NUMBER = 10000;

        private List<string> unsafeSource;
        private ConcurrentBag<string> safeSource;
        private object SyncRoot;
        private ulong changesSum;
        private ulong changesSumConcurrent;

        Thread[] threadsLock;
        Thread[] threadsConcurrent;
        #endregion

        private void changeSourceWithLock()
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

        private void changeSource()
        {
            for (int i = 0; i < NEW_ROWS_NUMBER; i++)
            {
                safeSource.Add("with concurrent");
                changesSumConcurrent++;
            }
        }

        private void threadsInit()
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

        private void fillSources()
        {
            for (int i = 0; i < INIT_ROWS_NUMBER; i++)
            {
                var str = "default" + i;
                unsafeSource.Add(str);
                safeSource.Add(str);
            }
        }

        public void Run()
        {
            fillSources();
            Console.WriteLine("Number of elements at collection: {0}", unsafeSource.Count);

            //start threads
            for (int i = 0; i < THREADS_NUMBER; i++)
            {
                threadsLock[i].Start();
                threadsConcurrent[i].Start();
            }
        }

        public void Setup()
        {
            unsafeSource = new List<string>();
            safeSource = new ConcurrentBag<string>();
            SyncRoot = new object();
            changesSum = 0;
            changesSumConcurrent = 0;

            threadsLock = new Thread[THREADS_NUMBER];
            threadsConcurrent = new Thread[THREADS_NUMBER];

            threadsInit();
        }
    }
}
