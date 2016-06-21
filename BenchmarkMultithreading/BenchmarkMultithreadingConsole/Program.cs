using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;

using BenchmarkMultithreading;


namespace BenchmarkMultithreadingConsole
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      foreach (var arg in args)
      {
        switch (arg)
        {
          case "run":
          {
            BenchmarkRunner.Run<BenchmarkContainer>();
            break;
          }

          case "pause":
          {
            Console.WriteLine("Press ENTER to release console.");
            Console.ReadKey();
            break;
          }

          default:
          {
            break;
          }
        }
      }
    }
  }
}
