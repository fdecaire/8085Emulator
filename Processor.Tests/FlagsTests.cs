using Xunit;

namespace Processor.Tests
{
	public class FlagsTests
	{
		[Fact]
		public void zero_flag_on()
		{
			var computer = new Computer();

			computer.SetFlags(0);
			Assert.True(computer.Flags.Zero);
		}

		[Fact]
		public void zero_flag_off()
		{
			var computer = new Computer();

			computer.SetFlags(55);
			Assert.False(computer.Flags.Zero);
		}

		[Fact]
		public void parity_flag_on()
		{
			var computer = new Computer();

			computer.SetFlags(0x3);
			Assert.True(computer.Flags.Parity);
		}

		[Fact]
		public void parity_flag_off()
		{
			var computer = new Computer();

			computer.SetFlags(0x1);
			Assert.False(computer.Flags.Parity);
		}

		[Fact]
		public void sign_flag_on()
		{
			var computer = new Computer();

			computer.SetFlags(0x80);
			Assert.True(computer.Flags.Sign);
		}

		[Fact]
		public void sign_flag_off()
		{
			var computer = new Computer();

			computer.SetFlags(0x77);
			Assert.False(computer.Flags.Sign);
		}

		[Fact]
		public void set_carry_flag_on()
		{
			var computer = new Computer();

			computer.Flags.Carry = true;
			Assert.True(computer.Flags.Carry);
		}

		[Fact]
		public void set_carry_flag_off()
		{
			var computer = new Computer();

			computer.Flags.Carry = false;
			Assert.False(computer.Flags.Carry);
		}

		[Fact]
		public void set_aux_carry_flag_on()
		{
			var computer = new Computer();

			computer.SetAuxCarryFlag(0xff,0x83);
			Assert.True(computer.Flags.AuxCarry);
		}

		[Theory]
		[InlineData(0x04, 0x01)]
		[InlineData(0x01, 0x04)]
		[InlineData(0x01, 0x01)]
		public void set_aux_carry_flag_off(byte data1, byte data2)
		{
			var computer = new Computer();

			computer.SetAuxCarryFlag(data1, data2);
			Assert.False(computer.Flags.AuxCarry);
		}
	}
}
