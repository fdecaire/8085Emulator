using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Processor.Tests
{
	public class LogicalTests
	{
		[Fact]
		public void and_b_with_accumulator()
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xa0 + 0x00;
			computer.A = 0x33;
			computer.B = 0x03;
			computer.LogicalAndWithAccumulator();

			Assert.Equal(0x03, computer.A);
		}

		[Fact]
		public void inclusive_or_with_accumulator()
		{
			var computer = new Computer();
			computer.Reset();

			computer.A = 0x30;
			computer.B = 0x25;
			computer.ComputerMemory[computer.PC] = 0xb0 + 0x00;
			computer.InclusiveORWithAccumulator();

			Assert.Equal(0x35, computer.A);
		}

		[Fact]
		public void and_immediate_with_assumulator()
		{
			var computer = new Computer();
			computer.Reset();

			computer.A = 0x30;
			computer.ComputerMemory[computer.PC] = 0xe6;
			computer.ComputerMemory[computer.PC + 1] = 0x25;
			computer.LogicalAndWithAccumulator();

			Assert.Equal(0x20, computer.A);
		}

		[Fact]
		public void complement_accumulator()
		{
			var computer = new Computer();
			computer.Reset();

			computer.A = 0x35;
			computer.ComplementAccoumulator();

			Assert.Equal(0xca, computer.A);
		}

		[Fact]
		public void complement_carry()
		{
			var computer = new Computer();
			computer.Reset();
			computer.Flags.Carry = true;

			computer.ComplementCarry();

			Assert.Equal(false, computer.Flags.Carry);
		}

		[Fact]
		public void inclusive_or_immediate()
		{
			var computer = new Computer();
			computer.Reset();

			computer.A = 0x35;
			computer.ComputerMemory[computer.PC + 1] = 0x42;
			computer.InclusiveOrImmediate();

			Assert.Equal(0x77, computer.A);
		}

		[Fact]
		public void exclusive_or_c_with_accumulator()
		{
			var computer = new Computer();
			computer.Reset();

			computer.A = 0xaa;
			computer.C = 0x0f;
			computer.ComputerMemory[computer.PC] = 0xb8 + 0x01;

			computer.ExclusiveOrWithAccumulator();

			Assert.Equal(0xa5, computer.A);
		}

		[Fact]
		public void exclusive_or_immediate()
		{
			var computer = new Computer();
			computer.Reset();

			computer.A = 0xaa;
			computer.ComputerMemory[computer.PC] = 0xee;
			computer.ComputerMemory[computer.PC + 1] = 0x0f;

			computer.ExclusiveOrImmediate();

			Assert.Equal(0xa5, computer.A);
		}

		[Fact]
		public void and_immediate_with_accumulator()
		{
			var computer = new Computer();
			computer.Reset();

			computer.A = 0xaa;
			computer.ComputerMemory[computer.PC] = 0xe6;
			computer.ComputerMemory[computer.PC + 1] = 0x0f;
			computer.AndImmediateWithAccumulator();

			Assert.Equal(0x0a,computer.A);
		}

		[Fact]
		public void compare_immediate_with_accumulator()
		{
			var computer = new Computer();
			computer.Reset();

			computer.A = 0xaa;
			computer.ComputerMemory[computer.PC] = 0xe6;
			computer.ComputerMemory[computer.PC + 1] = 0xaa;
			computer.CompareImmediate();

			Assert.Equal(true, computer.Flags.Zero);
		}

		[Theory]
		[InlineData(0xb8,false)]
		[InlineData(0xb9,true)]
		public void compare_with_accumulator(byte instruction, bool result)
		{
			var computer = new Computer();
			computer.Reset();

			computer.A = 0xaa;
			computer.B = 0xcc;
			computer.C = 0xaa;
			computer.D = 0xaa;
			computer.E = 0xab;
			computer.ComputerMemory[computer.PC] = instruction;
			computer.ComputerMemory[computer.PC + 1] = 0xaa;
			computer.CompareWithAccumulator();

			Assert.Equal(result, computer.Flags.Zero);
		}
	}
}
