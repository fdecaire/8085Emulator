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

		[Theory]
		[InlineData("01H",true)]
		[InlineData("02H", true)]
		[InlineData("03H", true)]
		[InlineData("04H", true)]
		[InlineData("05H", true)]
		[InlineData("06H", true)]
		[InlineData("07H", true)]
		[InlineData("08H", true)]
		[InlineData("09H", true)]
		[InlineData("0AH", true)]
		[InlineData("0BH", true)]
		[InlineData("0CH", true)]
		[InlineData("0DH", true)]
		[InlineData("0EH", true)]
		[InlineData("0FH", true)]
		[InlineData("0GH", false)]
		[InlineData("0VH", false)]
		[InlineData("55", false)]
		[InlineData("test", false)]
		public void string_is_hex(string input, bool expected)
		{
			var result = input.IsHex();

			Assert.Equal(result, expected);
		}

		[Theory]
		[InlineData("3829", true)]
		[InlineData("00", true)]
		[InlineData("34", true)]
		[InlineData("04H", false)]
		[InlineData("label", false)]
		[InlineData("9999", true)]
		public void string_is_number(string input, bool expected)
		{
			var result = input.IsNumber();

			Assert.Equal(result, expected);
		}
	}
}
