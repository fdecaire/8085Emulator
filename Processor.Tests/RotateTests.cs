using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Processor.Tests
{
	public class RotateTests
	{
		[Theory]
		[InlineData(0x55, 0xaa, false)]
		[InlineData(0xaa, 0x55, true)]
		public void rotate_accumulator_left(byte data, byte result, bool carryFlag)
		{
			var computer = new Computer();
			computer.Reset();

			computer.A = data;
			computer.RotateAccumulatorLeft();

			Assert.Equal(result, computer.A);
			Assert.Equal(carryFlag, computer.Flags.Carry);
		}

		[Theory]
		[InlineData(0xaa, 0x55, false)]
		[InlineData(0x55, 0xaa, true)]
		public void rotate_accumulator_right(byte data, byte result, bool carryFlag)
		{
			var computer = new Computer();
			computer.Reset();

			computer.A = data;
			computer.RotateAccumulatorRight();

			Assert.Equal(result, computer.A);
			Assert.Equal(carryFlag, computer.Flags.Carry);
		}

		[Theory]
		[InlineData(0xaa, 0x55, false)]
		[InlineData(0x55, 0x2a, true)]
		public void rotate_right_through_carry(byte data, byte result, bool carryFlag)
		{
			var computer = new Computer();
			computer.Reset();

			computer.A = data;
			computer.RotateRightThroughCarry();

			Assert.Equal(result, computer.A);
			Assert.Equal(carryFlag, computer.Flags.Carry);
		}

		[Theory]
		[InlineData(0xaa, 0x54, true)]
		[InlineData(0x55, 0xaa, false)]
		public void rotate_left_through_carry(byte data, byte result, bool carryFlag)
		{
			var computer = new Computer();
			computer.Reset();

			computer.A = data;
			computer.RotateLeftThroughCarry();

			Assert.Equal(result, computer.A);
			Assert.Equal(carryFlag, computer.Flags.Carry);
		}
	}
}
