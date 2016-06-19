using BenchmarkMultithreading.TestDomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkMultithreading.Benchmarks
{
  class MonitorEnter : IBenchmarkable
  {
    #region private fields
    private Client data;
    #endregion

    #region IBenchmarkable
    public void Run()
    {
      throw new NotImplementedException();
    }

    public void Setup()
    {
      throw new NotImplementedException();
    }
    #endregion

  }
}
