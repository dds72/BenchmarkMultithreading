using System;
using System.Collections.Concurrent;
using System.Threading;

namespace BenchmarkMultithreading.Benchmarks
{
    class ComparsionCollectionConcurrent : IBenchmarkable
    {
        #region [.inits]
        const int NEW_ROWS_NUMBER = 5000;
        const int THREADS_NUMBER = 10;
        const int INIT_ROWS_NUMBER = 10000;

        private ConcurrentBag<string> safeSource;
        private ulong changesSumConcurrent;

        Thread[] threadsConcurrent;
        #endregion

        private void changeSource()
        {
            for (int i = 0; i < NEW_ROWS_NUMBER; i++)
            {
                safeSource.Add("new value");
                changesSumConcurrent++;
            }
        }

        private void threadsInit()
        {
            for (int i = 0; i < THREADS_NUMBER; i++)
            {
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
                safeSource.Add(str);
            }
        }

        public void Run()
        {
            Setup();
            fillSources();
            Console.WriteLine("Number of elements at collection: {0}", safeSource.Count);

            //start threads
            for (int i = 0; i < THREADS_NUMBER; i++)
            {
                threadsConcurrent[i].Start();
            }
        }

        public void Setup()
        {
            safeSource = new ConcurrentBag<string>();
            changesSumConcurrent = 0;

            threadsConcurrent = new Thread[THREADS_NUMBER];

            threadsInit();
        }
    }
}