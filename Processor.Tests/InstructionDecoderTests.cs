using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Processor.Tests
{
	public class InstructionDecoderTests
	{
		[Theory]
		[InlineData(0x90)]
		[InlineData(0x91)]
		[InlineData(0x92)]
		[InlineData(0x93)]
		[InlineData(0x94)]
		[InlineData(0x95)]
		[InlineData(0x96)]
		[InlineData(0x97)]
		public void decode_subtract_instruction(byte instruction)
		{
			var result = InstructionDecoder.Decode(instruction);
			Assert.Equal(InstructionMnemonic.SUB, result);
		}

		[Theory]
		[InlineData(0x98)]
		[InlineData(0x99)]
		[InlineData(0x9a)]
		[InlineData(0x9b)]
		[InlineData(0x9c)]
		[InlineData(0x9d)]
		[InlineData(0x9e)]
		[InlineData(0x9f)]
		public void decode_subtract_with_borrow_instruction(byte instruction)
		{
			var result = InstructionDecoder.Decode(instruction);
			Assert.Equal(InstructionMnemonic.SBB, result);
		}

		[Theory]
		[InlineData(0x88)]
		[InlineData(0x89)]
		[InlineData(0x8a)]
		[InlineData(0x8b)]
		[InlineData(0x8c)]
		[InlineData(0x8d)]
		[InlineData(0x8e)]
		[InlineData(0x8f)]
		public void decode_add_with_carry_instruction(byte instruction)
		{
			var result = InstructionDecoder.Decode(instruction);
			Assert.Equal(InstructionMnemonic.ADC, result);
		}

		[Theory]
		[InlineData(0x80)]
		[InlineData(0x81)]
		[InlineData(0x82)]
		[InlineData(0x83)]
		[InlineData(0x84)]
		[InlineData(0x85)]
		[InlineData(0x86)]
		[InlineData(0x87)]
		public void decode_add_carry_instruction(byte instruction)
		{
			var result = InstructionDecoder.Decode(instruction);
			Assert.Equal(InstructionMnemonic.ADD, result);
		}

		[Theory]
		[InlineData(0x09)]
		[InlineData(0x19)]
		[InlineData(0x29)]
		[InlineData(0x39)]
		public void decode_double_add_instruction(byte instruction)
		{
			var result = InstructionDecoder.Decode(instruction);
			Assert.Equal(InstructionMnemonic.DAD, result);
		}

		[Theory]
		[InlineData(0x06)]
		[InlineData(0x0e)]
		[InlineData(0x16)]
		[InlineData(0x1e)]
		[InlineData(0x26)]
		[InlineData(0x2e)]
		[InlineData(0x36)]
		[InlineData(0x3e)]
		public void move_immediate_instruction(byte instruction)
		{
			var result = InstructionDecoder.Decode(instruction);
			Assert.Equal(InstructionMnemonic.MVI, result);
		}

		[Theory]
		[InlineData(0x05)]
		[InlineData(0x0d)]
		[InlineData(0x15)]
		[InlineData(0x1d)]
		[InlineData(0x25)]
		[InlineData(0x2d)]
		[InlineData(0x35)]
		[InlineData(0x3d)]
		public void decode_decrement_instruction(byte instruction)
		{
			var result = InstructionDecoder.Decode(instruction);
			Assert.Equal(InstructionMnemonic.DCR, result);
		}

		[Theory]
		[InlineData(0x0b)]
		[InlineData(0x1b)]
		[InlineData(0x2b)]
		[InlineData(0x3b)]
		public void decode_decrement_register_pair(byte instruction)
		{
			var result = InstructionDecoder.Decode(instruction);
			Assert.Equal(InstructionMnemonic.DCX, result);
		}

		[Theory]
		[InlineData(0x04)]
		[InlineData(0x0c)]
		[InlineData(0x14)]
		[InlineData(0x1c)]
		[InlineData(0x24)]
		[InlineData(0x2c)]
		[InlineData(0x34)]
		[InlineData(0x3c)]
		public void decode_increment_instruction(byte instruction)
		{
			var result = InstructionDecoder.Decode(instruction);
			Assert.Equal(InstructionMnemonic.INR, result);
		}

		[Theory]
		[InlineData(0xc7)]
		[InlineData(0xcf)]
		[InlineData(0xd7)]
		[InlineData(0xdf)]
		[InlineData(0xe7)]
		[InlineData(0xef)]
		[InlineData(0xf7)]
		[InlineData(0xff)]
		public void decode_restart_instruction(byte instruction)
		{
			var result = InstructionDecoder.Decode(instruction);
			Assert.Equal(InstructionMnemonic.RST, result);
		}

		[Theory]
		[InlineData(0xa8)]
		[InlineData(0xa9)]
		[InlineData(0xaa)]
		[InlineData(0xab)]
		[InlineData(0xac)]
		[InlineData(0xad)]
		[InlineData(0xae)]
		[InlineData(0xaf)]
		public void decode_exclusive_or_instruction(byte instruction)
		{
			var result = InstructionDecoder.Decode(instruction);
			Assert.Equal(InstructionMnemonic.XRA, result);
		}

		[Theory]
		[InlineData(0xa0)]
		[InlineData(0xa1)]
		[InlineData(0xa2)]
		[InlineData(0xa3)]
		[InlineData(0xa4)]
		[InlineData(0xa5)]
		[InlineData(0xa6)]
		[InlineData(0xa7)]
		public void decode_logical_and_instruction(byte instruction)
		{
			var result = InstructionDecoder.Decode(instruction);
			Assert.Equal(InstructionMnemonic.ANA, result);
		}
	}
}
