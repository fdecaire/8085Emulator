using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor
{
	//http://elearning.tukenya.ac.ke/pluginfile.php/14951/mod_resource/content/1/8085.InstructionSet.full-alphabetical.pdf
	public class InstructionStats
	{
		public int MCycles(InstructionMnemonic instruction)
		{
			switch (instruction)
			{
				case InstructionMnemonic.ACI:
					return 2;
				case InstructionMnemonic.ADC:
					break;
				case InstructionMnemonic.ADD:
					break;
				case InstructionMnemonic.ADI:
					break;
				case InstructionMnemonic.ANA:
					break;
				case InstructionMnemonic.ANI:
					break;
				case InstructionMnemonic.CALL:
					break;
				case InstructionMnemonic.CC:
					break;
				case InstructionMnemonic.CNC:
					break;
				case InstructionMnemonic.CP:
					break;
				case InstructionMnemonic.CM:
					break;
				case InstructionMnemonic.CPE:
					break;
				case InstructionMnemonic.CPO:
					break;
				case InstructionMnemonic.CZ:
					break;
				case InstructionMnemonic.CNZ:
					break;
				case InstructionMnemonic.CMA:
					break;
				case InstructionMnemonic.CMC:
					break;
				case InstructionMnemonic.CMP:
					break;
				case InstructionMnemonic.CPI:
					break;
				case InstructionMnemonic.DAA:
					break;

					//TODO: finish sorting these
				case InstructionMnemonic.NOP:
					break;
				case InstructionMnemonic.HLT:
					break;
				case InstructionMnemonic.MOV:
					break;
				case InstructionMnemonic.RET:
					break;
				case InstructionMnemonic.RC:
					break;
				case InstructionMnemonic.RNC:
					break;
				case InstructionMnemonic.RZ:
					break;
				case InstructionMnemonic.RNZ:
					break;
				case InstructionMnemonic.RP:
					break;
				case InstructionMnemonic.RM:
					break;
				case InstructionMnemonic.RPE:
					break;
				case InstructionMnemonic.RPO:
					break;
				case InstructionMnemonic.RST:
					break;
				case InstructionMnemonic.IN:
					break;
				case InstructionMnemonic.OUT:
					break;
				case InstructionMnemonic.LXI:
					break;
				case InstructionMnemonic.PUSH:
					break;
				case InstructionMnemonic.POP:
					break;
				case InstructionMnemonic.STA:
					break;
				case InstructionMnemonic.LDA:
					break;
				case InstructionMnemonic.XCHG:
					break;
				case InstructionMnemonic.XTHL:
					break;
				case InstructionMnemonic.SPHL:
					break;
				case InstructionMnemonic.PCHL:
					break;
				case InstructionMnemonic.DAD:
					break;
				case InstructionMnemonic.STAX:
					break;
				case InstructionMnemonic.LDAX:
					break;
				case InstructionMnemonic.INX:
					break;
				case InstructionMnemonic.MVI:
					break;
				case InstructionMnemonic.INR:
					break;
				case InstructionMnemonic.DCR:
					break;
				case InstructionMnemonic.SUB:
					break;
				case InstructionMnemonic.SBB:
					break;
				case InstructionMnemonic.XRA:
					break;
				case InstructionMnemonic.ORA:
					break;
				case InstructionMnemonic.SUI:
					break;
				case InstructionMnemonic.SBI:
					break;
				case InstructionMnemonic.XRI:
					break;
				case InstructionMnemonic.ORI:
					break;
				case InstructionMnemonic.RLC:
					break;
				case InstructionMnemonic.RRC:
					break;
				case InstructionMnemonic.RAL:
					break;
				case InstructionMnemonic.RAR:
					break;
				case InstructionMnemonic.JMP:
					break;
				case InstructionMnemonic.JC:
					break;
				case InstructionMnemonic.JNC:
					break;
				case InstructionMnemonic.JZ:
					break;
				case InstructionMnemonic.JNZ:
					break;
				case InstructionMnemonic.JP:
					break;
				case InstructionMnemonic.JM:
					break;
				case InstructionMnemonic.JPE:
					break;
				case InstructionMnemonic.JPO:
					break;
				case InstructionMnemonic.DCX:
					break;
				case InstructionMnemonic.STC:
					break;
				case InstructionMnemonic.SHLD:
					break;
				case InstructionMnemonic.LHLD:
					break;
				case InstructionMnemonic.RIM:
					break;
				case InstructionMnemonic.SIM:
					break;
				case InstructionMnemonic.EI:
					break;
				case InstructionMnemonic.DI:
					break;
			}

			return 0;
		}

		public int TStates(InstructionMnemonic instruction)
		{
			switch (instruction)
			{
				case InstructionMnemonic.NOP:
					break;
				case InstructionMnemonic.HLT:
					break;
				case InstructionMnemonic.MOV:
					break;
				case InstructionMnemonic.CALL:
					break;
				case InstructionMnemonic.ANA:
					break;
				case InstructionMnemonic.CC:
					break;
				case InstructionMnemonic.CNC:
					break;
				case InstructionMnemonic.CZ:
					break;
				case InstructionMnemonic.CNZ:
					break;
				case InstructionMnemonic.CP:
					break;
				case InstructionMnemonic.CM:
					break;
				case InstructionMnemonic.CPE:
					break;
				case InstructionMnemonic.CPO:
					break;
				case InstructionMnemonic.RET:
					break;
				case InstructionMnemonic.RC:
					break;
				case InstructionMnemonic.RNC:
					break;
				case InstructionMnemonic.RZ:
					break;
				case InstructionMnemonic.RNZ:
					break;
				case InstructionMnemonic.RP:
					break;
				case InstructionMnemonic.RM:
					break;
				case InstructionMnemonic.RPE:
					break;
				case InstructionMnemonic.RPO:
					break;
				case InstructionMnemonic.RST:
					break;
				case InstructionMnemonic.IN:
					break;
				case InstructionMnemonic.OUT:
					break;
				case InstructionMnemonic.LXI:
					break;
				case InstructionMnemonic.PUSH:
					break;
				case InstructionMnemonic.POP:
					break;
				case InstructionMnemonic.STA:
					break;
				case InstructionMnemonic.LDA:
					break;
				case InstructionMnemonic.XCHG:
					break;
				case InstructionMnemonic.XTHL:
					break;
				case InstructionMnemonic.SPHL:
					break;
				case InstructionMnemonic.PCHL:
					break;
				case InstructionMnemonic.DAD:
					break;
				case InstructionMnemonic.STAX:
					break;
				case InstructionMnemonic.LDAX:
					break;
				case InstructionMnemonic.INX:
					break;
				case InstructionMnemonic.MVI:
					break;
				case InstructionMnemonic.INR:
					break;
				case InstructionMnemonic.DCR:
					break;
				case InstructionMnemonic.ADD:
					break;
				case InstructionMnemonic.ADC:
					break;
				case InstructionMnemonic.SUB:
					break;
				case InstructionMnemonic.SBB:
					break;
				case InstructionMnemonic.XRA:
					break;
				case InstructionMnemonic.ORA:
					break;
				case InstructionMnemonic.CMP:
					break;
				case InstructionMnemonic.ADI:
					break;
				case InstructionMnemonic.ACI:
					break;
				case InstructionMnemonic.SUI:
					break;
				case InstructionMnemonic.SBI:
					break;
				case InstructionMnemonic.ANI:
					break;
				case InstructionMnemonic.XRI:
					break;
				case InstructionMnemonic.ORI:
					break;
				case InstructionMnemonic.CPI:
					break;
				case InstructionMnemonic.RLC:
					break;
				case InstructionMnemonic.RRC:
					break;
				case InstructionMnemonic.RAL:
					break;
				case InstructionMnemonic.RAR:
					break;
				case InstructionMnemonic.JMP:
					break;
				case InstructionMnemonic.JC:
					break;
				case InstructionMnemonic.JNC:
					break;
				case InstructionMnemonic.JZ:
					break;
				case InstructionMnemonic.JNZ:
					break;
				case InstructionMnemonic.JP:
					break;
				case InstructionMnemonic.JM:
					break;
				case InstructionMnemonic.JPE:
					break;
				case InstructionMnemonic.JPO:
					break;
				case InstructionMnemonic.DCX:
					break;
				case InstructionMnemonic.CMA:
					break;
				case InstructionMnemonic.STC:
					break;
				case InstructionMnemonic.CMC:
					break;
				case InstructionMnemonic.DAA:
					break;
				case InstructionMnemonic.SHLD:
					break;
				case InstructionMnemonic.LHLD:
					break;
				case InstructionMnemonic.RIM:
					break;
				case InstructionMnemonic.SIM:
					break;
				case InstructionMnemonic.EI:
					break;
				case InstructionMnemonic.DI:
					break;
			}
			return 0;
		}
	}
}
