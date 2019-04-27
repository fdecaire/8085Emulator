using Xunit;

namespace Processor.Tests
{
	public class AddSubtractTests
	{
		[Theory]
		[InlineData(0xf3, 0x45, 0x38, 0x01)]
		[InlineData(0x85, 0x1e, 0xa3, 0x94)]
		public void add_immediate(byte data, byte aregister, byte result, byte flagresult)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC + 1] = data;
			computer.A = aregister;
			computer.AddImmediate();

			Assert.Equal(result,computer.A);
			Assert.Equal(flagresult, computer.Flags.Register);
		}

		[Theory]
		[InlineData(0xf3, 0x45, false, 0x38, 0x01)]
		[InlineData(0xf3, 0x45, true, 0x39, 0x05)]
		public void add_immediate_with_carry(byte data, byte aregister, bool carryFlag, byte result, byte flagResults)
		{
			//c=0x01, p=0x04, ac=0x10, z=0x40, sign=0x80
			// FLAGS = cy=1, ac=0, p=0, z=0, s=0

			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC + 1] = data;
			computer.A = aregister;
			computer.Flags.Carry = carryFlag;
			computer.AddImmediateWithCarry();

			Assert.Equal(result, computer.A);
			Assert.Equal(flagResults, computer.Flags.Register);
		}

		[Theory]
		[InlineData(0xf3, 0x45, 0x38, 0x01)]
		[InlineData(0x85, 0x1e, 0xa3, 0x94)]
		public void add_register_b(byte eregister, byte aregister, byte result, byte flagresult)
		{
			//c=0x01, p=0x04, ac=0x10, z=0x40, sign=0x80
			// 
			// E = F3H
			// A = 45H   38H
			// FLAGS = cy=1, ac=0, p=0, z=0, s=0

			// E = 85H
			// A = 1EH  A3H
			// FLAGS = cy=0, ac=1, p=1, z=0, s=1
			// 94h

			var computer = new Computer();
			computer.Reset();
			computer.ComputerMemory[computer.PC] = 0xf8 + 0x03;
			computer.E = eregister;
			computer.A = aregister;
			computer.Add();

			Assert.Equal(result, computer.A);
			Assert.Equal(flagresult, computer.Flags.Register);
		}

		[Theory]
		[InlineData(0xf3, 0x45, false, 0x38, 0x01)]
		[InlineData(0xf3, 0x45, true, 0x39, 0x05)]
		public void add_register_b_with_carry(byte bregister, byte aregister, bool carryFlag, byte result, byte flagResults)
		{
			var computer = new Computer();
			computer.Reset();
			computer.ComputerMemory[computer.PC] = 0xf8 + 0x00;
			computer.B = bregister;
			computer.A = aregister;
			computer.Flags.Carry = carryFlag;
			computer.AddWithCarry();

			Assert.Equal(result, computer.A);
			Assert.Equal(flagResults, computer.Flags.Register);
		}

		[Fact]
		public void double_register_add()
		{
			var computer = new Computer();
			computer.Reset();
			computer.ComputerMemory[computer.PC] = 0x09 + 0x00;
			computer.B = 0;
			computer.C = 5;
			computer.H = 0;
			computer.L = 7;
			computer.DoubleRegisterAdd();

			Assert.Equal(12, computer.L);
            Assert.False(computer.Flags.Carry);
        }

		[Fact]
		public void double_register_add_with_carry()
		{
			var computer = new Computer();
			computer.Reset();
			computer.ComputerMemory[computer.PC] = 0x09 + 0x00;
			computer.B = 0xff;
			computer.C = 0xff;
			computer.H = 0;
			computer.L = 1;
			computer.DoubleRegisterAdd();

            Assert.True(computer.Flags.Carry);
        }

		[Theory]
		[InlineData(0x9b, 0x01,true,true)]
		[InlineData(0x33, 0x33, false, false)]
		public void decimal_adjust_accumulator(byte input1, byte result, bool carryFlag, bool auxCarry)
		{
			var computer = new Computer();
			computer.Reset();

			computer.A = input1;
			computer.DecimalAdjustAccumulator();

			Assert.Equal(result,computer.A);
			Assert.Equal(carryFlag,computer.Flags.Carry);
			Assert.Equal(auxCarry,computer.Flags.AuxCarry);
		}

		[Fact]
		public void subtract_immediate()
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC + 1] = 6;
			computer.A = 12;
			computer.SubtractImmediate();

			Assert.Equal(6, computer.A);
		}

		[Fact]
		public void subtract_immediate_with_borrow()
		{
			var computer = new Computer();
			computer.Reset();

			computer.Flags.Carry = true;
			computer.ComputerMemory[computer.PC + 1] = 6;
			computer.A = 12;
			computer.SubtractImmediateWithBorrow();

			Assert.Equal(5, computer.A);
		}

		[Theory]
		[InlineData(0,11)]
		[InlineData(1, 10)]
		[InlineData(2, 9)]
		[InlineData(3, 8)]
		[InlineData(4, 7)]
		[InlineData(5, 6)]
		[InlineData(7, 0)]
		public void subtract(byte regNum, int result)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = (byte) (0x90 + regNum);
			computer.A = 12;
			computer.B = 1;
			computer.C = 2;
			computer.D = 3;
			computer.E = 4;
			computer.H = 5;
			computer.L = 6;
			computer.Subtract();

			Assert.Equal(result, computer.A);
		}

		[Theory]
		[InlineData(0xf3, 0x45, 0xae, 0x80)]
		[InlineData(0x30, 0x20, 0x10, 0x10)]
		public void subtract_flag_results(byte accumulator, byte eregister, byte result, byte flags)
		{
			//ac=0x10,sign=0x80

			// A = F3H  after AEH
			// E = 45H
			// flags = cy=0, ac=0, s=1, p=0, z=0

			// A = 30H  after 10H
			// E = 20H
			// flags = cy=0, ac=1, s=1, p=0, z=0

			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = (byte)(0x90 + 0x03);

			computer.A = accumulator;
			computer.E = eregister;
			computer.Subtract();

			Assert.Equal(result,computer.A);
			Assert.Equal(flags, computer.Flags.Register);
		}

		[Theory]
		[InlineData(0, 10)]
		[InlineData(1, 9)]
		[InlineData(2, 8)]
		[InlineData(3, 7)]
		[InlineData(4, 6)]
		[InlineData(5, 5)]
		[InlineData(7, 0xff)]
		public void subtract_with_borrow(byte regNum, byte result)
		{
			var computer = new Computer();
			computer.Reset();

			computer.Flags.Carry = true;
			computer.ComputerMemory[computer.PC] = (byte)(0x90 + regNum);
			computer.A = 12;
			computer.B = 1;
			computer.C = 2;
			computer.D = 3;
			computer.E = 4;
			computer.H = 5;
			computer.L = 6;
			computer.SubtractWithBorrow();

			Assert.Equal(result, computer.A);
		}
	}
}
