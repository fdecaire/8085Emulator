using Xunit;

namespace Processor.Tests
{
	public class LoadAndStoreTests
	{
		[Theory]
		[InlineData(0x02)]
		[InlineData(0x12)]
		public void store_accumulator_indirect_instruction(byte instruction)
		{
			var result = InstructionDecoder.Decode(instruction);
			Assert.Equal(InstructionMnemonic.STAX, result);
		}

		[Theory]
		[InlineData(0x0a)]
		[InlineData(0x1a)]
		public void load_accumulator_indirect_instruction(byte instruction)
		{
			var result = InstructionDecoder.Decode(instruction);
			Assert.Equal(InstructionMnemonic.LDAX, result);
		}

		[Fact]
		public void store_hl_direct()
		{
			var computer = new Computer();
			computer.Reset();

			computer.H = 0x55;
			computer.L = 0xaa;
			computer.StoreHLDirect();

			Assert.Equal(0xaa, computer.ComputerMemory[computer.PC - 2]);
			Assert.Equal(0x55, computer.ComputerMemory[computer.PC - 1]);
		}

		[Fact]
		public void load_register_pair_immediate_b()
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC + 2] = 0x1;
			computer.ComputerMemory[computer.PC + 1] = 5;
			computer.ComputerMemory[computer.PC + 2] = 7;

			computer.LoadRegisterPairImmediate();

			Assert.Equal(7, computer.B);
			Assert.Equal(5, computer.C);
		}

		[Fact]
		public void load_accumulator_direct()
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC + 1] = 12;
			computer.ComputerMemory[computer.PC + 2] = 0;
			computer.ComputerMemory[12] = 23;

			computer.LoadAccumulatorDirect();

			Assert.Equal(23, computer.A);
		}

		[Fact]
		public void load_accumulator_indirect_using_de()
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0x0a + 0x10; // use D & E registers
			computer.D = 0;
			computer.E = 12;
			computer.ComputerMemory[12] = 57;

			computer.LoadAccumulatorIndirect();

			Assert.Equal(57, computer.A);
		}

		[Fact]
		public void load_accumulator_indirect_using_bc()
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0x0a + 0x00; // use B & C registers
			computer.B = 0;
			computer.C = 12;
			computer.ComputerMemory[12] = 83;

			computer.LoadAccumulatorIndirect();

			Assert.Equal(83, computer.A);
		}

		[Fact]
		public void load_hl_direct()
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC + 1] = 3;
			computer.ComputerMemory[computer.PC + 2] = 5;
			computer.LoadHLDirect();

			Assert.Equal(5, computer.H);
			Assert.Equal(3, computer.L);
		}

		[Fact]
		public void store_accumulator_direct()
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC + 1] = 12;
			computer.ComputerMemory[computer.PC + 2] = 0;
			computer.A = 23;
			computer.StoreAccumulatorDirect();

			Assert.Equal(23, computer.ComputerMemory[12]);
		}

		[Theory]
		[InlineData(0x00, 12, 83)]
		[InlineData(0x10, 35, 83)]
		public void store_accumulator_indirect(byte registerPair, int memoryLocation, byte result)
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = (byte) (0x02 + registerPair);
			computer.B = 0;
			computer.C = 12;
			computer.A = 83;
			computer.D = 0;
			computer.E = 35;

			computer.StoreAccumulatorIndirect();

			Assert.Equal(result, computer.ComputerMemory[memoryLocation]);
		}

		[Fact]
		public void move_hl_to_pc()
		{
			var computer = new Computer();
			computer.Reset();

			computer.H = 0x05;
			computer.L = 0x33;
			computer.MoveHLToPC();

			Assert.Equal(0x0533, computer.PC);
		}

		[Fact]
		public void exchange_hl_with_de()
		{
			var computer = new Computer();
			computer.Reset();

			computer.H = 1;
			computer.L = 2;
			computer.D = 3;
			computer.E = 4;

			computer.ExchangeHLandDE();

			Assert.Equal(1, computer.D);
			Assert.Equal(2, computer.E);
			Assert.Equal(3, computer.H);
			Assert.Equal(4, computer.L);
		}

		[Fact]
		public void move_hl_sp()
		{
			var computer = new Computer();
			computer.Reset();

			//TODO: finsh this test
		}
	}
}
