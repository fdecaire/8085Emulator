using Xunit;

namespace Processor.Tests
{
	public class DecrementIncrementTests
	{
		[Fact]
		public void decrement_breg()
		{
			var computer = new Computer();
			computer.Reset();
			computer.ComputerMemory[computer.PC] = 0x05 + 0x00;
			computer.B = 7;
			computer.Decrement();

			Assert.Equal(6,computer.B);

			//TODO: assert auxcarry flag
		}

		[Fact]
		public void decrement_register_pair_cd()
		{
			var computer = new Computer();
			computer.Reset();
			computer.ComputerMemory[computer.PC] = 0x0b + 0x00;
			computer.C = 7;
			computer.DecrementRegisterPair();

			Assert.Equal(6, computer.C);
		}

		[Fact]
		public void increment_register()
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0x04 + 0x08;
			computer.C = 5;
			computer.IncrementRegister();

			Assert.Equal(6, computer.C);
		}
	}
}
