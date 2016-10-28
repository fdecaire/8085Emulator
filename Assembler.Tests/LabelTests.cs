using Xunit;

namespace Assembler.Tests
{
	public class LabelTests
	{
		[Fact]
		public void label_defined_first()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = "  MOV A,C\nTEMP0: CMA\n JMP TEMP0";
			assembler.AssembleCode();

			Assert.Equal("792FC30100", assembler.HexResult);
		}

		[Fact]
		public void label_referenced_first()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " JMP TEMP0\n  MOV A,C\nTEMP0: CMA\n";
			assembler.AssembleCode();

			Assert.Equal("C30400792F", assembler.HexResult);
		}

		[Fact]
		public void label_contained_in_formula()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " JMP TEMP0+1\n  MOV A,C\nTEMP0: CMA\n";
			assembler.AssembleCode();

			Assert.Equal("C34010792F", assembler.HexResult);
		}

		[Fact]
		public void comment_line()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = ";This is a comment line\n;this is another line of comments\n";
			assembler.AssembleCode();

			Assert.Equal("", assembler.HexResult);
		}
	}
}
