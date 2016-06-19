using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkMultithreading.TestDomainObjects
{
  internal class Credit
  {
    public int Amount { get; set; }
    public byte Security { get; set; }
    public List<Client> clients { get; set; } = new List<Client>();
  }
}
