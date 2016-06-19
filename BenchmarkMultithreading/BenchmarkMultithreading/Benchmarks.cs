using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace BenchmarkMultithreading
{
  public class Benchmarks
  {
    #region public methods
    [Setup]
    public void Setup()
    {

    }

    [Benchmark]
    public void Lock()
    {

    }

    [Benchmark]
    public void MonitorEnter()
    {

    }
    #endregion
  }
}
