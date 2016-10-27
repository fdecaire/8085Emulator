using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Processor.Tests
{
	public class JumpAndSubroutineTests
	{
		[Fact]
		public void call()
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xcd;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Call();

			Assert.Equal(0xfffd, computer.SP);
			Assert.Equal(0x0510, computer.PC);
		}

		[Fact]
		public void return_instruction()
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xcd;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Call();
			computer.Return();

			Assert.Equal(0xffff, computer.SP);
			Assert.Equal(0x03, computer.PC);
		}

		[Fact]
		public void jump()
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xc3;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;
			computer.Jump();

			Assert.Equal(0x0510, computer.PC);
		}

		[Theory]
		[InlineData(true, 0x0510)]
		[InlineData(false, 0x0003)]
		public void jump_if_positive(bool positiveFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xc3;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Flags.Sign = !positiveFlag;

			computer.JumpIfPositive();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(true, 0x0510)]
		[InlineData(false, 0x0003)]
		public void jump_if_minus(bool positiveFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xc3;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Flags.Sign = positiveFlag;

			computer.JumpIfMinus();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(true, 0x0510)]
		[InlineData(false, 0x0003)]
		public void jump_if_zero(bool zeroFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xc3;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Flags.Zero = zeroFlag;

			computer.JumpIfZero();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(true, 0x0510)]
		[InlineData(false, 0x0003)]
		public void jump_if_not_zero(bool zeroFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xc3;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Flags.Zero = !zeroFlag;

			computer.JumpIfNotZero();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(true, 0x0510)]
		[InlineData(false, 0x0003)]
		public void jump_if_no_carry(bool carryFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xc3;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Flags.Carry = !carryFlag;

			computer.JumpIfNoCarry();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(true, 0x0510)]
		[InlineData(false, 0x0003)]
		public void jump_if_carry(bool carryFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xc3;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Flags.Carry = carryFlag;

			computer.JumpIfCarry();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(true, 0x0510)]
		[InlineData(false, 0x0003)]
		public void jump_if_parity_even(bool parityFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xc3;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Flags.Parity = parityFlag;

			computer.JumpIfParityEven();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(true, 0x0510)]
		[InlineData(false, 0x0003)]
		public void jump_if_parity_odd(bool parityFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xc3;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Flags.Parity = !parityFlag;

			computer.JumpIfParityOdd();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(true, 0x0510)]
		[InlineData(false, 0x0003)]
		public void call_if_carry(bool carryFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xc3;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Flags.Carry = carryFlag;

			computer.CallIfCarry();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(true, 0x0510)]
		[InlineData(false, 0x0003)]
		public void call_if_no_carry(bool carryFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xc3;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Flags.Carry = !carryFlag;

			computer.CallIfNoCarry();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(true, 0x0510)]
		[InlineData(false, 0x0003)]
		public void call_if_positive(bool signFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xc3;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Flags.Sign = !signFlag;

			computer.CallIfPositive();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(true, 0x0510)]
		[InlineData(false, 0x0003)]
		public void call_if_minus(bool signFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xc3;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Flags.Sign = signFlag;

			computer.CallIfMinus();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(true, 0x0510)]
		[InlineData(false, 0x0003)]
		public void call_if_zero(bool zeroFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xc3;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Flags.Zero = zeroFlag;

			computer.CallIfZero();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(true, 0x0510)]
		[InlineData(false, 0x0003)]
		public void call_if_not_zero(bool zeroFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xc3;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Flags.Zero = !zeroFlag;

			computer.CallIfNotZero();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(true, 0x0510)]
		[InlineData(false, 0x0003)]
		public void call_if_parity_even(bool parityFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xc3;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Flags.Parity = parityFlag;

			computer.CallIfParityEven();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(true, 0x0510)]
		[InlineData(false, 0x0003)]
		public void call_if_parity_odd(bool parityFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xc3;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Flags.Parity = !parityFlag;

			computer.CallIfParityOdd();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(false, 0x0511)]
		[InlineData(true, 0x0003)]
		public void return_if_positive(bool signFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xcd;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Call();

			computer.Flags.Sign = !signFlag;

			computer.ReturnIfPositive();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(false, 0x0511)]
		[InlineData(true, 0x0003)]
		public void return_if_negative(bool signFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xcd;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Call();

			computer.Flags.Sign = signFlag;

			computer.ReturnIfMinus();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(false, 0x0511)]
		[InlineData(true, 0x0003)]
		public void return_if_zero(bool zeroFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xcd;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Call();

			computer.Flags.Zero = zeroFlag;

			computer.ReturnIfZero();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(false, 0x0511)]
		[InlineData(true, 0x0003)]
		public void return_if_not_zero(bool zeroFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xcd;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Call();

			computer.Flags.Zero = !zeroFlag;

			computer.ReturnIfNotZero();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(false, 0x0511)]
		[InlineData(true, 0x0003)]
		public void return_if_parity_even(bool parityFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xcd;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Call();

			computer.Flags.Parity = parityFlag;

			computer.ReturnIfParityEven();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(false, 0x0511)]
		[InlineData(true, 0x0003)]
		public void return_if_parity_odd(bool parityFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xcd;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Call();

			computer.Flags.Parity = !parityFlag;

			computer.ReturnIfParityOdd();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(false, 0x0511)]
		[InlineData(true, 0x0003)]
		public void return_if_carry(bool carryFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xcd;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Call();

			computer.Flags.Carry = carryFlag;

			computer.ReturnIfCarry();

			Assert.Equal(address, computer.PC);
		}

		[Theory]
		[InlineData(false, 0x0511)]
		[InlineData(true, 0x0003)]
		public void return_if_no_carry(bool carryFlag, int address)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0xcd;
			computer.ComputerMemory[computer.PC + 1] = 0x10;
			computer.ComputerMemory[computer.PC + 2] = 0x05;

			computer.Call();

			computer.Flags.Carry = !carryFlag;

			computer.ReturnIfNoCarry();

			Assert.Equal(address, computer.PC);
		}
	}
}
