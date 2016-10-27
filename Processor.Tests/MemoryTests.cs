using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Processor.Tests
{
    public class MemoryTests
    {
	    [Fact]
	    public void set_memory()
	    {
		    var memory = new Memory();

			memory.Clear();
		    memory[0] = 5;

		    Assert.Equal(5, memory[0]);
	    }
		
    }
}
