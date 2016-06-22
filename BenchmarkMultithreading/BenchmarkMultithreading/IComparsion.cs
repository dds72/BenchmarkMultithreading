using System;

namespace BenchmarkMultithreading
{
    internal interface IComparsion
    {
      void fillSource();
      void changeSource();
      void threadsInit();
    }
}
