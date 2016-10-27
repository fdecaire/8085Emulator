using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Processor.Tests
{
	public class PushPopTests
	{
		[Theory]
		[InlineData(0xc5)]
		[InlineData(0xd5)]
		[InlineData(0xe5)]
		[InlineData(0xf5)]
		public void push_instruction(byte instruction)
		{
			var result = InstructionDecoder.Decode(instruction);
			Assert.Equal(InstructionMnemonic.PUSH, result);
		}

		[Theory]
		[InlineData(0xc1)]
		[InlineData(0xd1)]
		[InlineData(0xe1)]
		[InlineData(0xf1)]
		public void pop_instruction(byte instruction)
		{
			var result = InstructionDecoder.Decode(instruction);
			Assert.Equal(InstructionMnemonic.POP, result);
		}

		[Fact]
		public void push_bc()
		{
			var computer = new Computer();
			computer.Reset();

			computer.SP = 0xffff;
			computer.B = 0xca;
			computer.C = 0x54;
			computer.ComputerMemory[computer.PC] = 0xc5;
			computer.Push();

			Assert.Equal(0xca, computer.ComputerMemory[0xfffe]);
			Assert.Equal(0x54, computer.ComputerMemory[0xfffd]);
		}

		[Fact]
		public void pop_bc()
		{
			var computer = new Computer();
			computer.Reset();

			computer.SP = 0xfffd;
			computer.ComputerMemory[computer.PC] = 0xc1;
			computer.ComputerMemory[0xfffe] = 0x7e;
			computer.ComputerMemory[0xfffd] = 0x3a;
			computer.Pop();

			Assert.Equal(0x7e, computer.B);
			Assert.Equal(0x3a, computer.C);
		}

		[Fact]
		public void push_psw()
		{
			var computer = new Computer();
			computer.Reset();

			computer.SP = 0xffff;
			computer.A = 0xca;
			computer.Flags.Register = 0x54;
			computer.ComputerMemory[computer.PC] = 0xf5;
			computer.Push();

			Assert.Equal(0xca, computer.ComputerMemory[0xfffe]);
			Assert.Equal(0x54, computer.ComputerMemory[0xfffd]);
		}

		[Fact]
		public void pop_psw()
		{
			var computer = new Computer();
			computer.Reset();

			computer.SP = 0xfffd;
			computer.ComputerMemory[computer.PC] = 0xf1;
			computer.ComputerMemory[0xfffe] = 0x7e;
			computer.ComputerMemory[0xfffd] = 0xc4;
			computer.Pop();

			Assert.Equal(0x7e, computer.A);
			Assert.Equal(0xc4, computer.Flags.Register);
		}
	}
}
