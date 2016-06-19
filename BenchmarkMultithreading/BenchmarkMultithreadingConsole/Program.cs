using System;
using BenchmarkDotNet.Running;
using BenchmarkMultithreading;

namespace BenchmarkMultithreadingConsole
{
    class Program
  {
        static void Main(string[] args)
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
                }
            }
        }

  }
}
