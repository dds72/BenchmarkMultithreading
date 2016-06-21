﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace BenchmarkMultithreading.Benchmarks
{
    class ComparsionCollectionList : IBenchmarkable, IComparsion
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

        public void threadsInit()
        {
            for (int i = 0; i < THREADS_NUMBER; i++)
            {
                ThreadStart t1s = new ThreadStart(changeSource);
                Thread t1 = new Thread(t1s);
                threadsLock[i] = t1;
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
            Setup();
            fillSource();
            //Console.WriteLine("Number of elements at collection: {0}", unsafeSource.Count);

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
