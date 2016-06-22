using System.Collections.Generic;
using System.Threading;

namespace BenchmarkMultithreading.Benchmarks
{
    class ComparsionCollectionList : IBenchmarkable, IComparsion
    {

        #region [.inits]
        private List<string> unsafeSource = new List<string>();
        private object SyncRoot = new object();
        private readonly int threadsCount;
        private readonly int newRowsCount;
        private readonly int initRowsCount;

        private Thread[] threadsLock;

        public ComparsionCollectionList(int ThreadsCount, int NewRowsCount = 5000, int InitRowsCount = 10000)
        {
            this.threadsCount = ThreadsCount;
            this.newRowsCount = NewRowsCount;
            this.initRowsCount = InitRowsCount;
            this.threadsLock = new Thread[threadsCount];

            threadsInit();
            fillSource();
        }
        #endregion

        public void changeSource()
        {
            for (int i = 0; i < newRowsCount; i++)
            {
                lock (SyncRoot)
                {
                    unsafeSource.Add("new value");
                }
            }
        }

        public void threadsInit()
        {
            for (int i = 0; i < threadsCount; i++)
            {
                ThreadStart t1s = new ThreadStart(changeSource);
                Thread t1 = new Thread(t1s);
                threadsLock[i] = t1;
            }
        }

        public void fillSource()
        {
            for (int i = 0; i < initRowsCount; i++)
            {
                var str = "default";
                unsafeSource.Add(str);
            }
        }

        public void Run()
        {
            //start threads
            for (int i = 0; i < threadsCount; i++)
            {
                threadsLock[i].Start();
            }
        }
    }
}
