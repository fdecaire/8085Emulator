using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Processor.Tests
{
	public class CodeTests
	{
		[Fact]
		public void hello_world()
		{
			var computer = new Computer();
			computer.ComputerMemory.LoadMachineCodeDirect(":1D0000002110007EB7CA0E00D30123C30300760048656C6C6F20576F726C64210035:00000001FF");
			computer.Run();

			var output = computer.OutputPorts[1].GetText();

			Assert.Equal("Hello World!", output);
		}
	}
}
