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

			Assert.False(computer.Flags.Carry);
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

		[Theory]
		[InlineData(0x25, 0xf5, 0x15)]
		[InlineData(0xF5, 0xd5, 0x10)]
		[InlineData(0x50, 0x70, 0x81)]
		public void compare_immediate_with_accumulator(byte accumulator, byte tempData, byte flags)
		{
			//c=0x01, p=0x04, ac=0x10, z=0x40, sign=0x80
			// A = 25H
			// temp = 45H	30H
			// F = cy=1,ac=1,z=0,p=1,s=0

			// A = F5H
			// E = d5H
			// temp = 45H	20H
			// F = cy=0,ac=1,z=0,p=0,s=0

			var computer = new Computer();
			computer.Reset();

			computer.A = accumulator;
			computer.ComputerMemory[computer.PC] = 0xfe;
			computer.ComputerMemory[computer.PC + 1] = tempData;
			computer.CompareImmediate();

			Assert.Equal(computer.Flags.Carry, (flags & 0x01) > 0);
			Assert.Equal(computer.Flags.AuxCarry, (flags & 0x10) > 0);
			Assert.Equal(computer.Flags.Zero, (flags & 0x40) > 0);
			Assert.Equal(computer.Flags.Parity, (flags & 0x04) > 0);
			Assert.Equal(computer.Flags.Sign, (flags & 0x80) > 0);

			Assert.Equal(flags, computer.Flags.Register);
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
