using BenchmarkDotNet.Attributes;
using BenchmarkMultithreading.Benchmarks;

namespace BenchmarkMultithreading
{
    public class BenchmarkContainer
  {
    #region [.private fields]
    private ComparsionCollectionList testList;
    private ComparsionCollectionConcurrent testConcurrent;
    #endregion

    #region public methods
    [Setup]
    public void Setup()
    {
        testList = new ComparsionCollectionList();
        testConcurrent = new ComparsionCollectionConcurrent();
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
        testList.Run();
    }
    [Benchmark]
    public void ComparsionCollectionConcurrent()
    {
        testConcurrent.Run();
    }
    #endregion
    }
}
