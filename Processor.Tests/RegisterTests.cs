using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Processor.Tests
{
	public class RegisterTests
	{
		[Fact]
		public void register_increment()
		{
			byte reg = 0;

			reg = 1;

			reg = reg.Increment();

			Assert.Equal(2, reg);
		}

		[Fact]
		public void register_low_to_high_increment()
		{
			byte reg = 0;

			reg = 0xff;

			reg = reg.Increment();

			Assert.Equal(0, reg);
		}

		[Fact]
		public void register_decrement()
		{
			byte reg = 0;

			reg = 5;

			reg = reg.Decrement();

			Assert.Equal(4, reg);
		}

		[Fact]
		public void register_high_to_low_decrement()
		{
			byte reg = 0;

			reg = 0x0;

			reg = reg.Decrement();

			Assert.Equal(0xff, reg);
		}

		[Fact]
		public void register_shift_left()
		{
			byte reg = 0;

			reg = 3;
			reg = reg.ShiftLeft();

			Assert.Equal(6,reg);
		}

		[Fact]
		public void register_shift_right()
		{
			byte reg = 0;

			reg = 0x08;
			reg = reg.ShiftRight();

			Assert.Equal(0x4, reg);
		}
	}
}
