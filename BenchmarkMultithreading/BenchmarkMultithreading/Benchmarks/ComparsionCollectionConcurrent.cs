using System;
using System.Collections.Concurrent;
using System.Threading;

namespace BenchmarkMultithreading.Benchmarks
{
    class ComparsionCollectionConcurrent : IBenchmarkable, IComparsion
    {
        #region [.inits]
        const int NEW_ROWS_NUMBER = 5000;
        const int THREADS_NUMBER = 10;
        const int INIT_ROWS_NUMBER = 10000;

        private ConcurrentBag<string> safeSource = new ConcurrentBag<string>();
        private ulong changesSumConcurrent = 0;

        Thread[] threadsConcurrent = new Thread[THREADS_NUMBER];
        #endregion

        public void changeSource()
        {
            for (int i = 0; i < NEW_ROWS_NUMBER; i++)
            {
                safeSource.Add("new value");
                changesSumConcurrent++;
            }
        }

        public void threadsInit()
        {
            for (int i = 0; i < THREADS_NUMBER; i++)
            {
                ThreadStart t2s = new ThreadStart(changeSource);
                Thread t2 = new Thread(t2s);
                threadsConcurrent[i] = t2;
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
            threadsInit();
            fillSource();
            //Console.WriteLine("Number of elements at collection: {0}", safeSource.Count);

            //start threads
            for (int i = 0; i < THREADS_NUMBER; i++)
            {
                threadsConcurrent[i].Start();
            }
        }

        public void Setup()
        {
        }
    }
}