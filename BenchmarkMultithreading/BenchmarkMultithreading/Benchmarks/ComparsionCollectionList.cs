using System;
using System.Collections.Generic;
using System.Threading;

namespace BenchmarkMultithreading.Benchmarks
{
    class ComparsionCollectionList : IBenchmarkable
    {

        #region [.inits]
        const int NEW_ROWS_NUMBER = 5000;
        const int THREADS_NUMBER = 10;
        const int INIT_ROWS_NUMBER = 10000;

        private List<string> unsafeSource;
        private object SyncRoot;
        private ulong changesSum;

        Thread[] threadsLock;
        #endregion

        private void changeSourceWithLock()
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

        private void threadsInit()
        {
            for (int i = 0; i < THREADS_NUMBER; i++)
            {
                ThreadStart t1s = new ThreadStart(changeSourceWithLock);
                Thread t1 = new Thread(t1s);
                threadsLock[i] = t1;
            }
        }

        private void fillSources()
        {
            for (int i = 0; i < INIT_ROWS_NUMBER; i++)
            {
                var str = "default" + i;
                unsafeSource.Add(str);
            }
        }

        public void Run()
        {
            Setup();
            fillSources();
            Console.WriteLine("Number of elements at collection: {0}", unsafeSource.Count);

            //start threads
            for (int i = 0; i < THREADS_NUMBER; i++)
            {
                threadsLock[i].Start();
            }
        }

        public void Setup()
        {
            unsafeSource = new List<string>();
            SyncRoot = new object();
            changesSum = 0;

            threadsLock = new Thread[THREADS_NUMBER];

            threadsInit();
        }
    }
}
