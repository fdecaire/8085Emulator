using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Processor.Tests
{
	public class InterruptTests
	{
		[Fact]
		public void disable_interrupts()
		{
			var computer = new Computer();
			computer.Reset();
			computer.DisableInterrupts();

			Assert.False(computer.InterruptsEnabled);
		}

		[Fact]
		public void enable_interrupts()
		{
			var computer = new Computer();
			computer.Reset();
			computer.EnableInterrupts();

			Assert.True(computer.InterruptsEnabled);
		}
	}
}
