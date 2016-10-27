using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assembler.Tests
{
	public class DataTests
	{
		[Fact]
		public void db_ascii_instruction()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " DB 'This is test text'";
			assembler.AssembleCode();

			Assert.Equal("5468697320697320746573742074657874", assembler.HexResult);
		}

		[Fact]
		public void db_ascii_program_counter_update()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " DB 'This is test text'";
			assembler.AssembleCode();

			Assert.Equal(17, assembler.CurrentAddress);
		}

		[Fact]
		public void db_hex_instruction()
		{
			// DB 0A3H  = A3
			// DB -03h,5*2  = FD0A
		}

		[Fact]
		public void dw_instruction()
		{
			// DW 'A','AB'  = 41004241
			// DW  4H = 00400
			var assembler = new Assembler();
			assembler.AssemblyCode = "TEMPP: DW  0400H\n MOV A,B\n";
			assembler.AssembleCode();

			Assert.Equal("000478", assembler.HexResult);
		}

		[Fact]
		public void equ_instruction()
		{
			// STACK	EQU	TEMPP+256
			// BDOS	    EQU	00005H	;BDOS ENTRY TO CP/M
			// WBOOT    EQU 00000H; RE - ENTRY TO CP/ M WARM BOOT
			var assembler = new Assembler();
			assembler.AssemblyCode = "BDOS  EQU  00005H\n CALL BDOS\n";
			assembler.AssembleCode();

			Assert.Equal("CD0500", assembler.HexResult);
		}

		[Fact]
		public void ds_instruction()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = "TEMP0:	DS	5\n";

			assembler.AssembleCode();

			Assert.Equal("0000000000", assembler.HexResult);
		}

		[Fact]
		public void equ_instruction_post_label()
		{
			// STACK	EQU	TEMPP+256
			// BDOS	    EQU	00005H	;BDOS ENTRY TO CP/M
			// WBOOT    EQU 00000H; RE - ENTRY TO CP/ M WARM BOOT
			var assembler = new Assembler();
			assembler.AssemblyCode = " CALL BDOS\nBDOS  EQU  00005H\n";
			assembler.AssembleCode();

			Assert.Equal("CD0500", assembler.HexResult);
		}

		[Fact]
		public void add_directive()
		{
			// STACK	EQU	TEMPP+256
			var assembler = new Assembler();
			assembler.AssemblyCode = " DB 2+26";
			assembler.AssembleCode();

			Assert.Equal("1C", assembler.HexResult);
		}

		[Fact]
		public void subtract_directive()
		{
			// STACK EQU TEMPP-256
			var assembler = new Assembler();
			assembler.AssemblyCode = " DB 26-3";
			assembler.AssembleCode();

			Assert.Equal("17", assembler.HexResult);
		}

		[Fact]
		public void or_directive()
		{
			// (TEMP0 OR 0FFH)
			var assembler = new Assembler();
			assembler.AssemblyCode = " DB (55H OR 33H)";
			assembler.AssembleCode();

			Assert.Equal("77", assembler.HexResult);
		}

		[Fact]
		public void and_directive()
		{
			// MVI	L,(TEMP0 AND 0FFH)
			var assembler = new Assembler();
			assembler.AssemblyCode = " DB (55H AND 34H)";
			assembler.AssembleCode();

			Assert.Equal("14", assembler.HexResult);
		}

		[Fact]
		public void divide_directive()
		{
			// MVI	H,(TEMP0 / 0FFH)
		}

		[Fact]
		public void multiply_directive()
		{
			
		}
	}
}
