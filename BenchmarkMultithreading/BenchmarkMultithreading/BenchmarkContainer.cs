using BenchmarkDotNet.Attributes;
using BenchmarkMultithreading.Benchmarks;

namespace BenchmarkMultithreading
{
    public class BenchmarkContainer
  {
    #region [.private fields]
    private MonitorEnter monitorEnter;
    private Lock lockBench;
    private ComparsionCollectionList testList;
    private ComparsionCollectionConcurrent testConcurrent;
    #endregion

	#region [.public properties]
    public int ThreadsCount => 4;
    public int WritesCount => 10;

    public int CollectionThreadsCount => 10;
	#endregion
	
    #region [.public methods]
    [Setup]
    public void Setup()
    {
      monitorEnter = new MonitorEnter(ThreadsCount, WritesCount);
      lockBench = new Lock(ThreadsCount, WritesCount);
      testList = new ComparsionCollectionList(CollectionThreadsCount);
      testConcurrent = new ComparsionCollectionConcurrent(CollectionThreadsCount);
    }

//    [Benchmark]
    public void Lock()
    {
      lockBench.Run();
    }

//    [Benchmark]
    public void MonitorEnter()
    {
      monitorEnter.Run();
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
