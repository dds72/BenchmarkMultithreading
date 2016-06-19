using BenchmarkDotNet.Attributes;

namespace BenchmarkMultithreading
{
    public class BenchmarkContainer
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
    [Benchmark]
    public void ComparsionCollection()
    {

    }
    #endregion
  }
}
