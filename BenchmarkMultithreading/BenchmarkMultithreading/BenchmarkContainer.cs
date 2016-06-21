using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkMultithreading.Benchmarks;

namespace BenchmarkMultithreading
{
  public class BenchmarkContainer
  {
    #region private fields
    private MonitorEnter monitorEnter;
    private Lock lockBench;
    #endregion

    public int ThreadsCount => 4;
    public int WritesCount => 10;

    #region public methods
    [Setup]
    public void Setup()
    {
      monitorEnter = new MonitorEnter(ThreadsCount, WritesCount);
      lockBench = new Lock(ThreadsCount, WritesCount);
    }

    [Benchmark]
    public void Lock()
    {
      lockBench.Run();
    }

    [Benchmark]
    public void MonitorEnter()
    {
      monitorEnter.Run();
    }
    #endregion
  }
}
