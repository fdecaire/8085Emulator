using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Processor.Tests
{
	public class MoveTests
	{
		[Fact]
		public void read_move_source()
		{
			var computer = new Computer();
			var result = computer.DecodeRegisterNumbers(0x72);

			Assert.Equal(2, result.Source);
		}

		[Fact]
		public void read_move_dest()
		{
			var computer = new Computer();
			var result = computer.DecodeRegisterNumbers(0x72);

			Assert.Equal(6, result.Destination);
		}

		[Fact]
		public void move_b_to_c()
		{
			var computer = new Computer();

			computer.Reset();
			computer.B = 5;
			computer.SaveRegister(1, computer.ReadRegister(0));

			Assert.Equal(5, computer.C);
		}

		[Fact]
		public void move_b_to_d()
		{
			var computer = new Computer();

			computer.Reset();
			computer.B = 5;
			computer.SaveRegister(2, computer.ReadRegister(0));

			Assert.Equal(5, computer.D);
		}

		[Fact]
		public void move_b_to_e()
		{
			var computer = new Computer();

			computer.Reset();
			computer.B = 5;
			computer.SaveRegister(3, computer.ReadRegister(0));

			Assert.Equal(5, computer.E);
		}

		[Fact]
		public void move_b_to_h()
		{
			var computer = new Computer();

			computer.Reset();
			computer.B = 5;
			computer.SaveRegister(4, computer.ReadRegister(0));

			Assert.Equal(5, computer.H);
		}

		[Fact]
		public void move_b_to_l()
		{
			var computer = new Computer();

			computer.Reset();
			computer.B = 5;
			computer.SaveRegister(5, computer.ReadRegister(0));

			Assert.Equal(5, computer.L);
		}

		[Fact]
		public void move_b_to_accumulator()
		{
			var computer = new Computer();

			computer.Reset();
			computer.B = 5;
			computer.SaveRegister(7, computer.ReadRegister(0));

			Assert.Equal(5, computer.A);
		}

		[Fact]
		public void move_memory_to_accumulator()
		{
			var computer = new Computer();

			computer.Reset();
			computer.H = 0;
			computer.L = 5;
			computer.ComputerMemory[5] = 12;
			computer.SaveRegister(7, computer.ReadRegister(6));

			Assert.Equal(12, computer.A);
		}

		[Fact]
		public void move_accumulator_to_memory()
		{
			var computer = new Computer();

			computer.Reset();
			computer.A = 55;
			computer.H = 0;
			computer.L = 5;
			computer.SaveRegister(6, computer.ReadRegister(7));

			Assert.Equal(55, computer.ComputerMemory[5]);
		}

		[Fact]
		public void move_instruction_regb_to_regc()
		{
			var computer = new Computer();
			computer.Reset();

			computer.B = 34;

			computer.ComputerMemory[computer.PC] = 0x40 + 0x08 + 0x00;
			computer.Move();

			Assert.Equal(34,computer.C);
		}

		[Fact]
		public void move_immediate_to_c()
		{
			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory[computer.PC] = 0x06 + 0x08;
			computer.ComputerMemory[computer.PC + 1] = 5;
			computer.MoveImmediate();

			Assert.Equal(5, computer.C);
		}

		
	}
}
