using BenchmarkDotNet.Attributes;
using BenchmarkMultithreading.Benchmarks;

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
    public void ComparsionCollectionList()
    {
        var testObj = new ComparsionCollectionList();
        testObj.Run();
    }
    [Benchmark]
    public void ComparsionCollectionConcurrent()
    {
        var testObj = new ComparsionCollectionConcurrent();
        testObj.Run();
    }
        #endregion
    }
}
