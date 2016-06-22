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

        public ComparsionCollectionConcurrent(int ThreadsCount, int NewRowsCount = 5000, int InitRowsCount = 10000)
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
                ThreadStart t2s = new ThreadStart(changeSource);
                Thread t2 = new Thread(t2s);
                threadsConcurrent[i] = t2;
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
            //start threads
            for (int i = 0; i < threadsCount; i++)
            {
                threadsConcurrent[i].Start();
            }
        }
    }
}