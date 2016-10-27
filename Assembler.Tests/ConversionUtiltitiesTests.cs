using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assembler.Tests
{
	public class ConversionUtiltitiesTests
	{
		[Fact]
		public void string_to_int()
		{
			Assert.Equal(5, "5".ToInt());
		}

		[Fact]
		public void int_to_hex()
		{
			int temp = 0x55;

			Assert.Equal("55", temp.ToHex());
		}

		[Theory]
		[InlineData("055H", "55")]
		[InlineData("15", "0F")]
		public void string_to_hex(string testval, string result)
		{
			Assert.Equal(result, testval.ToHex());
		}

		[Theory]
		[InlineData("\tADD B",OpcodeEnum.ADD,"B")]
		[InlineData("  ADD B", OpcodeEnum.ADD, "B")]
		[InlineData(" ADD B  ", OpcodeEnum.ADD, "B")]
		[InlineData("\tDB 'this is a test string'", OpcodeEnum.DB, "'this is a test string'")]
		[InlineData("TEMP1 DB (TEST1 + 56H)", OpcodeEnum.DB, "(TEST1 + 56H)")]
		public void get_token(string testval, OpcodeEnum result1,string result2)
		{
			var sourceLine = new SourceLine(testval);

			Assert.Equal(result1, sourceLine.OpCode);
			Assert.Equal(result2, sourceLine.Operand);
		}
	}
}
