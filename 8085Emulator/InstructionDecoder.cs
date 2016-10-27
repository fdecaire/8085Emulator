using System;

namespace Processor
{
	public class InstructionDecoder
	{
		public static InstructionMnemonic Decode(byte instruction)
		{
			if (instruction == 0x76)
			{
				return InstructionMnemonic.HLT;
			}

			if ((instruction & 0xf8) == 0xa0)
			{
				return InstructionMnemonic.ANA;
			}

			if ((instruction & 0xc7) == 0xc7)
			{
				return InstructionMnemonic.RST;
			}

			if ((instruction & 0xc0) == 0x40)
			{
				return InstructionMnemonic.MOV;
			}

			if ((instruction & 0xf8) == 0x88)
			{
				return InstructionMnemonic.ADC;
			}

			if ((instruction & 0xf8) == 0x80)
			{
				return InstructionMnemonic.ADD;
			}

			if ((instruction & 0xcf) == 0x09) // 00RP1001
			{
				return InstructionMnemonic.DAD;
			}

			if ((instruction & 0xc7) == 0x05)
			{
				return InstructionMnemonic.DCR;
			}

			if ((instruction & 0xcf) == 0x0b)
			{
				return InstructionMnemonic.DCX;
			}

			if ((instruction & 0xcf) == 0x01)
			{
				return InstructionMnemonic.LXI;
			}

			if ((instruction & 0xcf) == 0xc5)
			{
				return InstructionMnemonic.PUSH;
			}

			if ((instruction & 0xcf) == 0xc1)
			{
				return InstructionMnemonic.POP;
			}

			if ((instruction & 0xef) == 0x02)
			{
				return InstructionMnemonic.STAX;
			}

			if ((instruction & 0xef) == 0x0a)
			{
				return InstructionMnemonic.LDAX;
			}

			if ((instruction & 0xcf) == 0x03)
			{
				return InstructionMnemonic.INX;
			}

			if ((instruction & 0xc7) == 0x06)
			{
				return InstructionMnemonic.MVI;
			}

			if ((instruction & 0xc7) == 0x04)
			{
				return InstructionMnemonic.INR;
			}

			if ((instruction & 0xf8) == 0x90)
			{
				return InstructionMnemonic.SUB;
			}

			if ((instruction & 0xf8) == 0x98)
			{
				return InstructionMnemonic.SBB;
			}

			if ((instruction & 0xf8) == 0xa8)
			{
				return InstructionMnemonic.XRA;
			}

			if ((instruction & 0xf8) == 0xb0)
			{
				return InstructionMnemonic.ORA;
			}

			return (InstructionMnemonic)Enum.ToObject(typeof(InstructionMnemonic), instruction);
		}
	}
}
